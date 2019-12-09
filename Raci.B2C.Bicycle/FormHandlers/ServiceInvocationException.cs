using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public class ServiceInvocationException : Exception
    {
        public HttpResponseMessage Response { get; private set; }

        public ServiceInvocationException(HttpResponseMessage message)
        {
            Response = message;
        }
    }
}