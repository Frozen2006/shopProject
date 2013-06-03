using BLL;
using BLL.membership;
using Entities;
using Helpers;
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
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
            }

            OrdersDetails order = _orderService.GetOrderDetails(orderId);

            if (order == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotFound });
            }

            if (order.userEmail != email)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.Forbidden });
            }

            if (order.OrderStatus == OrderStatus.Paid)
            {
                return RedirectToAction("CustomError", "Error", new { message = "The order has already been paid" });
            }

            ViewBag.OrderId = orderId;
            ViewBag.Price = order.TotalPrice;
            ViewBag.FinalPrice = order.PriceWithDiscount;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                string email = _userService.GetEmailIfLoginIn();
                if (email == null)
                {
                    return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
                }

                OrdersDetails order = _orderService.GetOrderDetails(model.OrderId);

                if (order.userEmail != email)
                {
                    return RedirectToAction("Error", "Error", new { Code = ErrorCode.Forbidden });
                }

                if (order.OrderStatus == OrderStatus.Paid)
                {
                    return RedirectToAction("CustomError", "Error", new { message = "The order has already been paid" });
                }

                return RedirectToAction("Index", "History");
            }

            return View(model);
        }

    }
}