using System;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public Products Get()
        {
            return "Hello World";
            return new Products();
        }

        [HttpGet("{name}")]
        public Products Get(string name)
        {
            return new Products(name);
        }

        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            var product = new Product(id);
            if (product.IsNew)
                throw new Exception();

            return product;
        }

        [HttpPost]
        public void Post(Product product)
        {
            product.Save();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, Product product)
        {
            var orig = new Product(id)
            {
                Name            = product.Name,
                Description     = product.Description,
                Price           = product.Price,
                DeliveryPrice   = product.DeliveryPrice
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var product = new Product(id);
            product.Delete();
        }


    }
}