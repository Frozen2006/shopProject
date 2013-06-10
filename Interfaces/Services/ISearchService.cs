using System.Collections.Generic;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities.PresentationModels;

namespace iTechArt.Shop.Common.Services
{
    public interface ISearchService
    {
        List<Product> GetTop10Results(string searchData);
        List<CategoriesInSearch> GetCategories(string searchData);
        SearchResult GetResults(string searchData,  int? categoryId, int page, int pageSize, SortType sort, bool reverse);
    }
}
