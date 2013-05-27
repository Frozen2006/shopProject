using BLL;
using BLL.membership;
using Entities;
using Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class ProductController : BaseController
    {
        [Inject]
        public ProductController(CategoryService ps, UsersService us, ICart cs)
            : base(ps, us, cs) { } 
        
        public ActionResult List(int categoryId, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            var model = new ProductListModel();

            Category category = _prodService.GetCategoryById(categoryId);

            model.Category = category;

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize ?? 10;
            model.SortType = sort ?? SortType.Alphabetic;
            model.Reverse = reverse ?? false;

            model.Products = _prodService.GetProducts(model.Category, model.PageNumber, model.PageSize,
                                                      model.SortType, model.Reverse);

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var product = _prodService.GetProduct(id);

            //to error page
            //if(product == null)
            return View(product);
        }

    }
}
