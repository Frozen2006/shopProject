using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.membership;
using Entities;
using Helpers;
using Interfaces;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/


        private ISearchService _search;


        public SearchController(ICategoryService ps, IUserService us, ICart cs, ISearchService ss) : base(ps, us, cs)
        {
            _search = ss;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Search(string data, string category, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            if (data.Length < 3)
                return View("Error");

            List<CategoriesInSearch> categories = _search.GetCategories(data);
            
            if (page == null)
            {
                page = 1;
                pageSize = 10;
                sort = SortType.Alphabetic;
                reverse = false;
            }

            List<Product> products;
            ProductInSearch pis;
            int count;
            if (String.IsNullOrWhiteSpace(category))
            {
                pis = _search.GetResults(data, (int) page, (int) pageSize, (SortType) sort,
                                                            (bool) reverse);
                products = pis.Products;
                count = pis.AllCount;
            }
            else
            {
                pis = _search.GetProductsFromCategory(data, category);
                products = pis.Products;
                count = pis.AllCount;
            }

            Models.SearchPageModel model = new SearchPageModel() {Categories = categories, Products = products, SearchRequest = data, PageSize = (int)pageSize, Reverse = (bool)reverse, SortType = (SortType)sort, Category = new Category(), Page = (int)page, CountAll = count };

            return View(model);
        }

        //Ajax responce method
        [HttpPost]
        public ActionResult AutocompleatRow(string data)
        {
            if ((Request.IsAjaxRequest() && !String.IsNullOrWhiteSpace(data)))
            {
                List<Product> findedProducts = _search.GetTop10Results(data);

                List<ajaxResponce> resp = new List<ajaxResponce>();

                foreach (var findedProduct in findedProducts)
                {
                    resp.Add(new ajaxResponce
                        {
                            value = findedProduct.Name,
                            ProductId = findedProduct.Id,
                            label = findedProduct.Name,
                            price = Convert.ToString(findedProduct.Price)
                        });
                }

                return Json(resp);
            }

            return null;
        }


        class ajaxResponce
        {
            public string label;
            public string value;
            public int ProductId;
            public string price;
        }

    }
}
