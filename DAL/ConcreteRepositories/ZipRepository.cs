using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;


namespace iTechArt.Shop.DataAccess.Repositories
{
    public class ZipRepository : RepositoryBase<Zip>, IZipRepository
    {
        public ZipRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Zip tiem)
        {
            Zip zip = CurrentDbSet.FirstOrDefault(m => m.ZipCode == tiem.ZipCode);

            if (zip != null)
            {
                zip = Mapper.Map(tiem, zip);
                Context.SaveChanges();
            }

        }
    }
}
