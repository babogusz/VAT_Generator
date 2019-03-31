using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Models
{
    [Table("dbo.tblProductQuantities")]
    public class ProductQuantity
    {
        [Key]
        public int ProductQuantityId { get; set; }
        public int Quantity { get; set; }

        [Required]
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public virtual int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}