using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using VAT_Generator.Models;

namespace VAT_Generator.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
            IEnumerable<Product> productList = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            return View(productList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            HttpResponseMessage response = WebApiUtils.WebApiClient.PostAsJsonAsync("Product", product).Result;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
            var product = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result.Where(p => p.ProductId == id).FirstOrDefault();
            
            if (product != null)
                return View(product);
            else
                return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            HttpResponseMessage response = WebApiUtils.WebApiClient.PutAsJsonAsync("Product/" + product.ProductId, product).Result;
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = WebApiUtils.WebApiClient.DeleteAsync("Product/" + id).Result;
            return RedirectToAction("Index");
        }

    }
}