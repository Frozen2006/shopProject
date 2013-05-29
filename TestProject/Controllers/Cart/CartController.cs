﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.membership;
using Interfaces;
using Ninject;
using TestProject.Models;

namespace TestProject.Controllers.Cart
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        private readonly ICart _cart;
        private readonly UsersService _usersService;

        [Inject]
        public CartController(ICart cartInit, UsersService us)
        {
            _cart = cartInit;
            _usersService = us;
        }


        [Filters.CustomAuthrize(Roles = "User")]
        public ActionResult Index()
        {

            string sessionId = Request.Cookies.Get("session_data").Value;
            string userEmail = _usersService.GetUserEmailFromSession(sessionId);

            CartViewModel model = new CartViewModel();
            model.Products = _cart.GetAllChart(userEmail);
            model.TotalPrice = _cart.GetTotalPrice(userEmail);


            return View(model);
        }

        //Ajax jquery responce method
        [HttpPost]
        public ActionResult Remove(string productId)
        {
            if (productId == null)
                return null;
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (productId.Length > 0))
            {
                string sessionId = Request.Cookies.Get("session_data").Value;
                string userEmail = _usersService.GetUserEmailFromSession(sessionId);

                _cart.DeleteProduct(userEmail, Convert.ToInt32(productId));

                return Content(Convert.ToString(_cart.GetTotalPrice(userEmail)));
            }
            return View("Index");
        }

        private static object _locker = new object();

        

        //Ajax jquery responce method
        [HttpPost]
        public ActionResult SetNewValue(string productId, int count)
        {
            if (productId == null)
                return null;
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (productId.Length > 0))
            {
                string sessionId = Request.Cookies.Get("session_data").Value;
                string userEmail = _usersService.GetUserEmailFromSession(sessionId);

                double newPositionTotalPrice;
                lock (_locker)
                {
                    newPositionTotalPrice = _cart.UpateCount(userEmail, Convert.ToInt32(productId), count);
                }

                var outData =
                    new
                        {
                            positionPrice = Convert.ToString(newPositionTotalPrice),
                            totalPrice = Convert.ToString(_cart.GetTotalPrice(userEmail))
                        };


                return Json(outData);
            }
            return View("Index");
        }


        public class jsonUpdateAll
        {
            public string Id;
            public string positionPrice;
            public string totalPrice;
            public string count;
        }

        //Ajax jquery responce method
        [HttpPost]
        public ActionResult SetNewValueToAll(List<int> Id, List<int> Count)
        {
           if (Id == null)
                return null;
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (Id.Count > 0))
            {
                string sessionId = Request.Cookies.Get("session_data").Value;
                string userEmail = _usersService.GetUserEmailFromSession(sessionId);

                List<jsonUpdateAll> outData = new List<jsonUpdateAll>();

                for (int q = 0; q < Id.Count; q++)
                {
                    lock (_locker)
                    {
                        double newPositionTotalPrice = _cart.UpateCount(userEmail, Convert.ToInt32(Id[q]), Count[q]);
                        outData.Add(new jsonUpdateAll { Id = Convert.ToString(Id[q]), positionPrice = Convert.ToString(newPositionTotalPrice), count = Convert.ToString(Count[q])});
                    }
                }

                string totalPrice = Convert.ToString(_cart.GetTotalPrice(userEmail));

                foreach (var jsonUpdateAll in outData)
                {
                    jsonUpdateAll.totalPrice = totalPrice;
                }
                /*var outData =
                    new
                    {
                        positionPrice = Convert.ToString(newPositionTotalPrice),
                        totalPrice = Convert.ToString(_cart.GetTotalPrice(userEmail))
                    };*/


                return Json(outData);
            }
            return View("Index");
        }
    }
}
