using System.Data.Entity;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.DataAccess.Base;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.ConcreteRepositories
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
                categoty = Mapper.Map(item, categoty);
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
