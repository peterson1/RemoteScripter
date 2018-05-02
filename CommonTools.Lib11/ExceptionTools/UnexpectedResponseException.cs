using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using CommonTools.Lib11.StringTools;

namespace CommonTools.Lib11.ExceptionTools
{
    public class UnexpectedResponseException : Exception
    {
        public UnexpectedResponseException(string message,
                                           string expectedResponse,
                                           string actualResponse) 
            : base(Compose(expectedResponse, actualResponse, message))
        {
        }


        private static string Compose(string expectedResponse, string actualResponse, string message)
            => $"{message}{L.f}expected: “{expectedResponse}”"
                      + $"{L.f}  actual: “{actualResponse}”";
    }
}
