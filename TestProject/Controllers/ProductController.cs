using iTechArt.Shop.Logic.Services;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using Ninject;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using iTechArt.Shop.Web.Common;
using iTechArt.Shop.Web.Filters;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers 
{
    public class ProductController : BaseController
    {
        [Inject]
        public ProductController(ICategoryService productService, IUserService userService, ICartService cartService)
            : base(productService, userService, cartService) { }
        
        public ActionResult List(int categoryId, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            Category category = ProdService.GetCategoryById(categoryId);

            var model = new ProductListModel
                {
                    Category = category,
                    PageNumber = page ?? 1,
                    PageSize = pageSize ?? 10,
                    Reverse = reverse ?? false,
                    SortType = sort ?? SortType.Alphabetic
                };

            model.Products = ProdService.GetProducts(model.Category, model.PageNumber, model.PageSize,
                                                      model.SortType, model.Reverse);

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var product = ProdService.GetProduct(id);
            if (product == null)
            {
                return RedirectToAction("Error", "Error", new { Code = ErrorCode.NotFound });
            }

            return View(product);
        }

        public ActionResult AddToCart(int productId, double count)
        {
            string email = UserService.GetEmailIfLoginIn();
            if (email == null)
            {
                Response.StatusCode = 403;
                return JsonReport("You are not logged in.");
            }
            Product product = ProdService.GetProduct(productId);
            if (product == null)
            {
                Response.StatusCode = 404;
                return JsonReport("Product not found");
            }

            CartService.Add(email, productId, count);
            return JsonReport(count + " units of" + product.Name + "were successfully added");
        }

        public ActionResult AddArrayToCart(int[] productIds, double[] counts)
        {
            string email = UserService.GetEmailIfLoginIn();
            if (email == null)
            {
                Response.StatusCode = 403;
                return JsonReport("You are not logged in.");
            }

            Product[] products = productIds.Select(id => ProdService.GetProduct(id)).ToArray();
            if (products.Contains(null))
            {
                Response.StatusCode = 404;
                return JsonReport("Product not found");
            }

            CartService.AddArray(email, productIds, counts);

            var respons = new StringBuilder();
            for (int i = 0; i < products.Length; i++)
            {
                respons.Append(string.Format("{0} units of {1},<br>", counts[i], products[i].Name));
            }

            respons.Append("were successfully added!");

            return JsonReport(respons.ToString());
        }
    }
}
