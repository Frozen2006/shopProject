using BLL;
using BLL.membership;
using Entities;
using Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ActionResult AddToCart(string productId, string count)
        {
            string email = _userService.GetEmailIfLoginIn();
            if (email == null)
            {
                Response.StatusCode = 403;
                return Content("User is not logged in");
            }

            int intProductId;
            double doubleCount;

            try
            {
                intProductId = int.Parse(productId);
                doubleCount = double.Parse(count);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content("Incorrect arguments:<br>" + ex.Message);
            }

            Product product = _prodService.GetProduct(intProductId);
            if (product == null)
            {
                Response.StatusCode = 404;
                return Content("Product not found");
            }

            _cartService.Add(email, intProductId, doubleCount);
            return Content(doubleCount + " units of" + product.Name + "were successfully added");
        }

        public ActionResult Details(int id)
        {
            var product = _prodService.GetProduct(id);

            //to error page
            //if(product == null)
            return View(product);
        }


        public ActionResult AddArrayToCart(string[] productIds, string[] counts)        
        {
            string email = _userService.GetEmailIfLoginIn();            
            if (email == null)
            {
                Response.StatusCode = 403;
                return Content("User is not logged in");
            }
            
            Product[] products;
            double[] doubleCounts;

            try
            {
                products = productIds.Select(id => int.Parse(id))
                    .Select(id => _prodService.GetProduct(id))
                    .ToArray();

                doubleCounts = counts.Select(c => double.Parse(c))
                    .ToArray();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content("Incorrect arguments:<br>" + ex.Message);
            }
            
            if (products.Contains(null))
            {
                Response.StatusCode = 404;
                return Content("Some products were not found");
            }

            StringBuilder respons = new StringBuilder();
            for (int i = 0; i < products.Length; i++)
            {
                _cartService.Add(email, products[i].Id, doubleCounts[i]);

                respons.Append(string.Format("{0} units of {1},<br>",
                    doubleCounts[i], products[i].Name));
            }

            respons.Append("were successfully added!");

            return Json(new { report = respons.ToString() });
        }
    }
}
