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
    public class ProductsController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Products/

        public ViewResult Index()
        {
            var products = db.PRODUCTS.Include("SUPPLIER");
            return View(products.ToList());
        }

        //
        // GET: /Products/Details/5

        public ViewResult Details(int id)
        {
            PRODUCT product = db.PRODUCTS.Single(p => p.ID == id);
            return View(product);
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERS, "ID", "NAME");
            return View();
        } 

        //
        // POST: /Products/Create

        [HttpPost]
        public ActionResult Create(PRODUCT product)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTS.AddObject(product);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERS, "ID", "NAME", product.SUPPLIER_ID);
            return View(product);
        }
        
        //
        // GET: /Products/Edit/5
 
        public ActionResult Edit(int id)
        {
            PRODUCT product = db.PRODUCTS.Single(p => p.ID == id);
            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERS, "ID", "NAME", product.SUPPLIER_ID);
            return View(product);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(PRODUCT product)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTS.Attach(product);
                db.ObjectStateManager.ChangeObjectState(product, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERS, "ID", "NAME", product.SUPPLIER_ID);
            return View(product);
        }

        //
        // GET: /Products/Delete/5
 
        public ActionResult Delete(int id)
        {
            PRODUCT product = db.PRODUCTS.Single(p => p.ID == id);
            return View(product);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            PRODUCT product = db.PRODUCTS.Single(p => p.ID == id);
            db.PRODUCTS.DeleteObject(product);
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