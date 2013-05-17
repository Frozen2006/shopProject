using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Ninject.Modules;
using System.Data.Entity;

namespace DAL
{
    public class NinjectKernel : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<ShopContext>();
        }
    }
}
