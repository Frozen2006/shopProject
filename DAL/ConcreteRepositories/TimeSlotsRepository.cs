using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;
using Entities;

namespace DAL.membership
{
    public class TimeSlotsRepository : RepositoryBase<DeliverySpot>
    {
        public TimeSlotsRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(DeliverySpot tiem)
        {
            DeliverySpot ds = CurrentDbSet.FirstOrDefault(m => (m.StartTime == tiem.StartTime) && (m.Type == tiem.Type));

            if (ds != null)
            {
                ds.EndTime = tiem.EndTime;
                ds.Orders = tiem.Orders;
                ds.StartTime = tiem.StartTime;
                ds.Type = tiem.Type;
                ds.Users = tiem.Users;
            }

            Context.SaveChanges();
        }
    }
}
