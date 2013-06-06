using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace BLL
{
    public class ErrorService : IErrorService
    {
        public string GetErrorDescription(ErrorCode code)
        {
            switch (code)
            {
                case ErrorCode.NotLoggedIn:
                    return "User is not logged in";
                case ErrorCode.NotFound:
                    return "Resource was not found";
                case ErrorCode.Forbidden:
                    return "Access forbidden";
                default:
                    return "Unknown error occured";
            }
        }
    }
}
