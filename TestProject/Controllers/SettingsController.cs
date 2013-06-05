using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.membership;
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
        //
        // GET: /Settings/


        public SettingsController(ICategoryService ps, IUserService us, ICart cs) : base(ps, us, cs)
        {}

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

            string userEmail = GetUserEmail();

            UserDetails userDetails = _userService.GetUserDetails(userEmail);

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
                string userEmail = GetUserEmail();

               _userService.ChangeDeliveryData(userEmail, model.Address1, model.Address2, model.Phone1, model.Phone2, model.Zip, model.City);
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
                string userEmail = GetUserEmail();

                if (_userService.CheckUser(userEmail, model.OldPassword))
                {
                    _userService.ChangePassword(userEmail, model.OldPassword, model.Password);
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
