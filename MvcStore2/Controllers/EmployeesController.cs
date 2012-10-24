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
    public class EmployeesController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Employees/

        public ViewResult Index()
        {   
            int depot_id = Convert.ToInt32(Session["depot_id"]);
            var employees = db.EMPLOYEES.Include("DEPOT").Where(m => m.DEPOT_ID == depot_id).Where(m => m.CHARGE == "sales");
            return View(employees.ToList());
        }

        //
        // GET: /Employees/Details/5

        public ViewResult Details(int id)
        {
            EMPLOYEE employee = db.EMPLOYEES.Single(e => e.ID == id);
            return View(employee);
        }

        //
        // GET: /Employees/Create

        public ActionResult Create()
        {
            int depot_id = Convert.ToInt32(Session["depot_id"]);
            var sales = new EMPLOYEE
            {
                DEPOT_ID = depot_id,
                CHARGE = "sales"
            };

            // ViewBag.DEPOT_ID = new SelectList(db.DEPOTS, "ID", "NAME");
            return View(sales);
        } 

        //
        // POST: /Employees/Create

        [HttpPost]
        public ActionResult Create(EMPLOYEE employee)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEES.AddObject(employee);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DEPOT_ID = new SelectList(db.DEPOTS, "ID", "NAME", employee.DEPOT_ID);
            return View(employee);
        }
        
        //
        // GET: /Employees/Edit/5
 
        public ActionResult Edit(int id)
        {
            EMPLOYEE employee = db.EMPLOYEES.Single(e => e.ID == id);
            ViewBag.DEPOT_ID = new SelectList(db.DEPOTS, "ID", "NAME", employee.DEPOT_ID);
            return View(employee);
        }

        //
        // POST: /Employees/Edit/5

        [HttpPost]
        public ActionResult Edit(EMPLOYEE employee)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEES.Attach(employee);
                db.ObjectStateManager.ChangeObjectState(employee, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEPOT_ID = new SelectList(db.DEPOTS, "ID", "NAME", employee.DEPOT_ID);
            return View(employee);
        }

        //
        // GET: /Employees/Delete/5
 
        public ActionResult Delete(int id)
        {
            EMPLOYEE employee = db.EMPLOYEES.Single(e => e.ID == id);
            return View(employee);
        }

        //
        // POST: /Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            EMPLOYEE employee = db.EMPLOYEES.Single(e => e.ID == id);
            db.EMPLOYEES.DeleteObject(employee);
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