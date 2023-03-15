// using System.Reflection;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using Proj.Dtos.Product;
// using Proj.Services.ProductService;

// namespace Proj.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class AdminController:ControllerBase
//     {
        

//         public IProductService ProductService { get; }
//         public AdminController(IProductService productService)
//         {
//             this.ProductService = productService;

//         }
//         [AllowAnonymous]
//         [HttpGet("GetAllProducts")]
//         public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetAll()
//         {

//             return Ok(await this.ProductService.GetAllUnAuth());
//         }
//         [HttpPost]
//         public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> AddProduct(AddProductDto product)
//         {
//             return Ok(await this.ProductService.AddProduct(product));
//         }
//     }
// }