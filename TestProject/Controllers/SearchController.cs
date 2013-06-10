using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Services;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Logic.Services;
using iTechArt.Shop.Entities;
using Ninject;
using iTechArt.Shop.Web.Common;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers 
{
    public class SearchController : BaseController
    {
        private ISearchService SearchService { get; set; }

        [Inject]
        public SearchController(ICategoryService categoryService, IUserService userService, ICartService cartService, ISearchService searchService)
            : base(categoryService, userService, cartService)
        {
            SearchService = searchService;
        }

        //What is category? is this id? than ti should be int!
        //If it is name? why do you use name?
        public ActionResult Search(string data, string category, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            ////What the hell is this?
            /// User sends search request and gets error O_o
            if (data.Length < 3)
                return RedirectToAction("Custom", "Error", new { Message = "Too short search request" });

            List<CategoriesInSearch> categories = SearchService.GetCategories(data);

            page = page ?? 1;
            pageSize = pageSize ?? 10;
            sort = sort ?? SortType.Alphabetic;
            reverse = reverse ?? false;

            SearchResult searchResult;
            if (String.IsNullOrWhiteSpace(category))
            {
                searchResult = SearchService.GetResults(data, (int) page, (int) pageSize, (SortType) sort,
                                                        (bool) reverse);
            }
            else
            {
                searchResult = SearchService.GetProductsFromCategory(data, category);
            }

            var model = new SearchPageModel()
                {
                    Categories = categories,
                    Products = searchResult.Products,
                    SearchRequest = data,
                    PageSize = (int) pageSize,
                    Reverse = (bool) reverse,
                    SortType = (SortType) sort,
                    Category = new Category(),
                    Page = (int) page,
                    CountAll = searchResult.AllCount
                };

            return View(model);
        }

        //Ajax responce method
        [HttpPost]
        public ActionResult AutocompleatRow(string data)
        {
            if ((Request.IsAjaxRequest() && !String.IsNullOrWhiteSpace(data)))
            {
                List<Product> findedProducts = SearchService.GetTop10Results(data);

                var resp = findedProducts.Select(findedProduct => new
                {
                    value = findedProduct.Name,
                    ProductId = findedProduct.Id,
                    label = findedProduct.Name,
                    price = Convert.ToString(findedProduct.Price)
                })
                .ToList();

                return Json(resp);
            }

            return null;
        }
    }
}
