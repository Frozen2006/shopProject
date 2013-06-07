using System.Data.Entity;
using System.Linq;
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
            Zip zip = CurrentDbSet.FirstOrDefault(m => m.zip1 == tiem.zip1);

            if (zip != null)
            {
                zip.zip1 = tiem.zip1;
                zip.city = tiem.city;
                zip.sub_city = tiem.sub_city;

                Context.SaveChanges();
            }

        }
    }
}
