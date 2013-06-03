using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public ActionResult Pay()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                //payment to database
                //redirece to somewhere
            }
            return View(model);
        }

    }
}