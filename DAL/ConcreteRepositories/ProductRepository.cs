using System.Data.Entity;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
        public override void Update(Product item)
        {
            var product = CurrentDbSet.Find(item.Id);
            if (product != null)
            {
                product = Mapper.Map(item, product);
            }
            Context.SaveChanges();
        }
    }
}
