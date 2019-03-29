using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VAT_Generator.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal NetPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxValue { get; set; }
        public decimal GrossPrice { get; set; }
        
        public List<Invoice> Invoices { get; set; }
    }
}