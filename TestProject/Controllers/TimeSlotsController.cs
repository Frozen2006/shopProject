using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    [Filters.CustomAuthrize(Roles = "User")]
    public class TimeSlotsController : Controller
    {
        //
        // GET: /TimeSlots/

        public ActionResult Index()
        {
            return View();
        }

    }
}
