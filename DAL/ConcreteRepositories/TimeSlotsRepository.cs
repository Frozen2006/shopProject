using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.DataAccess.Base;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.ConcreteRepositories
{
    public class TimeSlotsRepository : RepositoryBase<DeliverySpot>, ITimeSlotsRepository
    {
        public TimeSlotsRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(DeliverySpot item)
        {
            DeliverySpot ds = CurrentDbSet.FirstOrDefault(m => (m.StartTime == item.StartTime) && (m.Type == item.Type));

            if (ds != null)
            {
                ds = Mapper.Map(item, ds);
            }

            Context.SaveChanges();
        }
    }
}
