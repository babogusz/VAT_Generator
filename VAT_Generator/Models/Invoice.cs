using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VAT_Generator.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required(ErrorMessage = "Pole nie może być puste.")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Pole nie może być puste.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Pole nie może być puste.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Numer NIP musi składać się z dziesięciu cyfr.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Numer NIP musi składać się z dziesięciu cyfr.")]
        public string NIP { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public List<ProductQuantity> ProductQuantities { get; set; }
    }
}