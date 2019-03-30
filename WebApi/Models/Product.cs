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
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal NetPrice { get; set; }
        [Required]
        public int TaxRate { get; set; }
        [Required]
        public decimal TaxValue { get; set; }
        [Required]
        public decimal GrossPrice { get; set; }
    }
}