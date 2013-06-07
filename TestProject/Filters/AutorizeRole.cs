using System.Web;
using System.Web.Mvc;
using Helpers;
using Ninject;
using iTechArt.Shop.Logic.Membership;

namespace TestProject.Filters
{
    public class CustomAuthrizeAttribute : AuthorizeAttribute
    {

        public string UserEmail;
        public new RolesType Roles = (RolesType)(-1);

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var us = App_Start.NinjectWebCommon.Kernel.Get<UsersService>();

            string email = us.AtributeCheck(Roles);

            if (email == null)
                return false;
            
            httpContext.Items.Add("email", email);
            return true;
        }
    }
}