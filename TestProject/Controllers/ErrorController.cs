using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Unknown()
        {
            return View("Error");
        }
        public ViewResult NoAccess()
        {
            //Response.StatusCode = 404;  //you may want to set this to 200
            return View("NoAccess");
        }
        public ViewResult NotFound()
        {
            return View("NotFound");
        }
    }
}
