using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VAT_Hub.Models
{
    public class RemitaResponse
    {
        [Required]
        public string orderID { get; set; }
        [Required]
        public string RRR { get; set; }
        [Required]
        public string Statuscode { get; set; }
        [Required]
        public string status { get; set; }
    }
}