using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("dbo.tblProducts")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal NetPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxValue { get; set; }
        public decimal GrossPrice { get; set; }
    }
}