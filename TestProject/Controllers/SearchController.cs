using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Entities;
using Helpers;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public SearchController(SearchService ss)
        {
            _search = ss;
        }


        private SearchService _search;


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
            if (String.IsNullOrWhiteSpace(category))
            {
                products = _search.GetResults(data, (int) page, (int) pageSize, (SortType) sort,
                                                            (bool) reverse);
            }
            else
            {
                products = _search.GetProductsFromCategory(data, category);
            }

            Models.SearchPageModel model = new SearchPageModel() {Categories = categories, Products = products};

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
