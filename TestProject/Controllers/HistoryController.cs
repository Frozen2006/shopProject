﻿using Helpers;
using iTechArt.Shop.Common.Services;
using System.Web.Mvc;
using Ninject;
using iTechArt.Shop.Web.Common;
using iTechArt.Shop.Web.Filters;

namespace iTechArt.Shop.Web.Controllers 
{
    [CustomAuthrize]
    public class HistoryController : BaseController
    {
        private IOrderService OrderService { get; set; }

        [Inject]
        public HistoryController(ICategoryService productService, IUserService userService, ICartService cartService, IOrderService orderService)
            : base(productService, userService, cartService)
        {
            OrderService = orderService;
        }

        public ActionResult Index()
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
            }

            var orders = OrderService.GetUserOrders(email);
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            OrdersDetails order = OrderService.GetOrderDetails(id);

            return View(order);
        }

    }
}
