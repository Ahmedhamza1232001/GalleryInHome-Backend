// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;

// namespace Proj.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class AccountController  : ControllerBase
//     {
//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly SignInManager<IdentityUser> _signInManager;
//       public AccountController (UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
//       {
//             _signInManager = signInManager;
//             _userManager = userManager;
//       }


//       [HttpPost]
//         public IActionResult Register()
//     }
// }