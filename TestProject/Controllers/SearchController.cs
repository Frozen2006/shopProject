using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Services;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Entities;
using Ninject;
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

        public ActionResult Search(string data, int? category, int? page, int? pageSize, SortType? sort, bool? reverse)
        {
            if (String.IsNullOrWhiteSpace(data) || (data.Length <= 3))
            {
                return View(new SearchPageModel() { Categories = new List<CategoriesInSearch>(), Products = new List<Product>()});
            }

            List<CategoriesInSearch> categories = SearchService.GetCategories(data);

            page = page ?? 1;
            pageSize = pageSize ?? 10;
            sort = sort ?? SortType.Alphabetic;
            reverse = reverse ?? false;
            

            SearchResult searchResult = SearchService.GetResults(data, category, (int) page, (int) pageSize, (SortType) sort,
                                                        (bool) reverse);

            var model = new SearchPageModel()
                {
                    Categories = categories,
                    Products = searchResult.Products,
                    SearchRequest = data,
                    PageSize = (int) pageSize,
                    Reverse = (bool) reverse,
                    SortType = (SortType) sort,
                    Page = (int) page,
                    CategoryId = category,
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
