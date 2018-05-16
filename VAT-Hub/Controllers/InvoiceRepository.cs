using System;
using System.Net.Http;
using System.Threading.Tasks;
using VAT_Hub.Models;
using VAT_Hub.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace VAT_Hub.Controllers
{
    public class InvoiceRepository
    {
        private static HttpClient Client;
        private static MyHttpClientHandler.MyHandler _handler;
        public async Task<InvoiceResponse> PushInvoiceToRDCBrains(InvoiceRequest request)
        {
            _handler = new MyHttpClientHandler.MyHandler();
            Client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("ADDA:test");
            Client.BaseAddress = new Uri("http://35.185.72.232/");
            Client.DefaultRequestHeaders.Accept.Clear();
            //Client.DefaultRequestHeaders.Add("Content-Type", "application/json");
           // Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = new InvoiceResponse();
            var json = new JavaScriptSerializer().Serialize(request);
            HttpContent _Body = new StringContent(json);
            // and add the header to this object instance
            // optional: add a formatter option to it as well
            _Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // synchronous request without the need for .ContinueWith() or await
            HttpResponseMessage  asresponse = Client.PostAsync("RDCWebServices/RDCPush.jsp", _Body).Result;
            if (asresponse.IsSuccessStatusCode)
            {
                var resresultasstring = await asresponse.Content.ReadAsStringAsync();
            }
            var result = await Client.PostAsJsonAsync("RDCWebServices/RDCPush.jsp", request);
            if (result.IsSuccessStatusCode)
            {
                var resultasstring = await result.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<InvoiceResponse>(resultasstring);
            }
            else
            {
                response = DummyResponse();
            }
            return response;
        }
        private InvoiceResponse DummyResponse()
        {
            InvoiceResponse response = new InvoiceResponse();
            response.Bill_Number = "Payment Reference generated";
            response.peopleRSN = "025";
            response.folderRSN = "310007680995";
            response.statusCode = "310007680995";
            response.status = "310007680995";
            return response;
        }
    }
}