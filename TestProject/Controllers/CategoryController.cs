using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using DAL.Repositories;
using Ninject;
using TestProject.App_Start;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _prodService = NinjectWebCommon.Kernel.Get<CategoryService>();
        //
        // GET: /Product/
        public ActionResult Index()
        {
            var root = _prodService.GetRootCategory();
            return RedirectToAction("Details", new { id = root.Id });
        }

        public ActionResult Details(int id)
        {
            var categoryDetails = new CategoryDetailsModel();
           
            Category category = _prodService.GetCategoryById(id);

            categoryDetails.Category = category;
            categoryDetails.Subcategories = _prodService.GetSubcategories(category);
            categoryDetails.Parents = _prodService.GetParentsList(category);

            return View(categoryDetails);
        }

    }
}
