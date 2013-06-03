using BLL;
using BLL.membership;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Filters;
using TestProject.Models;

namespace TestProject.Controllers
{
    [CustomAuthrize]
    public class PaymentController : BaseController
    {
        private readonly OrderService _orderService;

        public PaymentController(CategoryService ps, UsersService us, ICart cs, OrderService os)
            : base(ps, us, cs)
        {
            _orderService = os;
        }

        [HttpGet]
        public ActionResult Pay(int orderId)
        {
            string email = _userService.GetEmailIfLoginIn();
            if (email == null)
            {
                return RedirectToAction("Error", "ErrorController", new { Code = ErrorCode.NotLoggedIn });
            }

            //Get order by ID and check it whether it belongs to current user
            //and it's status is "waiting for payment"


            //var order = _userService.GetOrder(orderId)
            //if (order.Status == "WaitsForPayment" 
            //&& order.User == CURRENT_USER)
            //{            //
            //  order.status = "Payed"
            //}
            ViewBag.OrderId = orderId;
            ViewBag.Price = _cartService.GetTotalPrice(email);

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