using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStore2.Models;

namespace MvcStore2.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        EntityStore db = new EntityStore();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sales()
        {
            int user_id = Convert.ToInt32(Session["user_id"]);
            var user = db.EMPLOYEES.Include("DEPOT").Single(d => d.ID == user_id);
            Session["depot_id"] = user.DEPOT_ID;
            Session["user_name"] = user.NAME;

            var order = new ORDER {
                ELABORATION_DATE = DateTime.Now,
                EMPLOYEE_ID = user_id,
                STATE = 1
            };

            ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME");
            //ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEES, "ID", "LOGIN");
            

            return View(order);
        }
        
        public ActionResult Depot()
        {
            int user_id = Convert.ToInt32(Session["user_id"]);
            var user = db.EMPLOYEES.Include("DEPOT").Single(d => d.ID == user_id);
            Session["depot_id"] = user.DEPOT_ID;
            Session["depot_name"] = user.DEPOT.NAME;
            return View();
        }
        
        public ActionResult Admin()
        {

            return RedirectToActionPermanent("Index", "Depots");
        }
    }
}
