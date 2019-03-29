using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using VAT_Generator.Models;

namespace VAT_Generator.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Invoice> invoiceList;
            HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Invoice").Result;
            invoiceList = resp.Content.ReadAsAsync<IEnumerable<Invoice>>().Result;
            return View(invoiceList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Invoice invoice)
        {
            HttpResponseMessage response = WebApiUtils.WebApiClient.PostAsJsonAsync("Invoice", invoice).Result;
            var newInvoice = response.Content.ReadAsAsync<Invoice>().Result;

            return RedirectToAction("AddProducts", new { id = newInvoice.InvoiceId });
        }


        public ActionResult Delete(int id)
        {
            WebApiUtils.WebApiClient.DeleteAsync("Invoice/" + id);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult AddProducts(int id)
        {
            HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
            var products = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            ViewData["InvoiceId"] = id;
            return View(products);
        }


        [HttpGet]
        public ActionResult EditProducts(int id)
        {
            HttpResponseMessage response = WebApiUtils.WebApiClient.GetAsync("ProductQuantity").Result;
            var quantities = response.Content.ReadAsAsync<IEnumerable<ProductQuantity>>().Result.Where(q => q.InvoiceId == id);

            HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
            var products = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            var ProductQuantityViewModel = from p in products
                                   join q in quantities on p.ProductId equals q.ProductId into pq
                                   from q in pq.DefaultIfEmpty()
                                   select new ProductQuantityViewModel { ProductQuantity = q, Product = p };
            
            ViewData["InvoiceId"] = id;
            return View(ProductQuantityViewModel);
        }


        [HttpPost]
        public ActionResult AddProducts(int invoiceId, FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(',');
            string[] quantities = formCollection["quantity"].Split(',');

            var resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
            var productList = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            resp = WebApiUtils.WebApiClient.GetAsync("Invoice").Result;
            var invoice = resp.Content.ReadAsAsync<IEnumerable<Invoice>>().Result.Where(i => i.InvoiceId == invoiceId).FirstOrDefault();

            resp = WebApiUtils.WebApiClient.GetAsync("ProductQuantity").Result;
            var productQuantities = resp.Content.ReadAsAsync<IEnumerable<ProductQuantity>>().Result.Where(q => q.InvoiceId == invoice.InvoiceId);

            if (invoice != null)
            {
                ProductQuantity productQuantity = null;
                for (int i = 0; i < ids.Count(); i++)
                {
                    int ID = -1;
                    int quantity = -1;
                    if (int.TryParse(ids[i], out ID) && int.TryParse(quantities[i], out quantity) && quantity > 0)
                    {
                        var product = productList.Where(p => p.ProductId == ID).FirstOrDefault();
                        if (product != null)
                        {
                            productQuantity = productQuantities.Where(q => q.ProductId == product.ProductId).FirstOrDefault();
                            if (productQuantity == null)
                            {
                                productQuantity = new ProductQuantity();
                                productQuantity.ProductId = product.ProductId;
                                productQuantity.Quantity = quantity;
                                productQuantity.InvoiceId = invoice.InvoiceId;
                                HttpResponseMessage response = WebApiUtils.WebApiClient.PostAsJsonAsync("ProductQuantity", productQuantity).Result;
                            }
                            else
                            {
                                productQuantity.Quantity = quantity;
                                HttpResponseMessage response = WebApiUtils.WebApiClient.PutAsJsonAsync("ProductQuantity/" + productQuantity.ProductQuantityId, productQuantity).Result;
                            }
                        }
                    }
                    else if (ID >= 0)
                    {
                        productQuantity = productQuantities.Where(q => q.ProductId == ID).FirstOrDefault();
                        if (productQuantity != null)
                            WebApiUtils.WebApiClient.DeleteAsync("ProductQuantity/" + productQuantity.ProductQuantityId);
                    }
                }

                return RedirectToAction("Details", new { id = invoiceId });
            }

            return RedirectToAction("Index");
        }


        public ActionResult Details(int id, bool partial = false)
        {
            HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Invoice/" + id.ToString()).Result;
            var invoice = resp.Content.ReadAsAsync<Invoice>().Result;
            
            resp = WebApiUtils.WebApiClient.GetAsync("ProductQuantity").Result;
            invoice.ProductQuantities = resp.Content.ReadAsAsync<IEnumerable<ProductQuantity>>().Result.Where(p => p.InvoiceId == id).ToList();
            
            resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
            var allProducts = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result.ToList();

            foreach(var quantity in invoice.ProductQuantities)
            {
                var product = allProducts.Where(p => p.ProductId == quantity.ProductId).FirstOrDefault();
                if (product != null)
                    quantity.Product = product;
            }


            var NetSum = invoice.ProductQuantities.Where(q => q.Product.TaxRate == invoice.ProductQuantities.First().Product.TaxRate).Sum(q => q.Product.NetPrice * q.Quantity);
            var rates = invoice.ProductQuantities.Select(q => q.Product.TaxRate).Distinct();

            if (invoice != null)
            {
                if (partial)
                    return PartialView("_InvoiceDetails", invoice);
                else
                    return View(invoice);

            }
            return RedirectToAction("Index");
        }

        public ActionResult SaveAsPdf(int id)
        {
            return new Rotativa.ActionAsPdf("Details", new { id = id, partial = true });
        }
    }
}