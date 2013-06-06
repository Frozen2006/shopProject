using Interfaces;
using Ninject;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class BaseController : Controller
    {
        protected  ICategoryService ProdService { get; set; }
        protected  IUserService UserService{ get; set; }
        protected  ICartService CartService { get; set; }

        [Inject]
        public BaseController(ICategoryService productService, IUserService userService, ICartService cartService)
        {
            ProdService = productService;
            UserService = userService;
            CartService = cartService;
        }

        protected string GetUserEmail()
        {
            return (string)HttpContext.Items["email"];
        }
    }
}