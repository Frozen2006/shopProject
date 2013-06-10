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
        SearchResult GetProductsFromCategory(string searchData, int categoryId);
        SearchResult GetResults(string searchData, int page, int pageSize, SortType sort, bool reverse);
    }
}
