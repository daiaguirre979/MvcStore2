using System;   
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStore2.Models;

namespace MvcStore2.Controllers
{ 
    public class OrdersController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Orders/

        public ViewResult Index()
        {
            var orders = db.ORDERS.Include("CUSTOMER").Include("EMPLOYEE");
            return View(orders.ToList());
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(int id)
        {

            ORDER order = db.ORDERS.Single(o => o.ID == id);
            return View(order);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            int customer_id = Convert.ToInt32(Session["customer_id"]);            
            int employee_id = Convert.ToInt32(Session["user_id"]);
            
            var order = new ORDER{
                ELABORATION_DATE = DateTime.Now,
                CUSTOMER_ID = customer_id,
                EMPLOYEE_ID = employee_id,
                STATE = 1
            };

            //ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME");
            //ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN");

            return View(order);
        } 

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(ORDER order)
        {
            if (ModelState.IsValid)
            {
                db.ORDERS.AddObject(order);
                db.SaveChanges();
                
                var last_order = db.ORDERS.ToList().LastOrDefault();
                return RedirectToActionPermanent("Order", new { id = last_order.ID });               
            }

            ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME", order.CUSTOMER_ID);
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", order.EMPLOYEE_ID);
            return View(order);
        }
        
        //
        // GET: /Orders/Edit/5
 
        public ActionResult Edit(int id)
        {
            ORDER order = db.ORDERS.Single(o => o.ID == id);
            ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME", order.CUSTOMER_ID);
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", order.EMPLOYEE_ID);
            return View(order);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(ORDER order)
        {
            if (ModelState.IsValid)
            {
                db.ORDERS.Attach(order);
                db.ObjectStateManager.ChangeObjectState(order, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME", order.CUSTOMER_ID);
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", order.EMPLOYEE_ID);
            return View(order);
        }

        //
        // GET: /Orders/Delete/5
 
        public ActionResult Delete(int id)
        {
            ORDER order = db.ORDERS.Single(o => o.ID == id);
            return View(order);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ORDER order = db.ORDERS.Single(o => o.ID == id);
            db.ORDERS.DeleteObject(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // NotAttended  1
        // InAttention  2
        // ReadytoSent  3
        // Sent         4

        public ViewResult NotAttended()
        
        {
            var orders = db.ORDERS.Include("CUSTOMER").Include("EMPLOYEE").Where(m => m.STATE == 1);
            Session["order_state"] = 1;
            Session["action_name"] = "NotAttended";
            Session["order_state_name"] = "No Atendidos";
    
            return View(orders.ToList());
        }
        
        public ViewResult InAttention()
        {
            var orders = db.ORDERS.Include("CUSTOMER").Include("EMPLOYEE").Where(m => m.STATE == 2);
            Session["order_state"] = 2;
            Session["action_name"] = "InAttention";
            Session["order_state_name"] = "En Atencion";

            return View(orders.ToList());
        }
        
        public ViewResult ReadyToSend()
        {
            var orders = db.ORDERS.Include("CUSTOMER").Include("EMPLOYEE").Where(m => m.STATE == 3);
            Session["order_state"] = 3;
            Session["action_name"] = "ReadyToSend";
            Session["order_state_name"] = "Listos Para Enviar";
            return View(orders.ToList());
        }
        public ViewResult Sent()
        {
            var orders = db.ORDERS.Include("CUSTOMER").Include("EMPLOYEE").Where(m => m.STATE == 4);
            Session["order_state"] = 4;
            Session["action_name"] = "Sent";
            Session["order_state_name"] = "Enviados";

            return View(orders.ToList());
        }

        public ViewResult Order(int id)
        {
            ORDER order = db.ORDERS.Single(o => o.ID == id);
            Session["order_id"] = order.ID;            
            return View(order);
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult ChangeStateOne(int id)
        {
            ORDER order = db.ORDERS.Single(o => o.ID == id);
            order.STATE = 2;
            order.ATTENTION_DATE = DateTime.Now;
        
            db.SaveChanges();
            return RedirectToAction("NotAttended");
        }
        public ActionResult ChangeStateTwo(int id)
        {
            ORDER order = db.ORDERS.Single(o => o.ID == id);
            order.STATE = 3;
            order.CHECK_DEPOT_DATE = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("InAttention");
        }
        public ActionResult ChangeStateThree(int id)
        {
            ORDER order = db.ORDERS.Single(o => o.ID == id);
            order.STATE = 4;
            order.SENDING_LIST_DATE = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("ReadyToSend");
        }
        [HttpPost]
        public ActionResult OrderPost(ORDER order)
        {
            if (ModelState.IsValid)
            {
                db.ORDERS.AddObject(order);
                db.SaveChanges();

                var last_order = db.ORDERS.ToList().LastOrDefault();
                return RedirectToActionPermanent("Order", new { id = last_order.ID});
            }

            return View(order);
        }

        //
        // POST: /Orders/Edit/5
        
        
        /*
        public string GenerateOrderCode(int customer_id)
        {
            var customer = db.CUSTOMERS.Single(m => m.ID == customer_id);

            string n_id = customer.ID.ToString();
            /*
            if()


            if (ModelState.IsValid)
            {
                db.ORDERS.Attach(order);
                db.ObjectStateManager.ChangeObjectState(order, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME", order.CUSTOMER_ID);
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", order.EMPLOYEE_ID);
            return View(order);
        }*/

    }
}