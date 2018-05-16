using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAT_Hub.Models
{
    public class InvoiceRequest
    {
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerPhone { get; set; }
        public string customerEmail { get; set; }
        public string invoiceId { get; set; }
        public string mdaId { get; set; }
        public string rtn { get; set; }
        public string currencyType { get; set; }
        public string nationality { get; set; }
        public string typeOfCargo { get; set; }
        public string portOfCall { get; set; }
        public string rotationNumber { get; set; }
        public IList<Item> Items { get; set; }
        public string totalAmount { get; set; }
        public string totalVat { get; set; }
    }
}