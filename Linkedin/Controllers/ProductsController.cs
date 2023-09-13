using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linkedin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Linkedin.Models;

namespace Linkedin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Our  product constructor
        public ProductsController(JsonFileProductService productService) {

            this.ProductService = productService;
       
        }
        
        public JsonFileProductService ProductService { get; }


        [HttpGet]

        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        // [HttpPost] "[FromBody]"
        [Route("Rate")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery] string ProductId,
            [FromQuery] int Raiting
            )
        {
            ProductService.AddRating(ProductId, Raiting);
            return Ok();
        }
    }
}
