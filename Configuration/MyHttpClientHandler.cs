using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace VAT_Hub.Configuration
{
    public class MyHttpClientHandler
    {
        public class MyHandler : HttpClientHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
               // request.Headers.Add("username", "ADDA");
                //request.Headers.Add("password", "test");
                request.Headers.Add("userId", "12345");
                request.Headers.Add("secretKey", "XYZABC");

                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}