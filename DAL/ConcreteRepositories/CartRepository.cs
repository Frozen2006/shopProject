using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.DataAccess.Base;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.ConcreteRepositories
{
    public class CartRepository : RepositoryBase<Cart>
    {
        public CartRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Cart item)
        {
          
            Cart cart = CurrentDbSet.FirstOrDefault(m => m.ProductId == item.ProductId);

            if (cart != null)
            {
                cart = Mapper.Map(item, cart);
                Context.SaveChanges();
            }

        }
    }
}
