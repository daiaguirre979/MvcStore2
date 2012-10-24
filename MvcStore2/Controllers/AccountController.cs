using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStore2.Models;

namespace MvcStore2.Controllers
{
    public class AccountController : Controller
    {
        private EntityStore db = new EntityStore();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LogOn()
        {
            var admin = db.USERS.FirstOrDefault();
            if (admin == null) {
                return RedirectToAction("Create", "Admin");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(EMPLOYEE model)
        {
            if (ModelState.IsValid)
            {

                var user = db.EMPLOYEES.Where(c => c.LOGIN == model.LOGIN).Where(c => c.PASSWORD == model.PASSWORD).FirstOrDefault();
                if (user != null)
                {
                    Session["user"] = user;
                    Session["user_name"] = user.NAME;
                    
                    Session["user_id"] = user.ID;

                    if(user.CHARGE == "admin"){
                        
                    }
                    else if (user.CHARGE == "sales")
                    {
                        return RedirectToAction("Sales", "Home");
                    }
                    else {
                        return RedirectToAction("Depot", "Home");
                    }
                    
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View(model);
        }

        public ActionResult LogUp()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            Session["UserName"] = null;

            return RedirectToAction("LogOn", "Account");
        }

    }
}
