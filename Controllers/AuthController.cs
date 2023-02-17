using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj.Data;
using Proj.Dtos.User;

namespace Proj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            this.authRepo = authRepo;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await this.authRepo.Register(
                new User { UserName = request.UserName }, request.Password
            );
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}