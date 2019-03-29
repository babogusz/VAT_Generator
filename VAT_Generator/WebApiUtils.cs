using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace VAT_Generator
{
    public static class WebApiUtils
    {
        public static HttpClient WebApiClient = new HttpClient();

        static WebApiUtils()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:53217/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}