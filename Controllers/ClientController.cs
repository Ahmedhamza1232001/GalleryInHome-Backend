using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj.Dtos.Product;
using Proj.Services.ProductService;
using Microsoft.AspNetCore.Authorization;


namespace Proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClientController:ControllerBase
    {
        private readonly IProductService _productService;
        public ClientController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> Get()
        {
            return Ok(await _productService.GetAllProducts());
        }
       // [HttpPost]

        // public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> AddProduct(StakeholderAddProductDto product)
        // {
        //     return Ok(await _productService.AddProduct(product));
        // }


    }
}