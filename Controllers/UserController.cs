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
    [Authorize]
    public class UserController:ControllerBase
    {
        private readonly IProductService ProductService;
        public UserController(IProductService productService)
        {
            this.ProductService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> Get()
        {
            return Ok(await this.ProductService.GetAllProducts());
        }
    }
}