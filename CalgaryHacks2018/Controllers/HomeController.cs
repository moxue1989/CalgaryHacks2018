using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalgaryHacks2018.Controllers
{
    public class HomeController : Controller
    {
        public static string PASSWORD = "admin";
        public static string TWOFAPASS = null;

        public ActionResult Index()
        {
            if (TWOFAPASS == null)
            {
                Random random = new Random();
                TWOFAPASS = random.Next(0, 1000) + "";
                EmailSender.SendEmail("mo_xue1989@yahoo.ca", "Binary-Bot Two-Factor Authentication", "Your verification code is: " + TWOFAPASS);
            }
            return View();
        }

        public ActionResult Login(string password, string twofatoken)
        {
            if (password == PASSWORD && twofatoken == TWOFAPASS)
            {
                TWOFAPASS = null;
                HttpContext.Session["user"] = "admin";
                return RedirectToAction("Control", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Control()
        {          
            if(HttpContext.Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}