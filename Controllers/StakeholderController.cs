using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj.Dtos;
using Proj.Dtos.Product;
using Proj.models;   //to use specific folder | you could use global key word in C# 10
using Proj.Services.ProductService;

namespace Proj.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")] //api word here is optional
    public class StakeholderController : ControllerBase
    {
        private readonly IProductService _productService;
        public StakeholderController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> Get()
        {
            return Ok(await _productService.GetAllProducts());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<StakeholderGetProductDto>>> GetSingle(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> AddProduct(StakeholderAddProductDto product)
        {
            return Ok(await _productService.AddProduct(product));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> UpdateProduct(StakeholderUpdateProductDto updatedProduct)
        {
            var response = await _productService.UpdateProduct(updatedProduct);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("Material")]
        public async Task<ActionResult<ServiceResponse<StakeholderGetProductDto>>> AddProductMaterial(StakeholderAddProductMaterialDto newProductMaterial)
        {
            return Ok(await _productService.AddProductMaterial(newProductMaterial));
        }


    }
}