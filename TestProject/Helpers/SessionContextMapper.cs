using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Interfaces;

namespace TestProject.Helpers
{
    public class SessionContextMapper : ISessionContext
    {
        public void SetSessionData(string guid)
        {
            HttpCookie cookie = new HttpCookie("session_data");
            cookie.Value = guid;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public string GetSessionDataIfExist()
        {
            //Find cookie, and if exist - return cookie value
            HttpRequest Request = HttpContext.Current.Request;
            if ((Request.Cookies.Get("session_data") != null) &&
                (Request.Cookies.Get("session_data").Value != String.Empty))
            {
                return Request.Cookies.Get("session_data").Value;
            }
            else
            {
                return null;
            }
        }

        public void RemoveSessionData()
        {
            
            if (HttpContext.Current.Request.Cookies["session_data"] != null)
            {
                HttpContext.Current.Cache.Remove(HttpContext.Current.Request.Cookies.Get("session_data").Value);

                var cookie = new HttpCookie("session_data")
                {
                    Expires = DateTime.Now.AddDays(-1d)
                };

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
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
