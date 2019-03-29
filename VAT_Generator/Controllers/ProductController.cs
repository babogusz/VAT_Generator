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
        // GET: Product
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Index_Get()
        {
            IEnumerable<Product> productList;
            HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
            //var result = resp.Content.ReadAsStringAsync().Result;
            //productList = JsonConvert.DeserializeObject<IEnumerable<Product>>(result);
            productList = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            return View(productList);
        }

        //// POST: Product
        //[HttpPost]
        //[ActionName("Index")]
        //public ActionResult Index_Post(IEnumerable<Product> productList)
        //{
        //    //IEnumerable<ProductModel> productList;
        //    //HttpResponseMessage resp = WebApiUtils.WebApiClient.GetAsync("Product").Result;
        //    //productList = resp.Content.ReadAsAsync<IEnumerable<ProductModel>>().Result;
        //    return View(productList);
        //}
        
    }
}