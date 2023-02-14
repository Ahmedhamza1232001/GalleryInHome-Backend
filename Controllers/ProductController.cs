using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj.models;   //to use specific folder | you could use global key word in C# 10

namespace Proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api word here is optional
    public class ProductController : ControllerBase
    {
        private static List<Product> ProductList = new List<Product>()
        {
            new Product(),
            new Product(){Name = "Chair"}   
        };
        [HttpGet] //when I use it it worked in swagger
        //but I Can make it work by typing the route in the browser
        public ActionResult<List<Product>> Get()
        {
            return Ok(ProductList);
        }


    }
}