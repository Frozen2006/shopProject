using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.Repositories
{
    public class CartRepository : RepositoryBase<Cart>
    {
        public CartRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Cart tiem)
        {
          
            Cart cart = CurrentDbSet.FirstOrDefault(m => m.ProductId == tiem.ProductId);

            if (cart != null)
            {
                /*cart.Count = tiem.Count;
                cart.Product = cart.Product;*/
                cart = Mapper.Map(tiem, cart);

                Context.SaveChanges();
            }

        }
    }
}
