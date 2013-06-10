using System.Collections.Generic;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Web.Common;

namespace iTechArt.Shop.Common.Services
{
    public interface ICategoryService
    {
        Category GetRootCategory();
        Category GetCategoryById(int id);
        IEnumerable<Category> GetSubcategories(Category category);
        IEnumerable<Category> GetParentsList(Category category);
        IEnumerable<Product> GetProducts(Category category, int page, int pageSize, SortType sort, bool reverse);
        Product GetProduct(int productId);
    }
}
