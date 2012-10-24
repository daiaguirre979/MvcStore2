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
    public class RegionsController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Regions/

        public ViewResult Index()
        {
            return View(db.REGIONS.ToList());
        }

        //
        // GET: /Regions/Details/5

        public ViewResult Details(int id)
        {
            REGION region = db.REGIONS.Single(r => r.ID == id);
            return View(region);
        }

        //
        // GET: /Regions/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Regions/Create

        [HttpPost]
        public ActionResult Create(REGION region)
        {
            if (ModelState.IsValid)
            {
                db.REGIONS.AddObject(region);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(region);
        }
        
        //
        // GET: /Regions/Edit/5
 
        public ActionResult Edit(int id)
        {
            REGION region = db.REGIONS.Single(r => r.ID == id);
            return View(region);
        }

        //
        // POST: /Regions/Edit/5

        [HttpPost]
        public ActionResult Edit(REGION region)
        {
            if (ModelState.IsValid)
            {
                db.REGIONS.Attach(region);
                db.ObjectStateManager.ChangeObjectState(region, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        //
        // GET: /Regions/Delete/5
 
        public ActionResult Delete(int id)
        {
            REGION region = db.REGIONS.Single(r => r.ID == id);
            return View(region);
        }

        //
        // POST: /Regions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            REGION region = db.REGIONS.Single(r => r.ID == id);
            db.REGIONS.DeleteObject(region);
            
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