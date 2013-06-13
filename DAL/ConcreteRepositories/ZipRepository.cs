using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.DataAccess.Base;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.ConcreteRepositories
{
    public class ZipRepository : RepositoryBase<Zip>, IZipRepository
    {
        public ZipRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Zip item)
        {
            Zip zip = CurrentDbSet.FirstOrDefault(m => m.ZipCode == item.ZipCode);

            if (zip != null)
            {
                zip = Mapper.Map(item, zip);
                Context.SaveChanges();
            }

        }
    }
}
