using System;
using System.Web;
using System.Web.Caching;
using iTechArt.Shop.Web.Common;

namespace iTechArt.Shop.Web.Helpers
{
    public class SessionContextMapper : ISessionContext
    {
        public void SetSessionData(string guid)
        {
            var cookie = new HttpCookie("session_data") {Value = guid};
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public string GetSessionDataIfExist()
        {
            //Find cookie, and if exist - return cookie value
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("session_data");
            if ( (cookie != null) && (cookie.Value != String.Empty))
            {
                return cookie.Value;
            }
            return null;
        }

        public void RemoveSessionData()
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies.Get("session_data");
            
            if (httpCookie == null) return;
            
            HttpContext.Current.Cache.Remove(httpCookie.Value);

            var cookie = new HttpCookie("session_data")
                {
                    Expires = DateTime.Now.AddDays(-1d)
                };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public string GetUserDataFromCash(string guid)
        {
            return (string)HttpContext.Current.Cache.Get(guid);
        }

        public void AddUserDataToCash(string guid, string email)
        {
            HttpContext.Current.Cache.Add(guid, email, null, DateTime.Now.AddDays(1.0), TimeSpan.Zero,
                                              CacheItemPriority.Normal, null);
        }

    }
}
