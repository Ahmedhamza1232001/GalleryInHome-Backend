using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proj.Services.FactoryService;
using Proj.Dtos.Product;
using Proj.Dtos.Factory;

namespace Proj.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FactoryController : ControllerBase
    {
        private readonly IFactoryService factoryService;

        public FactoryController(IFactoryService factoryService)
        {
            this.factoryService = factoryService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<StakeholderGetProductDto>>> AddFactory(AddFactoryDto newFactory)
        {
            return Ok(await this.factoryService.AddFactory(newFactory));
        }
    }
}