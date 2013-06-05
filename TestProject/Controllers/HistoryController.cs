using BLL;
using BLL.membership;
using Helpers;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Filters;

namespace TestProject.Controllers
{
    [CustomAuthrize]
    public class HistoryController : BaseController
    {
        private readonly  IOrderService _orderService;

        public HistoryController(ICategoryService ps, IUserService us, ICart cs, IOrderService os)
            : base(ps, us, cs)
        {
            _orderService = os;
        }

        public ActionResult Index()
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
            }

            var orders = _orderService.GetUserOrders(email);
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            OrdersDetails order = _orderService.GetOrderDetails(id);

            return View(order);
        }

    }
}
