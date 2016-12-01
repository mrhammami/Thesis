using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thesis.Models;

namespace Thesis.Controllers
{
    public class HomeController : Controller
    {

        static Entities Entities = new Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Rólunk";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Írjon nekünk!";

            return View();
        }
        
    }
}