using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VAT_Hub.Models
{
    public class ERPRequest
    {
        [Required]
        public string vatAmount { get; set; }
        [Required]
        public string subAmount { get; set; }
        [Required]
        public string payerName { get; set; }
        [Required]
        public string payerEmail { get; set; }
        [Required]
        public string payerPhone { get; set; }
    }
}