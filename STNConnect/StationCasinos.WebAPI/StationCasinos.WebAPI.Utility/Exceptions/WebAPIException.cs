using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.WebAPI.Utility
{
    public class WebAPIException : Exception
    {
        public WebAPIException() { }

        public WebAPIException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public WebAPIException(HttpStatusCode statusCode, Exception exception) : base(exception.Message,exception)
        {
            StatusCode = statusCode;
        }

        public WebAPIException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
