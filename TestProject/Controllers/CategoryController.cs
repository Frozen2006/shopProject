using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Entities;
using Ninject;
using TestProject.App_Start;
using TestProject.Models;
using BLL.membership;
using Interfaces;

namespace TestProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _prodService;
        private readonly UsersService _userService;
        private readonly ICart _cartService;

        [Inject]
        public CategoryController(CategoryService ps, UsersService us, ICart cs)
        {
            _prodService = ps;
            _userService = us;
            _cartService = cs;
        }

        public ActionResult Index()
        {
            var root = _prodService.GetRootCategory();
            return RedirectToAction("Details", new { id = root.Id });
        }
        
        public ActionResult Details(int id, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            var model = new CategoryDetailsModel();

            Category category = _prodService.GetCategoryById(id);

            model.Category = category;
            model.Subcategories = _prodService.GetSubcategories(category);
            model.Parents = _prodService.GetParentsList(category);

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize ?? 10;
            model.SortType = sort ?? SortType.Alphabetic;
            model.Reverse = reverse ?? false;

            model.Products = _prodService.GetProducts(model.Category, model.PageNumber, model.PageSize,
                                                      model.SortType, model.Reverse);


            return View(model);
        }

        public ActionResult AddToCart(string productId, string count)
        {
            int intProductId;
            int intCount;

            if (!int.TryParse(productId, out intProductId) ||
                !int.TryParse(count, out intCount))
            {
                return Content("Incorrect arguments");
            }

            var cookie = Request.Cookies["session_data"];
            if (cookie == null)
            {
                return Content("User is not logged in");
            }

            string email = _userService.GetUserEmailFromSession(cookie.Value);
            if (email == null)
            {
                return Content("User is not logged in");
            }
            

            Product product = _prodService.GetProduct(intProductId);
            if (product == null)
                return Content("Product not found");


            _cartService.Add(email, intProductId, intCount);
            return Content(intCount + " units of" + product.Name + "were successfully added");
        }

        public ActionResult TestAction()
        {
            return Content("It works");
        }


        /*
             interface ICart
    {
        void Add(string UserEmail, int ProductId, int Count);
        List<Helpers.ProductInCart> GetAllChart(string UserEmail);
        void DeleteProduct(string UserEmail, int ProductId);
        void Clear(string UserEmail);
        void UpateCount(string UserEmail, int NewCount);
        double GetTotalPrice(string UserEmail);
        
        //Other methods be realize in future
        //void Buy(string UserEmail);
    }
         */

    }
}
