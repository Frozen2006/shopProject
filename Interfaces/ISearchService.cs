using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTechArt.Shop.Entities;
using Helpers;
using iTechArt.Shop.Entities;

namespace Interfaces
{
    public interface ISearchService
    {
        List<Product> GetTop10Results(string searchData);
        List<CategoriesInSearch> GetCategories(string searchData);
        ProductInSearch GetProductsFromCategory(string searchData, string categoryName);
        ProductInSearch GetResults(string searchData, int page, int pageSize, SortType sort, bool reverse);
    }
}
