﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;
using Interfaces.Repositories;
using Ninject.Modules;
using System.Data.Entity;
using Ninject.Web.Common;

namespace NinjectModules
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {

            Bind<DbContext>().To<DAL.ShopContext>().InRequestScope();

            Bind<ICategoryRepository>().To<DAL.Repositories.DbFirstRepository.CategoryRepository>();
            Bind<IRepository<Product>>().To<DAL.Repositories.DbFirstRepository.ProductRepository>(); 
            

            Bind<DAL.membership.UserRepository>().To<DAL.membership.UserRepository>();
            Bind<DAL.membership.RoleRepository>().To<DAL.membership.RoleRepository>();
            Bind<DAL.membership.SessionRepository>().To<DAL.membership.SessionRepository>();

        }
    }
}