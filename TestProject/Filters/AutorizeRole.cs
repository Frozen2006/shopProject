using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.membership;
using Ninject;

namespace TestProject.Filters
{
    public class CustomAuthrizeAttribute : AuthorizeAttribute
    {

       // public string Role;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            UsersService us = App_Start.NinjectWebCommon.Kernel.Get<UsersService>();


            return us.AtributeCheck(Roles);
        }
    }
}