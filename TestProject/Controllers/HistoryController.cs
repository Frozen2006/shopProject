using BLL;
using BLL.membership;
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
        private readonly  OrderService _orderService;

        public HistoryController(CategoryService ps, UsersService us, ICart cs, OrderService os)
            : base(ps, us, cs)
        {
            _orderService = os;
        }

        public ActionResult Index()
        {
            var email = _userService.GetEmailIfLoginIn();

            if (email == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
            }

            var orders = _orderService.GetUserOrders(email);
            return View(orders);
        }

    }
}
