using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Entities;
using Helpers;
using Interfaces;
using Ninject;
using TestProject.Models;

namespace TestProject.Controllers.MembershipControllesr
{
    public class MembershipController : BaseController
    {

        private IZipCodeService ZipCodeService { get; set; }

        [Inject]
        public MembershipController(ICategoryService productService, IUserService userService, ICartService cartService, IZipCodeService zipCodeService)
            : base(productService, userService, cartService)
        {
            ZipCodeService = zipCodeService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User us = Mapper.Map<RegisterModel, User>(model);
                us.Role = (int) RolesType.User;

                if (UserService.CeateUser(us))
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "BAAAAAAAAAAAAAAAAAAD");
            ViewBag.Data = "BAD";
            return View(model);
        }


        //Ajax jquery responce method
        [HttpPost]
        public ActionResult RegisterRow(string term)
        {
            if (term == null)
                return null;
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (term.Length > 0))
            {
                List<Tuple<string,string>> dataList = ZipCodeService.GetCities(term);

                var jsonOutZips = dataList.Select(dL => new jsonOutData {label = dL.Item1 + " " + dL.Item2, value = dL.Item1, city = dL.Item2}).ToList();

                return Json(jsonOutZips);
            }
            return View("Register");
        }


        private struct jsonOutData
        {
            public string label;
            public string value;
            public string city;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserService.LogIn(model.Email, model.Password))
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Bad user name or pass");
            ViewBag.Data = "Bad user name or pass";
            return View(model);
        }

        public ActionResult LogOut()
        {
            var httpCookie = HttpContext.Request.Cookies.Get("session_data");
            if (httpCookie != null)
                UserService.LogOut(httpCookie.Value);
            return RedirectToAction("Index", "Home");
        }
    }
}
