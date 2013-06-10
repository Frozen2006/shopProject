using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Services;
using System.Web.Mvc;
using Ninject;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Filters;

namespace iTechArt.Shop.Web.Controllers 
{
    [CustomAuthrize]
    public class HistoryController : BaseController
    {
        private IOrderService OrderService { get; set; }

        [Inject]
        public HistoryController(ICategoryService categoryService, IUserService userService, ICartService cartService, IOrderService orderService)
            : base(categoryService, userService, cartService)
        {
            OrderService = orderService;
        }

        public ActionResult Index()
        {
            var email = UserService.GetEmailIfLoginIn();
            if (email == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotLoggedIn });
            }
            
            var orders = OrderService.GetUserOrders(email);
            if (orders == null)
            {
                return RedirectToAction("Error", "Error", new {Code = ErrorCode.NotFound});
            }

            return View(orders);
        }

        public ActionResult Details(int id)
        {
            OrdersDetails order = OrderService.GetOrderDetails(id);
            if (order == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotFound });
            }

            return View(order);
        }

    }
}
