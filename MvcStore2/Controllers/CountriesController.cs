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
    public class CountriesController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Countries/

        public ViewResult Index()
        {
            var countries = db.COUNTRIES.Include("REGION");
            return View(countries.ToList());
        }

        //
        // GET: /Countries/Details/5

        public ViewResult Details(int id)
        {
            COUNTRY country = db.COUNTRIES.Single(c => c.ID == id);
            return View(country);
        }

        //
        // GET: /Countries/Create

        public ActionResult Create()
        {
            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME");
            return View();
        } 

        //
        // POST: /Countries/Create

        [HttpPost]
        public ActionResult Create(COUNTRY country)
        {
            if (ModelState.IsValid)
            {
                db.COUNTRIES.AddObject(country);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME", country.REGION_ID);
            return View(country);
        }
        
        //
        // GET: /Countries/Edit/5
 
        public ActionResult Edit(int id)
        {
            COUNTRY country = db.COUNTRIES.Single(c => c.ID == id);
            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME", country.REGION_ID);
            return View(country);
        }

        //
        // POST: /Countries/Edit/5

        [HttpPost]
        public ActionResult Edit(COUNTRY country)
        {
            if (ModelState.IsValid)
            {
                db.COUNTRIES.Attach(country);
                db.ObjectStateManager.ChangeObjectState(country, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.REGION_ID = new SelectList(db.REGIONS, "ID", "NAME", country.REGION_ID);
            return View(country);
        }

        //
        // GET: /Countries/Delete/5
 
        public ActionResult Delete(int id)
        {
            COUNTRY country = db.COUNTRIES.Single(c => c.ID == id);
            return View(country);
        }

        //
        // POST: /Countries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            COUNTRY country = db.COUNTRIES.Single(c => c.ID == id);
            db.COUNTRIES.DeleteObject(country);
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