using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.membership;
using Helpers;
using Ninject;
using TestProject.Models;

namespace TestProject.Controllers
{
    [Filters.CustomAuthrize(Roles = "User")]
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/
         [Inject]
        public SettingsController(UsersService uservice)
        {
            us = uservice;
        }

        private readonly UsersService us;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Delivery()
        {
            Models.ChangeDeliveryAddressModel model = new ChangeDeliveryAddressModel();

           string sessionId = Request.Cookies.Get("session_data").Value;
           string userEmail = us.GetUserEmailFromSession(sessionId);

            UserDetails userDetails = us.GetUserDetails(userEmail);

            model.Address1 = userDetails.address;
            model.Address2 = userDetails.address2;
            model.Phone1 = userDetails.phone;
            model.Phone2 = userDetails.phone2;
            model.Zip = userDetails.zip;
            model.City = userDetails.city;


            return View(model);
        }


        [HttpPost]
        public ActionResult Delivery(Models.ChangeDeliveryAddressModel model)
        {
            if (ModelState.IsValid)
            {
                string sessionId = Request.Cookies.Get("session_data").Value;
                string userEmail = us.GetUserEmailFromSession(sessionId);

               us.ChangeDeliveryData(userEmail, model.Address1, model.Address2, model.Phone1, model.Phone2, model.Zip, model.City);
            }
            ModelState.AddModelError("", "Input data is bad");
            ViewBag.Data = "BAD";
            return View(model);
        }


        [HttpPost]
        public ActionResult Account(Models.ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string sessionId = Request.Cookies.Get("session_data").Value;
                string userEmail = us.GetUserEmailFromSession(sessionId);

                if (us.CheckUser(userEmail, model.OldPassword))
                {
                    us.ChangePassword(userEmail, model.OldPassword, model.Password);
                    ModelState.AddModelError("", "Success!");
                    return View(new Models.ChangePasswordModel{});
                }
                else
                {
                    ModelState.AddModelError("", "Wrong old password.");
                }
            }
            ModelState.AddModelError("", "Input data is bad");
            ViewBag.Data = "BAD";
            return View(model);
        }

    }
}
