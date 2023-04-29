using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proj.Data;
using Proj.Dtos;
using Proj.Dtos.Product;

namespace Proj.Services.ProductService
{
    public class ProductService : IProductService
    {
        //make all of these private fields
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        //make exception here
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> AddProduct(StakeholderAddProductDto newProduct)
        {
            var serviceresponse = new ServiceResponse<List<StakeholderGetProductDto>>();
            Product product = _mapper.Map<Product>(newProduct);
            product.User = await _context.Users.FirstOrDefaultAsync(p => p.Id == GetUserId());
            _context.Add(product); //why didn't we use Addproductdto instate of product
            await _context.SaveChangesAsync();
            serviceresponse.Data = await _context.Products
            .Where(p => p.User.Id == GetUserId())
            .Select(p => _mapper.Map<StakeholderGetProductDto>(p))
            .ToListAsync();
            return serviceresponse;
        }

        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> GetStakeholderProducts()
        {
            var response = new ServiceResponse<List<StakeholderGetProductDto>>();
            var dbProducts = await _context.Products
            .Where(p => p.User.Id == GetUserId())
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .Include(p =>p.Images)
            .ToListAsync();
            //why we use mapping here
            response.Data = dbProducts.Select(p => _mapper.Map<StakeholderGetProductDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> GetAllUnAuth()
        {
            var response = new ServiceResponse<List<StakeholderGetProductDto>>();
            var dbProducts = await _context.Products
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .Include(p => p.Images)
            .ToListAsync();
            response.Data = dbProducts.Select(p => _mapper.Map<StakeholderGetProductDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<StakeholderGetProductDto>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<StakeholderGetProductDto>();
            var dbProduct = await _context.Products
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .FirstOrDefaultAsync(p => p.Id == id && p.User.Id == GetUserId());
            serviceresponse.Data = _mapper.Map<StakeholderGetProductDto>(dbProduct);
            return serviceresponse;
        }

        public async Task<ServiceResponse<StakeholderGetProductDto>> UpdateProduct(StakeholderUpdateProductDto updatedProduct)
        {
            ServiceResponse<StakeholderGetProductDto> response = new();

            try
            {

                var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                if (product.User.Id == GetUserId())
                {
                    _mapper.Map(updatedProduct, product);
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<StakeholderGetProductDto>(product);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product Not found";

                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;


        }

        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<StakeholderGetProductDto>> response = new();

            try
            {

                Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.User.Id == GetUserId());
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    response.Data = _context.Products
                    .Where(p => p.User.Id == GetUserId())
                    .Select(p => _mapper.Map<StakeholderGetProductDto>(p)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character Not Found";
                }



            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<StakeholderGetProductDto>> AddProductMaterial(StakeholderAddProductMaterialDto newProductMaterial)
        {
            var response = new ServiceResponse<StakeholderGetProductDto>();
            try
            {
                var product = await _context.Products
                .Include(p => p.Factory)
                .Include(p => p.Materials)
                .FirstOrDefaultAsync(p => p.Id == newProductMaterial.ProductId &&
                p.User.Id == GetUserId());


                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    return response;
                }
                var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == newProductMaterial.MaterialId);
                if (material is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }
                product.Materials.Add(material);
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

        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> GetAllProducts()
        {
            var response = new ServiceResponse<List<StakeholderGetProductDto>>();
            var dbProducts = await _context.Products
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .Include(p => p.Images)
            .ToListAsync();
            //why we use mapping here
            response.Data = dbProducts.Select(p => _mapper.Map<StakeholderGetProductDto>(p)).ToList();
            return response;
        }
    }
}