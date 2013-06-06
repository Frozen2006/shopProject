using System;
using System.Web.Mvc;
using Interfaces;
using Ninject;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class ErrorController : Controller
    {
        private IErrorService ErrorService { get; set; }

        [Inject]
        public ErrorController(IErrorService errorService)
        {
            ErrorService = errorService;
        }

        public ActionResult Error(string code)
        {
            ErrorCode errorCode;
            if (!Enum.TryParse<ErrorCode>(code ,out errorCode))
                errorCode = ErrorCode.Unknown;

            string message = ErrorService.GetErrorDescription(errorCode);

            var model = new ErrorModel {Message = message};

            return View("Error", model);
        }

        public ActionResult Custom(string message)
        {
            var model = new ErrorModel {Message = message};

            return View("Error", model);
        }
    }
}
