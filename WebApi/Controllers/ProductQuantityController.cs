using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductQuantityController : ApiController
    {
        private ApiDBContext db = new ApiDBContext();

        // GET: api/ProductQuantity
        public IQueryable<ProductQuantity> GetProductQuantities()
        {
            return db.ProductQuantities;
        }

        // GET: api/ProductQuantity/5
        [ResponseType(typeof(ProductQuantity))]
        public IHttpActionResult GetProductQuantity(int id)
        {
            ProductQuantity productQuantity = db.ProductQuantities.Find(id);
            if (productQuantity == null)
            {
                return NotFound();
            }

            return Ok(productQuantity);
        }

        // PUT: api/ProductQuantity/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductQuantity(int id, ProductQuantity productQuantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productQuantity.ProductQuantityId)
            {
                return BadRequest();
            }

            db.Entry(productQuantity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductQuantityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductQuantity
        [ResponseType(typeof(ProductQuantity))]
        public IHttpActionResult PostProductQuantity(ProductQuantity productQuantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductQuantities.Add(productQuantity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productQuantity.ProductQuantityId }, productQuantity);
        }

        // DELETE: api/ProductQuantity/5
        [ResponseType(typeof(ProductQuantity))]
        public IHttpActionResult DeleteProductQuantity(int id)
        {
            ProductQuantity productQuantity = db.ProductQuantities.Find(id);
            if (productQuantity == null)
            {
                return NotFound();
            }

            db.ProductQuantities.Remove(productQuantity);
            db.SaveChanges();

            return Ok(productQuantity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductQuantityExists(int id)
        {
            return db.ProductQuantities.Count(e => e.ProductQuantityId == id) > 0;
        }
    }
}