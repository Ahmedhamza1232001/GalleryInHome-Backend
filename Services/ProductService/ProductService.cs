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
        #region DependencyInjection
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }
        #endregion
        //make exception here
        private int GetStakeholderId() => int.Parse(_httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));

        // public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(StakeholderAddProductDto newProduct)
        // {
        //     var serviceresponse = new ServiceResponse<List<GetProductDto>>();
        //     Product product = _mapper.Map<Product>(newProduct);
        //     product.Stakeholder = await _context.Users.FirstOrDefaultAsync(p => p.Id == GetStakeholderId());
        //     _context.Add(product); //why didn't we use Addproductdto instate of product
        //     await _context.SaveChangesAsync();
        //     serviceresponse.Data = await _context.Products
        //     .Where(p => p.User.Id == GetUserId())
        //     .Select(p => _mapper.Map<GetProductDto>(p))
        //     .ToListAsync();
        //     return serviceresponse;
        // }

        public async Task<ServiceResponse<List<GetProductDto>>> GetStakeholderProducts()
        {
            var response = new ServiceResponse<List<GetProductDto>>();
            var dbProducts = await _context.Products
            .Where(p => p.Stakeholder.Id == GetStakeholderId())
            .Include(p => p.Materials)
            .Include(p =>p.Images)
            .ToListAsync();
            //why we use mapping here
            response.Data = dbProducts.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
            return response;
        }

        // public async Task<ServiceResponse<List<GetProductDto>>> GetAll()
        // {
        //     var response = new ServiceResponse<List<GetProductDto>>();
        //     var dbProducts = await _context.Products
        //     .Include(p => p.Factory)
        //     .Include(p => p.Materials)
        //     .Include(p => p.Images)
        //     .ToListAsync();
        //     response.Data = dbProducts.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
        //     return response;
        // }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<GetProductDto>();
            var dbProduct = await _context.Products
            .Include(p => p.Materials)
            .FirstOrDefaultAsync(p => p.Id == id && p.Stakeholder.Id == GetStakeholderId());
            serviceresponse.Data = _mapper.Map<GetProductDto>(dbProduct);
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> response = new();

            try
            {

                var product = await _context.Products
                .Include(p => p.Stakeholder)
                .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                if (product.Stakeholder.Id == GetStakeholderId())
                {
                    _mapper.Map(updatedProduct, product);
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetProductDto>(product);
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

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> response = new();

            try
            {

                Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.Stakeholder.Id == GetStakeholderId());
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    response.Data = _context.Products
                    .Where(p => p.Stakeholder.Id == GetStakeholderId())
                    .Select(p => _mapper.Map<GetProductDto>(p)).ToList();
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

        // public async Task<ServiceResponse<GetProductDto>> AddProductMaterial(StakeholderAddProductMaterialDto newProductMaterial)
        // {
        //     var response = new ServiceResponse<GetProductDto>();
        //     try
        //     {
        //         var product = await _context.Products
        //         .Include(p => p.Factory)
        //         .Include(p => p.Materials)
        //         .FirstOrDefaultAsync(p => p.Id == newProductMaterial.ProductId &&
        //         p.Stakeholder.Id == GetStakeholderId());


        //         if (product == null)
        //         {
        //             response.Success = false;
        //             response.Message = "Product not found.";
        //             return response;
        //         }
        //         var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == newProductMaterial.MaterialId);
        //         if (material is null)
        //         {
        //             response.Success = false;
        //             response.Message = "Skill not found.";
        //             return response;
        //         }
        //         product.Materials.Add(material);
        //         await _context.SaveChangesAsync();
        //         response.Data = _mapper.Map<GetProductDto>(product);
        //     }
        //     catch (Exception ex)
        //     {
        //         response.Success = false;
        //         response.Message = ex.Message;
        //     }
        //     return response;
        // }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> response = new();
            var dbProducts = await _context.Products
            .Include(p => p.Materials)
            .Include(p => p.Images)
            .ToListAsync();
            //why we use mapping here
            response.Data = dbProducts.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
            return response;
        }
    }
}