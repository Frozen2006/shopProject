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

namespace TestProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _prodService;

        [Inject]
        public CategoryController(CategoryService ps)
        {
            _prodService = ps;
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

    }
}
