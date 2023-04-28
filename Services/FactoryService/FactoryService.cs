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
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public FactoryService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<StakeholderGetProductDto>> AddFactory(AddFactoryDto newFactory)
        {
            ServiceResponse<StakeholderGetProductDto> response = new();
            try
            {
                Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == newFactory.ProductId &&
                p.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    return response;
                }

                Factory factory = new()
                {
                    Name = newFactory.Name,
                    Product = product
                };



            _context.Factories.Add(factory);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<StakeholderGetProductDto>(product);
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