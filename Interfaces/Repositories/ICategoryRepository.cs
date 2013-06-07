using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Common.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetRootCategory();
    }
}
