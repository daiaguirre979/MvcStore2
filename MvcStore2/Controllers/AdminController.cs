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
    public class AdminController : Controller
    {
        private EntityStore db = new EntityStore();

        //
        // GET: /Admin/

        public ViewResult Index()
        {
            return View(db.USERS.ToList());
        }

        //
        // GET: /Admin/Details/5

        public ViewResult Details(int id)
        {
            USER user = db.USERS.Single(u => u.ID == id);
            return View(user);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(USER user)
        {
            if (ModelState.IsValid)
            {
                db.USERS.AddObject(user);
                db.SaveChanges();
                return RedirectToActionPermanent("Admin", "Home") ;  
            }

            return View(user);
        }
        
        //
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            USER user = db.USERS.Single(u => u.ID == id);
            return View(user);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(USER user)
        {
            if (ModelState.IsValid)
            {
                db.USERS.Attach(user);
                db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
            USER user = db.USERS.Single(u => u.ID == id);
            return View(user);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            USER user = db.USERS.Single(u => u.ID == id);
            db.USERS.DeleteObject(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult LogOnAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOnAdmin(USER model)
        {
            if (ModelState.IsValid)
            {

                var user = db.USERS.Where(c => c.NAME == model.NAME).Where(c => c.PASSWORD == model.PASSWORD).FirstOrDefault();
                if (user != null)
                {
                    Session["user"] = user;
                    Session["user_name"] = user.NAME;
                    //Session["user_id"] = user.ID;

                    return RedirectToAction("Admin", "Home");
                    
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View(model);
        }
    }
}