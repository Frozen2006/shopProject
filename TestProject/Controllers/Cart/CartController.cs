using System;
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

                double newPositionTotalPrice = _cart.UpateCount(userEmail, Convert.ToInt32(productId), count);

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


    }
}
