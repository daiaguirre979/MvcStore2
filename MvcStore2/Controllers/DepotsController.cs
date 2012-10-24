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
    public class DepotsController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Depots/

        public ViewResult Index()
        {
            var depots = db.DEPOTS.Include("REGION");
            return View(depots.ToList());
        }

        //
        // GET: /Depots/Details/5

        public ViewResult Details(int id)
        {
            DEPOT depot = db.DEPOTS.Single(d => d.ID == id);

            var boss = db.EMPLOYEES.Where(m => m.DEPOT_ID == depot.ID).FirstOrDefault();

            Session["depot_id"] = depot.ID;
            ViewBag.boss = boss;

            return View(depot);
        }

        //
        // GET: /Depots/Create

        public ActionResult Create()
        {
            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME");
            return View();
        } 

        //
        // POST: /Depots/Create

        [HttpPost]
        public ActionResult Create(DEPOT depot)
        {
            if (ModelState.IsValid)
            {
                db.DEPOTS.AddObject(depot);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME", depot.REGION_ID);
            return View(depot);
        }
        
        //
        // GET: /Depots/Edit/5
 
        public ActionResult Edit(int id)
        {
            DEPOT depot = db.DEPOTS.Single(d => d.ID == id);
            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME", depot.REGION_ID);
            return View(depot);
        }

        //
        // POST: /Depots/Edit/5

        [HttpPost]
        public ActionResult Edit(DEPOT depot)
        {
            if (ModelState.IsValid)
            {
                db.DEPOTS.Attach(depot);
                db.ObjectStateManager.ChangeObjectState(depot, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME", depot.REGION_ID);
            return View(depot);
        }

        //
        // GET: /Depots/Delete/5
 
        public ActionResult Delete(int id)
        {
            DEPOT depot = db.DEPOTS.Single(d => d.ID == id);
            return View(depot);
        }

        //
        // POST: /Depots/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            DEPOT depot = db.DEPOTS.Single(d => d.ID == id);
            db.DEPOTS.DeleteObject(depot);
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