using BLL;
using Entities;
using Interfaces;
using Ninject;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TestProject.Filters;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class ProductController : BaseController
    {
        [Inject]
        public ProductController(ICategoryService productService, IUserService userService, ICartService cartService)
            : base(productService, userService, cartService) { }
        
        public ActionResult List(int categoryId, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            var model = new ProductListModel();

            Category category = ProdService.GetCategoryById(categoryId);

            model.Category = category;

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize ?? 10;
            model.SortType = sort ?? SortType.Alphabetic;
            model.Reverse = reverse ?? false;

            model.Products = ProdService.GetProducts(model.Category, model.PageNumber, model.PageSize,
                                                      model.SortType, model.Reverse);

            return View(model);
        }

        public ActionResult AddToCart(string productId, string count)
        {
            string email = UserService.GetEmailIfLoginIn();
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

            Product product = ProdService.GetProduct(intProductId);
            if (product == null)
            {
                Response.StatusCode = 404;
                return Content("Product not found");
            }

            CartService.Add(email, intProductId, doubleCount);
            return Content(doubleCount + " units of" + product.Name + "were successfully added");
        }

        public ActionResult Details(int id)
        {
            var product = ProdService.GetProduct(id);

            //to error page
            //if(product == null)
            return View(product);
        }
      
        private static object _lock = new object();

        [CustomAuthrize]
        public ActionResult AddArrayToCart(string[] productIds, string[] counts)
        {
            lock (_lock)
            {
                string email = GetUserEmail();
                //  string email = bkmz2
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
                                         .Select(id => ProdService.GetProduct(id))
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

                CartService.AddArray(email, products.Select(p => p.Id).ToArray(), doubleCounts);

                StringBuilder respons = new StringBuilder();
                for (int i = 0; i < products.Length; i++)
                {
                    respons.Append(string.Format("{0} units of {1},<br>",
                                                 doubleCounts[i], products[i].Name));
                }

                respons.Append("were successfully added!");

                return Json(new {report = respons.ToString()});
            }
        }
    }
}
