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
    public class SuppliersController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Suppliers/

        public ViewResult Index()
        {
            return View(db.SUPPLIERS.ToList());
        }

        //
        // GET: /Suppliers/Details/5

        public ViewResult Details(int id)
        {
            SUPPLIER supplier = db.SUPPLIERS.Single(s => s.ID == id);
            return View(supplier);
        }

        //
        // GET: /Suppliers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Suppliers/Create

        [HttpPost]
        public ActionResult Create(SUPPLIER supplier)
        {
            if (ModelState.IsValid)
            {
                db.SUPPLIERS.AddObject(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(supplier);
        }
        
        //
        // GET: /Suppliers/Edit/5
 
        public ActionResult Edit(int id)
        {
            SUPPLIER supplier = db.SUPPLIERS.Single(s => s.ID == id);
            return View(supplier);
        }

        //
        // POST: /Suppliers/Edit/5

        [HttpPost]
        public ActionResult Edit(SUPPLIER supplier)
        {
            if (ModelState.IsValid)
            {
                db.SUPPLIERS.Attach(supplier);
                db.ObjectStateManager.ChangeObjectState(supplier, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        //
        // GET: /Suppliers/Delete/5
 
        public ActionResult Delete(int id)
        {
            SUPPLIER supplier = db.SUPPLIERS.Single(s => s.ID == id);
            return View(supplier);
        }

        //
        // POST: /Suppliers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SUPPLIER supplier = db.SUPPLIERS.Single(s => s.ID == id);
            db.SUPPLIERS.DeleteObject(supplier);
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