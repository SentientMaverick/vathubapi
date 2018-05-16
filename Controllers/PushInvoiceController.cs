using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using VAT_Hub.Models;

namespace VAT_Hub.Controllers
{
    [Route("api/push_invoice_data")]
    public class PushInvoiceController : ApiController
    {
        // GET: api/PushInvoice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PushInvoice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PushInvoice
        [HttpPost]
        public async Task<IHttpActionResult> Post(InvoiceRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }
                InvoiceRepository _repository = new InvoiceRepository();
                InvoiceResponse response = await _repository.PushInvoiceToRDCBrains(request);
                if (response.statusCode == "025")
                {
                    return Created<InvoiceResponse>(Request.RequestUri, response);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // PUT: api/PushInvoice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PushInvoice/5
        public void Delete(int id)
        {
        }
    }
}
