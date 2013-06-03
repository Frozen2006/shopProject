using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error(string code)
        {
            ErrorCode errorCode;
            if (!Enum.TryParse<ErrorCode>(code ,out errorCode))
                errorCode = ErrorCode.Unknown;

            string message;

            switch (errorCode)
            {
                case ErrorCode.NotLoggedIn:
                    message = "User is not logged in";
                    break;
                case ErrorCode.NotFound:
                    message = "Resource was not found";
                    break;
                case ErrorCode.Forbidden:
                    message = "Access forbidden";
                    break;
                default:
                    message = "Unknown error occured";
                    break;
            }

            var model = new ErrorModel();
            model.Message = message;

            return View("Error", model);
        }
    }
}
