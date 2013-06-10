using System;
using System.Web.Mvc;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities;
using Ninject;
using iTechArt.Shop.Common.Services;
using iTechArt.Shop.Web.Models;


namespace iTechArt.Shop.Web.Controllers 
{
    public class CategoryController : BaseController
    {
        [Inject]
        public CategoryController(ICategoryService categoryService, IUserService userService, ICartService cartService)
            : base(categoryService, userService, cartService) { }

        public ActionResult Index()
        {
            Category root = CategoryService.GetRootCategory();
            return RedirectToAction("Details", new { id = root.Id });
        }
        
        public ActionResult Details(int id)
        {
            Category category = CategoryService.GetCategoryById(id);
            if (category == null)
                return RedirectToAction("Error", "Error", new {Code = ErrorCode.NotFound});

            var model = new CategoryDetailsModel
                {
                    Category = category,
                    Subcategories = CategoryService.GetSubcategories(category)
                };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CategoryNavigation(Category category)
        {
            if(category == null)
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.Unknown });

            var model = new CategotyNavigationModel
                {
                    Category = category,
                    Parents = CategoryService.GetParentsList(category)
                };

            return PartialView(model);
        }
    }
}
