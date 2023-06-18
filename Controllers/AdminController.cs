// using System.Net;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Proj.Dtos.Product;
// using Proj.Services.ProductService;
// using Microsoft.AspNetCore.Authorization;


// namespace Proj.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     [Authorize]
//     public class AdminController : ControllerBase
//     {


//         private readonly IProductService _productService; // need to make a User service
//         public AdminController(IProductService userService)
//         {
//             _userService = userService;
//         }

//         [HttpGet("GetAll")]
//         public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> Get()
//         {
//             return Ok(await _userService.GetAllUsers());
//         }
//     }
// }