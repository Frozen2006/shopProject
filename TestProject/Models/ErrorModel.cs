using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}