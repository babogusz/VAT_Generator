﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VAT_Generator.Models
{
    public class Product
    {
        [DisplayName("ID")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste.")]
        [DisplayName("Nazwa produktu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste.")]
        [DisplayName("Cena netto [zł]")]

        public decimal NetPrice { get; set; }
        [Required(ErrorMessage = "Pole nie może być puste.")]
        [DisplayName("Stawka podatku [%]")]

        public int TaxRate { get; set; }
        [Required(ErrorMessage = "Pole nie może być puste.")]
        [DisplayName("Wartość podatku [zł]")]

        public decimal TaxValue { get; set; }
        [Required(ErrorMessage = "Pole nie może być puste.")]
        [DisplayName("Cena brutto [zł]")]
        public decimal GrossPrice { get; set; }
        
        public List<Invoice> Invoices { get; set; }
    }
}