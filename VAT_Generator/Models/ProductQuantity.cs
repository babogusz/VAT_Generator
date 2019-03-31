using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VAT_Generator.Models
{
    public class ProductQuantity
    {
        public int ProductQuantityId { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }

    public class ProductQuantityViewModel
    {
        public ProductQuantity ProductQuantity { get; set; }
        public Product Product { get; set; }
    }
}