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
        public CategoryController(ICategoryService ps, IUserService us, ICart cs)
            : base(ps, us, cs) { }


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

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CategoryNavigation(Category category)
        {
            var model = new CategotyNavigationModel();

            model.Category = category;
            model.Parents = _prodService.GetParentsList(category);

            return PartialView(model);
        }
    }
}
