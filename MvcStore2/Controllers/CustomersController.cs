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
    public class CustomersController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Customers/
            
        public ViewResult Index()
        {
            var customers = db.CUSTOMERS.Include("EMPLOYEE");
            Session["customer_dni"] = null;
            
            return View(customers.ToList());
        }

        //
        // GET: /Customers/Details/5

        public ViewResult Details(int id)
        {
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.ID == id);
            return View(customer);
        }

        //
        // GET: /Customers/Create

        public ActionResult Create()
        {
            int employee_id = Convert.ToInt32(Session["user_id"]);
            if (Session["customer_dni"] != null)
            {
                string customer_dni = Session["customer_dni"].ToString();

                var customer = new CUSTOMER
                {
                    EMPLOYEE_ID = employee_id,
                    DNI = customer_dni
                };
                return View(customer);
            }
            else {
                var customer = new CUSTOMER
                {
                    EMPLOYEE_ID = employee_id                    
                };

                return View(customer);
            }
                

            //ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN");
            //return View(customer);
        } 

        //
        // POST: /Customers/Create

        [HttpPost]
        public ActionResult Create(CUSTOMER customer)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMERS.AddObject(customer);
                db.SaveChanges();

                //int employee_id = Convert.ToInt32(Session["user_id"]);                
                //var last_customer= db.CUSTOMERS.ToList().Where(m => m.EMPLOYEE_ID == employee_id).LastOrDefault();

                var new_customer = db.CUSTOMERS.Single(m => m.DNI == customer.DNI);

                return RedirectToAction("Customer", new { id = new_customer.ID });  
            }

            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", customer.EMPLOYEE_ID);
            return View(customer);
        }
        
        //
        // GET: /Customers/Edit/5
 
        public ActionResult Edit(int id)
        {
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.ID == id);
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", customer.EMPLOYEE_ID);
            return View(customer);
        }

        //
        // POST: /Customers/Edit/5

        [HttpPost]
        public ActionResult Edit(CUSTOMER customer)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMERS.Attach(customer);
                db.ObjectStateManager.ChangeObjectState(customer, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", customer.EMPLOYEE_ID);
            return View(customer);
        }

        //
        // GET: /Customers/Delete/5
 
        public ActionResult Delete(int id)
        {
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.ID == id);
            return View(customer);
        }

        //
        // POST: /Customers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.ID == id);
            db.CUSTOMERS.DeleteObject(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult DetailsByDni(string dni){
            
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.DNI== dni);            
            Session["cusstomer_name"] = customer.NAME;
            Session["cusstomer_id"] = customer.ID;
            
            return View(customer);
        }

        public ActionResult Search(string DNI)
        {
            var customer = db.CUSTOMERS.Where(x => x.DNI == DNI).FirstOrDefault();

            if (customer != null)
            {
                Session["customer_name"] = customer.NAME;
                Session["customer_id"] = customer.ID;
                return Json(new { id = customer.ID, name = customer.NAME, message = true });
            }
            else
            {
                Session["customer_dni"] = DNI;
                Session["customer_name"] = null;
                return Json(new { message = false });
            }

        }

        public ActionResult Customer(int id)
        {
            CUSTOMER customer = db.CUSTOMERS.Include("ORDERS").Single(c => c.ID == id);
            
            Session["customer_name"] = customer.NAME;
            Session["customer_id"] = id;

            ViewBag.Orders = customer.ORDERS.ToList();
            
            return View(customer);
        }
    }
}