using System.Net;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Logic.Services;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using Ninject;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using iTechArt.Shop.Web.Filters;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers 
{
    public class ProductController : BaseController
    {
        [Inject]
        public ProductController(ICategoryService categoryService, IUserService userService, ICartService cartService)
            : base(categoryService, userService, cartService) { }
        
        public ActionResult List(int categoryId, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            Category category = CategoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotFound });
            }

            var model = new ProductListModel
                {
                    Category = category,
                    PageNumber = page ?? 1,
                    PageSize = pageSize ?? 10,
                    Reverse = reverse ?? false,
                    SortType = sort ?? SortType.Alphabetic
                };

            model.Products = CategoryService.GetProducts(model.Category, model.PageNumber, model.PageSize,
                                                      model.SortType, model.Reverse);

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var product = CategoryService.GetProduct(id);
            if (product == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotFound });
            }

            return View(product);
        }

        /// <summary>
        /// For AJAX calls only.
        /// </summary>
        /// <returns>JSON report and response status code 403, 404 if error occured.</returns>
        public ActionResult AddToCart(int productId, double count)
        {
            if (count < 0)
            {
                return JsonReport("Incorrect product count", HttpStatusCode.InternalServerError);
            }

            string email = UserService.GetEmailIfLoginIn();
            if (email == null)
            {
                return JsonReport("You are not logged in.", HttpStatusCode.Unauthorized);
            }

            Product product = CategoryService.GetProduct(productId);
            if (product == null)
            {
                return JsonReport("Product not found", HttpStatusCode.NotFound);
            }

            CartService.Add(email, productId, count);

            string report = CartService.GetAddingReport(product, count);
            return JsonReport(report);
        }

        /// <summary>
        /// For AJAX calls only.
        /// </summary>
        /// <returns>JSON report and response status code 403, 404 if error occured.</returns>
        public ActionResult AddArrayToCart(int[] productIds, double[] counts)
        {
            if (counts.Min() < 0)
            {
                return JsonReport("Incorrect product count", HttpStatusCode.InternalServerError);
            }

            string email = UserService.GetEmailIfLoginIn();
            if (email == null)
            {
                return JsonReport("You are not logged in.", HttpStatusCode.Unauthorized);
            }

            Product[] products = productIds.Select(id => CategoryService.GetProduct(id)).ToArray();
            if (products.Contains(null))
            {
                return JsonReport("Product not found", HttpStatusCode.NotFound);
            }

            CartService.AddArray(email, productIds, counts);

            string report = CartService.GetAddingReport(products, counts);
            return JsonReport(report);
        }
    }
}
