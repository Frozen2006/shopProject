﻿using System.Web.Mvc;
using Helpers;
using Interfaces;
using Ninject;
using TestProject.Filters;
using TestProject.Models;

namespace TestProject.Controllers
{
    [CustomAuthrize(Roles = RolesType.User)]
    public class SettingsController : BaseController
    {
        [Inject]
        public SettingsController(ICategoryService productService, IUserService userService, ICartService cartService)
            : base(productService, userService, cartService) { }

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
            var model = new ChangeDeliveryAddressModel();

            string userEmail = GetUserEmail();

            UserDetails userDetails = UserService.GetUserDetails(userEmail);

            model.Address1 = userDetails.address;
            model.Address2 = userDetails.address2;
            model.Phone1 = userDetails.phone;
            model.Phone2 = userDetails.phone2;
            model.Zip = userDetails.zip;
            model.City = userDetails.city;

            return View(model);
        }


        [HttpPost]
        public ActionResult Delivery(ChangeDeliveryAddressModel model)
        {
            if (ModelState.IsValid)
            {
                string userEmail = GetUserEmail();

               UserService.ChangeDeliveryData(userEmail, model.Address1, model.Address2, model.Phone1, model.Phone2, model.Zip, model.City);
            }
            ModelState.AddModelError("", "Input data is bad");
            ViewBag.Data = "BAD";
            return View(model);
        }


        [HttpPost]
        public ActionResult Account(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string userEmail = GetUserEmail();

                if (UserService.CheckUser(userEmail, model.OldPassword))
                {
                    UserService.ChangePassword(userEmail, model.OldPassword, model.Password);
                    ModelState.AddModelError("", "Success!");
                    return View(new ChangePasswordModel());
                }
                ModelState.AddModelError("", "Wrong old password.");
            }
            ModelState.AddModelError("", "Input data is bad");
            ViewBag.Data = "BAD";
            return View(model);
        }

    }
}
