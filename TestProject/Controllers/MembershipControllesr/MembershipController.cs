using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using Ninject;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers .MembershipControllesr
{
    public class MembershipController : BaseController
    {

        private IZipCodeService ZipCodeService { get; set; }

        [Inject]
        public MembershipController(ICategoryService categoryService, IUserService userService, ICartService cartService, IZipCodeService zipCodeService)
            : base(categoryService, userService, cartService)
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
                    UserService.LogIn(model.Email, model.Password);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Email is already exist, or input data is wrong!");
            ViewBag.Data = "BAD EMAIL";
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

            ModelState.AddModelError("", "Bad user name or password");
            ViewBag.Data = "Bad user name or password";
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
