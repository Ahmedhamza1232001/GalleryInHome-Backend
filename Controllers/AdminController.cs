using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj.Dtos.Product;
using Proj.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Proj.Services.UserService;

namespace Proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {


        private readonly IUserService UserService;
        public AdminController(IUserService userService)
        {
            this.UserService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<StakeholderGetProductDto>>>> Get()
        {
            return Ok(await this.UserService.GetAllUsers());
        }
    }
}