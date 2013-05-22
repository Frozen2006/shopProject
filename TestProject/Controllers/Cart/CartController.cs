using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.membership;
using Interfaces;
using Ninject;
using TestProject.Models;

namespace TestProject.Controllers.Cart
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        private readonly ICart _cart;
        private readonly UsersService _usersService;

        [Inject]
        public CartController(ICart cartInit, UsersService us)
        {
            _cart = cartInit;
            _usersService = us;
        }


        [Filters.CustomAuthrize(Roles = "User")]
        public ActionResult Index()
        {

            string sessionId = Request.Cookies.Get("session_data").Value;
            string userEmail = _usersService.GetUserEmailFromSession(sessionId);

            CartViewModel model = new CartViewModel();
            model.Products = _cart.GetAllChart(userEmail);
            model.TotalPrice = _cart.GetTotalPrice(userEmail);


            return View(model);
        }

    }
}
