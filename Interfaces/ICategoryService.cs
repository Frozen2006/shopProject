using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Entities;

namespace Interfaces
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
