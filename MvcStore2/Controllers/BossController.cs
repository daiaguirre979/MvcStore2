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
    public class BossController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Boss/

        public ViewResult Index()
        {
            var employees = db.EMPLOYEES.Include("DEPOT");
            return View(employees.ToList());
        }

        //
        // GET: /Boss/Details/5

        public ViewResult Details(int id)
        {
            EMPLOYEE employee = db.EMPLOYEES.Single(e => e.ID == id);
            return View(employee);
        }

        //
        // GET: /Boss/Create

        public ActionResult Create()
        {
            ViewBag.DEPOT_ID = new SelectList(db.DEPOTS, "ID", "NAME");

            int depot_id = Convert.ToInt32(Session["depot_id"]);

            var boss = new EMPLOYEE
            {
                DEPOT_ID = depot_id,
                CHARGE = "boss"
            };


            return View(boss);
        } 

        //
        // POST: /Boss/Create

        [HttpPost]
        public ActionResult Create(EMPLOYEE employee)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEES.AddObject(employee);
                db.SaveChanges();
                //return RedirectPermanent("/");
                return RedirectToActionPermanent("Details", "Depots", new { id = Session["depot_id"] });
            }

            //ViewBag.DEPOT_ID = new SelectList(db.DEPOTS, "ID", "NAME", employee.DEPOT_ID);
            return View(employee);
        }
        
        //
        // GET: /Boss/Edit/5
 
        public ActionResult Edit(int id)
        {
            EMPLOYEE employee = db.EMPLOYEES.Single(e => e.ID == id);
            ViewBag.DEPOT_ID = new SelectList(db.DEPOTS, "ID", "NAME", employee.DEPOT_ID);
            return View(employee);
        }

        //
        // POST: /Boss/Edit/5

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
        // GET: /Boss/Delete/5
 
        public ActionResult Delete(int id)
        {
            EMPLOYEE employee = db.EMPLOYEES.Single(e => e.ID == id);
            return View(employee);
        }

        //
        // POST: /Boss/Delete/5

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