using System.Web;
using System.Web.Mvc;
using Ninject;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Logic.Membership;
using iTechArt.Shop.Web;

namespace iTechArt.Shop.Web.Filters
{
    public class CustomAuthrizeAttribute : AuthorizeAttribute
    {

        public string UserEmail;
        public new RolesType Roles = (RolesType)(-1);

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var us = NinjectWebCommon.Kernel.Get<UsersService>();

            string email = us.AtributeCheck(Roles);

            if (email == null)
                return false;
            
            httpContext.Items.Add("email", email);
            return true;
        }
    }
}