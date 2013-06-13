using System.Net;
using Ninject;
using System.Web.Mvc;
using iTechArt.Shop.Common.Services;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Web.Controllers 
{
    /// <summary>
    /// Base class for controllers. Contains main common used services.
    /// </summary>
    public class BaseController : Controller
    {
        protected ICategoryService CategoryService { get; set; }
        protected  IUserService UserService{ get; set; }
        protected  ICartService CartService { get; set; }

        [Inject]
        public BaseController(ICategoryService categoryService, IUserService userService, ICartService cartService)
        {
            CategoryService = categoryService;
            UserService = userService;
            CartService = cartService;
        }

        protected string GetUserEmail()
        {
			var user = (User)HttpContext.Items["userData"];

            return user.Email;
        }

        public JsonResult JsonReport(string report, bool withError)
        {
            return Json(new {Report = report, WithError = withError});
        }
    }
}