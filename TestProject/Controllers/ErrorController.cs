using System;
using System.Web.Mvc;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Services;
using Ninject;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers 
{
    public class ErrorController : Controller
    {
        private IErrorService ErrorService { get; set; }

        [Inject]
        public ErrorController(IErrorService errorService)
        {
            ErrorService = errorService;
        }

        public ActionResult Index()
        {
            var model = new ErrorModel();
            return View("Error", model);
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
