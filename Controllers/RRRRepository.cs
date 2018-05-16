using System;
using System.Net.Http;
using System.Threading.Tasks;
using VAT_Hub.Models;
using VAT_Hub.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace VAT_Hub.Controllers
{
    public class RRRRepository
    {
        private static HttpClient Client;
        private static MyHttpClientHandler.MyHandler _handler;
        public async Task<RemitaResponse> GetRRRNumber(ERPRequest request)
        {
            _handler = new MyHttpClientHandler.MyHandler();
            Client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("ADDA:test");
            string url= "vatAmount=" + request.vatAmount + "&subAmount=" + request.subAmount + "&payerName=" + request.payerName + "&payerEmail=" + request.payerEmail + "&payerPhone=" + request.payerPhone;
            url = url.Trim();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = new RemitaResponse();
            var result= await Client.GetAsync("http://35.185.72.232/PAYMENT_CODE_GENERATOR/api/generate_split_payment_rrr?"+ "vatAmount=" + request.vatAmount + "&subAmount=" + request.subAmount + "&payerName=" + HttpUtility.UrlEncode(request.payerName) + "&payerEmail=" + request.payerEmail + "&payerPhone=" + request.payerPhone);
            if (result.IsSuccessStatusCode)
            {
                var resultasstring = await result.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<RemitaResponse>(resultasstring);
                if (response.Statuscode != "025")
                {
                    // response = DummyResponse();
                    //"vatAmount=26.00&subAmount=324.00&payerName=Mr.Stephen%20Kershaw&payerEmail=kershaw@mail3.netprovider.net&payerPhone=07019813547"
                }
            }
            else
            {
                response = DummyResponse();
            }
            return response;
        }
        private RemitaResponse DummyResponse()
        {
            RemitaResponse response = new RemitaResponse();
            response.status = "Payment Reference generated";
            response.Statuscode = "025";
            response.RRR = "310007680995";
            response.orderID = "310007680995";
            return response;
        }
    }
}