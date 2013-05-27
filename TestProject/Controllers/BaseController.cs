using BLL;
using BLL.membership;
using Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class BaseController : Controller
    {
        protected readonly CategoryService _prodService;
        protected readonly UsersService _userService;
        protected readonly ICart _cartService;

        [Inject]
        public BaseController(CategoryService ps, UsersService us, ICart cs)
        {
            _prodService = ps;
            _userService = us;
            _cartService = cs;
        }

    }
}