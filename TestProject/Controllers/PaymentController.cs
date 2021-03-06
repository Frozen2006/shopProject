﻿using System;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Services;
using System.Web.Mvc;
using Ninject;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Filters;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers 
{
    [CustomAuthrize]
    public class PaymentController : BaseController
    {
        private IOrderService OrderService { get; set; }

        [Inject]
        public PaymentController(ICategoryService categoryService, IUserService userService, ICartService cartService, IOrderService orderService)
            : base(categoryService, userService, cartService)
        {
            OrderService = orderService;
        }

        [HttpGet]
        public ActionResult Pay(int orderId)
        {
            OrdersDetails order = OrderService.GetOrderForPayment(orderId);
            if (order == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotFound });
            }

            if (order.OrderStatus != OrderStatus.NotPaid)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.Forbidden });
            }

            var model = new PaymentModel()
                {
                    FinalPrice = order.PriceWithDiscount,
                    OrderId = order.Id,
                    Price = order.TotalPrice
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                OrdersDetails order = OrderService.GetOrderForPayment(model.OrderId);
                if (order == null)
                {
                    return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotFound });
                }

                //TODO: bank service with profit logging

                try
                {
                    OrderService.UpdateOrder(order.Id, OrderStatus.Paid);
                }
                catch (Exception)
                {
                    return RedirectToAction("Custom", "Error", new { Message = "Can't update an order status" });
                }

                return RedirectToAction("Index", "History");
            }

            return View(model);
        }

    }
}