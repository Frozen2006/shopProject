using System;
using System.Web.Mvc;

namespace iTechArt.Shop.Web.Controllers 
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}