using System;

namespace TestProject.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}