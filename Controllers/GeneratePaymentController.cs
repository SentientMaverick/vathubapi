using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VAT_Hub.Models;

namespace VAT_Hub.Controllers
{
    [Route("api/generate_split_payment_rrr")]
   // [RoutePrefix(Name ="api/generate_split_payment_rrr")]
    public class GeneratePaymentController : ApiController
    {
        // GET: api/GeneratePayment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GeneratePayment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GeneratePayment
        [HttpPost]
        public async Task<IHttpActionResult> Post(string vatAmount,string subAmount,     
            string payerName, string payerEmail,string payerPhone, ERPRequest request)
        {      
            try
            {
                if (request == null && payerPhone!=null)
                {
                    request = new ERPRequest();
                    request.payerEmail = payerEmail.Trim();
                    request.payerPhone = payerPhone.Trim();
                    request.payerName = payerName.Trim();
                    int v1 =(int)Convert.ToDecimal(vatAmount.Trim().ToString());
                    int v2 = (int)Convert.ToDecimal(subAmount.Trim().ToString());
                    request.vatAmount =v1.ToString();
                    request.subAmount = v2.ToString();
                } 
                if (request == null)
                {
                   return BadRequest();
                }
                RRRRepository _repository=new RRRRepository();
                RemitaResponse response = await _repository.GetRRRNumber(request);
                if (response.Statuscode == "025")
                {        
                    return Created<RemitaResponse>(Request.RequestUri, response);
                }
                return BadRequest();
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }

        // PUT: api/GeneratePayment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GeneratePayment/5
        public void Delete(int id)
        {
        }
    }
}
