using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.membership;
using Helpers;
using Ninject;

namespace TestProject.Filters
{
    public class CustomAuthrizeAttribute : AuthorizeAttribute
    {

        public string UserEmail;
        public new RolesType Roles = (RolesType)(-1);

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            UsersService us = App_Start.NinjectWebCommon.Kernel.Get<UsersService>();

            string email = us.AtributeCheck(Roles);

            if (email == null)
                return false;
            
            httpContext.Items.Add("email", email);
            return true;
        }
    }
}