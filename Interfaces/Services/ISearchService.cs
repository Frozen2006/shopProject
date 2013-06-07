using System.Collections.Generic;
using iTechArt.Shop.Entities;
using Helpers;
using iTechArt.Shop.Web.Common;

namespace iTechArt.Shop.Common.Services
{
    public interface ISearchService
    {
        List<Product> GetTop10Results(string searchData);
        List<CategoriesInSearch> GetCategories(string searchData);
        ProductInSearch GetProductsFromCategory(string searchData, string categoryName);
        ProductInSearch GetResults(string searchData, int page, int pageSize, SortType sort, bool reverse);
    }
}
