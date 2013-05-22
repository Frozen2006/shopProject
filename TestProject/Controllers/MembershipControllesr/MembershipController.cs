using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.membership;
using Ninject;

namespace TestProject.Controllers.MembershipControllesr
{
    public class MembershipController : Controller
    {
        //
        // GET: /Membership/

        [Inject]
        public MembershipController(UsersService uservice, Interfaces.IZipCode zip)
        {
            us = uservice;
            _myZip = zip;
        }

        private readonly UsersService us;
        private Interfaces.IZipCode _myZip;

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (us.CeateUser(model.Email, model.Password, model.Title, model.firstName, model.lastName, model.Address1, model.Address2, model.Phone1, model.Phone2, model.Zip, model.City))
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "BAAAAAAAAAAAAAAAAAAD");
            ViewBag.Data = "BAD";
            return View(model);
        }




        //Ajax jquery responce method
        [HttpGet]
        public ActionResult RegisterRow(string q)
        {
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (q.Length >= 3))
            {
                //string outData = _myZip.GetFirstCityPartical(q);
                List<string> dataList = _myZip.GetCities(q);

                //var data = new { name = outData };
                return Json(dataList);
            }
            return View("Register");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TestProject.Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (us.LogIn(model.Email, model.Password))
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Somthing is wrong");
            ViewBag.Data = "Bad user name or pass";
            return View(model);
        }

        public ActionResult LogOut()
        {
            us.LogOut(HttpContext.Request.Cookies.Get("session_data").Value);
            return RedirectToAction("Index", "Home");
        }

    }
}
