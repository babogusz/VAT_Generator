using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Models
{
    [Table("dbo.tblInvoices")]
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public string NIP { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public virtual List<ProductQuantity> ProductQuantities { get; set; }
    }
}