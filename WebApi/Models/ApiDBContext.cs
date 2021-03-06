﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Models
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext()
        {
            Configuration.LazyLoadingEnabled = false;
            this.Database.Initialize(false);

            if (this.Products.Count() == 0)
            {
                var product = new Product() { Name = "Bulbulator", NetPrice = 100, TaxRate = 5, TaxValue = 23, GrossPrice = 123 };
                this.Products.Add(product);
                product = new Product() { Name = "Trytytka", NetPrice = 10, TaxRate = 5, TaxValue = 0.5M, GrossPrice = 10.5M };
                this.Products.Add(product);
                product = new Product() { Name = "Mop ze spryskiwaczem Rovus", NetPrice = 200, TaxRate = 7, TaxValue = 14, GrossPrice = 214 };
                this.Products.Add(product);
                product = new Product() { Name = "Kultywator ręczny Tornadica Top Shop", NetPrice = 1000, TaxRate = 23, TaxValue = 230, GrossPrice = 1230 };
                this.Products.Add(product);
                product = new Product() { Name = "Godzina pracy programisty", NetPrice = 1000, TaxRate = 23, TaxValue = 230, GrossPrice = 1230 };
                this.Products.Add(product);
                this.SaveChanges();
            }

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ProductQuantity> ProductQuantities { get; set; }
    }

    public class ApiDBInitializer : DropCreateDatabaseIfModelChanges<ApiDBContext>
    {
        protected override void Seed(ApiDBContext context)
        {
            var product = new Product() { Name = "Bulbulator", NetPrice = 100, TaxRate = 5, TaxValue = 23, GrossPrice = 123 };
            context.Products.Add(product);
            product = new Product() { Name = "Trytytka", NetPrice = 10, TaxRate = 5, TaxValue = 0.5M, GrossPrice = 10.5M };
            context.Products.Add(product);
            product = new Product() { Name = "Mop ze spryskiwaczem Rovus", NetPrice = 200, TaxRate = 7, TaxValue = 14, GrossPrice = 214 };
            context.Products.Add(product);
            product = new Product() { Name = "Kultywator ręczny Tornadica Top Shop", NetPrice = 1000, TaxRate = 23, TaxValue = 230, GrossPrice = 1230 };
            context.Products.Add(product);
            product = new Product() { Name = "Godzina pracy programisty", NetPrice = 1000, TaxRate = 23, TaxValue = 230, GrossPrice = 1230 };
            context.Products.Add(product);
            context.SaveChanges();
        }
    }
}