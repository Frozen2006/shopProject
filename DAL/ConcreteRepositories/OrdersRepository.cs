using System.Data.Entity;
using System.Linq;
using iTechArt.Shop.Entities;
using Interfaces;

namespace iTechArt.Shop.DataAccess.Repositories
{
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Order tiem)
        {
            Order ord = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

            if (ord != null)
            {
                ord.Buyes = tiem.Buyes;
                ord.DeliverySpot = tiem.DeliverySpot;
                ord.Status = tiem.Status;
                ord.User = tiem.User;
                
            }
            Context.SaveChanges();
        }
    }
}
