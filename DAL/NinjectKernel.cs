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
            //object life cycle = life cycle of thread.
            //1 user = 1 thread... -> 1 context on 1 user
            Bind<DbContext>().To<ShopContext>().InThreadScope();
        }
    }
}
