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
    public class DemosController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Demos/

        public ViewResult Index()
        {
            var customers = db.CUSTOMERS.Include("EMPLOYEE");
            return View(customers.ToList());
        }

        //
        // GET: /Demos/Details/5

        public ViewResult Details(int id)
        {
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.ID == id);
            return View(customer);
        }

        //
        // GET: /Demos/Create

        public ActionResult Create()
        {
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN");
            return View();
        } 

        //
        // POST: /Demos/Create

        [HttpPost]
        public ActionResult Create(CUSTOMER customer)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMERS.AddObject(customer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", customer.EMPLOYEE_ID);
            return View(customer);
        }
        
        //
        // GET: /Demos/Edit/5
 
        public ActionResult Edit(int id)
        {
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.ID == id);
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN", customer.EMPLOYEE_ID);
            return View(customer);
        }

        //
        // POST: /Demos/Edit/5

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
        // GET: /Demos/Delete/5
 
        public ActionResult Delete(int id)
        {
            CUSTOMER customer = db.CUSTOMERS.Single(c => c.ID == id);
            return View(customer);
        }

        //
        // POST: /Demos/Delete/5

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

        public ActionResult demo() {
            return View();
        }
    }
}