using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Repositories;
using DAL.Repositories.DbFirstRepository;
using Entities;
using Ninject;

namespace BLL
{
    public class CategoryService
    {
        [Inject]
        public IRepository<Product> ProductRepository { get; set; }
        [Inject]
        public ICategoryRepository CategoryRepository { get; set; }
        
        public Category GetRootCategory()
        {
            return CategoryRepository.GetRootCategory();
        }
        
        public Category GetCategoryById(int id)
        {
            return CategoryRepository.Read(id);
        }

        public IEnumerable<Category> GetSubcategories(Category category)
        {
            return category.Categories1.Where(c => c.Id != category.Id);
        }

        public IEnumerable<Category> GetParentsList(Category category)
        {
            Category parent = category.Category1;
            var parents = new List<Category>();

            while (category.Category1.Id != category.Id)
            {
                parents.Add(category.Category1);
                category = category.Category1;
            }

            parents.Reverse();

            return parents;
        }
    }
}
