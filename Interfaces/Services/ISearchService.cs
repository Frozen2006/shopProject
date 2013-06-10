using System.Collections.Generic;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Common;

namespace iTechArt.Shop.Common.Services
{
    public interface ISearchService
    {
        List<Product> GetTop10Results(string searchData);
        List<CategoriesInSearch> GetCategories(string searchData);
        SearchResult GetProductsFromCategory(string searchData, string categoryName);
        SearchResult GetResults(string searchData, int page, int pageSize, SortType sort, bool reverse);
    }
}
