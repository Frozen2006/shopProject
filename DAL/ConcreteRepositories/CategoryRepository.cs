using System.Data.Entity;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(DbContext context)
            : base(context)
        {
        }
        public override void Update(Category item)
        {
            var categoty = CurrentDbSet.Find(item.Id);
            if (categoty != null)
            {

                categoty.Parent = item.Parent;
                categoty.Name = item.Name;

                categoty.ChildCategories = item.ChildCategories;
                categoty.ParentCategory = item.ParentCategory;
                categoty.Products = item.Products;
            }
            Context.SaveChanges();
        }

        private const int _rootCategoryId = 0;
        public Category GetRootCategory()
        {
            return Read(_rootCategoryId);
        }
    }
}
