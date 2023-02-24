using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos.Factory;
using Proj.Dtos.Product;

namespace Proj.Services.FactoryService
{
    public interface IFactoryService
    {
        Task<ServiceResponse<GetProductDto>> AddFactory(AddFactoryDto newFactory);
    }
}