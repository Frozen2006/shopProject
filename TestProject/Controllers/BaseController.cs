using Interfaces;
using Ninject;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ICategoryService _prodService;
        protected readonly IUserService _userService;
        protected readonly ICart _cartService;


        [Inject]
        public BaseController(ICategoryService ps, IUserService us, ICart cs)
        {
            _prodService = ps;
            _userService = us;
            _cartService = cs;
        }

        protected string GetUserEmail()
        {
            return (string)HttpContext.Items["email"];
        }

    }
}