using Helpers;
using Interfaces;
using System.Web.Mvc;
using Ninject;
using TestProject.Filters;
using TestProject.Models;

namespace TestProject.Controllers
{
    [CustomAuthrize]
    public class PaymentController : BaseController
    {
        private IOrderService OrderService { get; set; }

        [Inject]
        public PaymentController(ICategoryService productService, IUserService userService, ICartService cartService, IOrderService orderService)
            : base(productService, userService, cartService)
        {
            OrderService = orderService;
        }

        [HttpGet]
        public ActionResult Pay(int orderId)
        {
            string email = GetUserEmail();
            if (email == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
            }

            OrdersDetails order = OrderService.GetOrderDetails(orderId);

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
                string email = GetUserEmail();
                if (email == null)
                {
                    return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
                }

                OrdersDetails order = OrderService.GetOrderDetails(model.OrderId);

                if (order.userEmail != email)
                {
                    return RedirectToAction("Error", "Error", new { Code = ErrorCode.Forbidden });
                }

                if (order.OrderStatus == OrderStatus.Paid)
                {                    
                    return RedirectToAction("CustomError", "Error", new { message = "The order has already been paid" });
                }

                OrderService.UpdateOrder(model.OrderId, OrderStatus.Paid);

                return RedirectToAction("Index", "History");
            }

            return View(model);
        }

    }
}