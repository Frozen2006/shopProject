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
    public class ZipRepository : RepositoryBase<Zip>
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
