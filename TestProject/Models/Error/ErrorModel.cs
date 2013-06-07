using System;

namespace iTechArt.Shop.Web.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}