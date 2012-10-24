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
    public class OrderLinesController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /OrderLines/

        public ViewResult Index()
        {
            var order_lines = db.ORDER_LINES.Include("ORDER").Include("PRODUCT");
            return View(order_lines.ToList());
        }

        //
        // GET: /OrderLines/Details/5

        public ViewResult Details(int id)
        {
            ORDER_LINES order_lines = db.ORDER_LINES.Single(o => o.ID == id);
            return View(order_lines);
        }

        //
        // GET: /OrderLines/Create

        public ActionResult Create()
        {
            int order_id = Convert.ToInt32(Session["order_id"]);
            
            var order_line = new ORDER_LINES { 
                ORDER_ID = order_id
            };

            //ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "CP_ENVIO");
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME");
            return View(order_line);
        } 

        //
        // POST: /OrderLines/Create

        [HttpPost]
        public ActionResult Create(ORDER_LINES order_lines)
        {
            if (ModelState.IsValid)
            {
                db.ORDER_LINES.AddObject(order_lines);
                db.SaveChanges();

                return RedirectToActionPermanent("Order", "Orders", new { id = order_lines.ORDER_ID});
                //return RedirectToAction("Index");  
            }

            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "CP_ENVIO", order_lines.ORDER_ID);
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME", order_lines.PRODUCT_ID);
            return View(order_lines);
        }
        
        //
        // GET: /OrderLines/Edit/5
 
        public ActionResult Edit(int id)
        {
            ORDER_LINES order_lines = db.ORDER_LINES.Single(o => o.ID == id);
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "CP_ENVIO", order_lines.ORDER_ID);
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME", order_lines.PRODUCT_ID);
            return View(order_lines);
        }

        //
        // POST: /OrderLines/Edit/5

        [HttpPost]
        public ActionResult Edit(ORDER_LINES order_lines)
        {
            if (ModelState.IsValid)
            {
                db.ORDER_LINES.Attach(order_lines);
                db.ObjectStateManager.ChangeObjectState(order_lines, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "CP_ENVIO", order_lines.ORDER_ID);
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME", order_lines.PRODUCT_ID);
            return View(order_lines);
        }

        //
        // GET: /OrderLines/Delete/5
 
        public ActionResult Delete(int id)
        {
            ORDER_LINES order_lines = db.ORDER_LINES.Single(o => o.ID == id);
            return View(order_lines);
        }

        //
        // POST: /OrderLines/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ORDER_LINES order_lines = db.ORDER_LINES.Single(o => o.ID == id);
            db.ORDER_LINES.DeleteObject(order_lines);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}