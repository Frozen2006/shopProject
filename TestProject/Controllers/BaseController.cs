using System.Net;
using Ninject;
using System.Web.Mvc;
using iTechArt.Shop.Common.Services;

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
            return (string)HttpContext.Items["email"];
        }

        /// <summary>
        /// Get JSON-object report.
        /// </summary>
        /// <param name="report">Report text</param>
        /// <returns>{ Report: "report" }</returns>
        public JsonResult JsonReport(string report)
        {
            return Json(new { Report = report });
        }

        /// <summary>
        /// Get JSON-object report and set response code.
        /// </summary>
        /// <param name="report">Report text</param>
        /// <param name="code">Response code</param>
        /// <returns>{ Report: "report" }</returns>
        public JsonResult JsonReport(string report, HttpStatusCode code)
        {
            Response.StatusCode = (int)code;
            return JsonReport(report);
        }
    }
}