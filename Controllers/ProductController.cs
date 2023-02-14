using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj.models;   //to use specific folder | you could use global key word in C# 10
using Proj.Services.ProductService;

namespace Proj.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")] //api word here is optional
    public class ProductController : ControllerBase
    {
        public IProductService ProductService { get; }
        public ProductController(IProductService productService)
        {
            this.ProductService = productService;

        }
        private static List<Product> ProductList = new List<Product>()
        {
            new Product(),
            new Product(){Name = "Chair"}   
        };
        [HttpGet("GetAll")] //when I use it it worked in swagger
        //but I Can make it work by typing the route in the browser
        //[Route("GetAll")]
        public ActionResult<List<Product>> Get()
        {
            return Ok(ProductService.GetAllProducts());
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetSingle(int id)
        {
            return Ok(ProductService.GetProductById(id));
        }
        [HttpPost]
        public ActionResult<List<Product>> AddProduct(Product product)
        {
            
            return Ok(ProductService.AddProduct(product));
        }



    }
}