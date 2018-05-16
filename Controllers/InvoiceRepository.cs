using System;
using System.Net.Http;
using System.Threading.Tasks;
using VAT_Hub.Models;
using VAT_Hub.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

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
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = new InvoiceResponse();
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