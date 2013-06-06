using System.Web.Mvc;
using BLL;
using Entities;
using Ninject;
using TestProject.Models;
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
            Category root = ProdService.GetRootCategory();
            return RedirectToAction("Details", new { id = root.Id });
        }
        
        public ActionResult Details(int id, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            Category category = ProdService.GetCategoryById(id);

            var model = new CategoryDetailsModel
                {
                    Category = category,
                    Subcategories = ProdService.GetSubcategories(category)
                };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CategoryNavigation(Category category)
        {
            var model = new CategotyNavigationModel
                {
                    Category = category,
                    Parents = ProdService.GetParentsList(category)
                };

            return PartialView(model);
        }
    }
}
