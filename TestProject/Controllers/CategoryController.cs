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
    public class CategoryController : BaseController
    {
        [Inject]
        public CategoryController(ICategoryService productService, IUserService userService, ICartService cartService)
            : base(productService, userService, cartService) { }


        public ActionResult Index()
        {
            var root = ProdService.GetRootCategory();
            return RedirectToAction("Details", new { id = root.Id });
        }
        
        public ActionResult Details(int id, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            var model = new CategoryDetailsModel();

            Category category = ProdService.GetCategoryById(id);
            model.Category = category;
            model.Subcategories = ProdService.GetSubcategories(category);

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CategoryNavigation(Category category)
        {
            var model = new CategotyNavigationModel();

            model.Category = category;
            model.Parents = ProdService.GetParentsList(category);

            return PartialView(model);
        }
    }
}
