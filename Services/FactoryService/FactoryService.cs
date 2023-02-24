using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proj.Data;
using Proj.Dtos.Factory;
using Proj.Dtos.Product;

namespace Proj.Services.FactoryService
{
    public class FactoryService : IFactoryService
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public FactoryService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<GetProductDto>> AddFactory(AddFactoryDto newFactory)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();
            try
            {
                Product product = await this.context.Products
                .FirstOrDefaultAsync(p => p.Id == newFactory.ProductId &&
                p.User.Id == int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    return response;
                }

                Factory factory = new Factory{
                    Name = newFactory.Name,
                    Product = product
                };



            this.context.Factories.Add(factory);
                await this.context.SaveChangesAsync();
                response.Data = this.mapper.Map<GetProductDto>(product);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
    }
}