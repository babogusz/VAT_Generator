using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAT_Generator.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string NIP { get; set; }
        public DateTime CreationDate { get; set; }

        public List<ProductQuantity> ProductQuantities { get; set; }
    }
}