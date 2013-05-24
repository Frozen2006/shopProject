using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.membership;
using Ninject;

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
