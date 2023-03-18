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
        private readonly IMapper Mapper;
        private readonly DataContext Context;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public ProductService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
            this.Context = context;
            this.Mapper = mapper;
        }

        //make exception here 
        private int GetUserId() => int.Parse(this.HttpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> AddProduct(StakeholderAddProductDto newProduct)
        {
            var serviceresponse = new ServiceResponse<List<StakeholderGetProductDto>>();
            Product product = this.Mapper.Map<Product>(newProduct);
            product.User = await this.Context.Users.FirstOrDefaultAsync(p => p.Id == GetUserId());
            this.Context.Add(product); //why didn't we use Addproductdto instate of product
            await this.Context.SaveChangesAsync();
            serviceresponse.Data = await this.Context.Products
            .Where(p => p.User.Id == GetUserId())
            .Select(p => this.Mapper.Map<StakeholderGetProductDto>(p))
            .ToListAsync();
            return serviceresponse;
        }

        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> GetAllProducts()
        {
            var response = new ServiceResponse<List<StakeholderGetProductDto>>();
            var dbProducts = await this.Context.Products
            .Where(p => p.User.Id == GetUserId())
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .Include(p =>p.Images)
            .ToListAsync();
            //why we use mapping here 
            response.Data = dbProducts.Select(p => this.Mapper.Map<StakeholderGetProductDto>(p)).ToList();
            return response;
        }
        public async Task<ServiceResponse<List<StakeholderGetProductDto>>> GetAllUnAuth()
        {
            var response = new ServiceResponse<List<StakeholderGetProductDto>>();
            var dbProducts = await this.Context.Products
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .Include(p => p.Images)
            .ToListAsync();
            response.Data = dbProducts.Select(p => this.Mapper.Map<StakeholderGetProductDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<StakeholderGetProductDto>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<StakeholderGetProductDto>();
            var dbProduct = await this.Context.Products
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .FirstOrDefaultAsync(p => p.Id == id && p.User.Id == GetUserId());
            serviceresponse.Data = this.Mapper.Map<StakeholderGetProductDto>(dbProduct);
            return serviceresponse;
        }

        public async Task<ServiceResponse<StakeholderGetProductDto>> UpdateProduct(StakeholderUpdateProductDto updatedProduct)
        {
            ServiceResponse<StakeholderGetProductDto> response = new ServiceResponse<StakeholderGetProductDto>();

            try
            {

                var product = await this.Context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                if (product.User.Id == GetUserId())
                {
                    this.Mapper.Map(updatedProduct, product);
                    await this.Context.SaveChangesAsync();

                    response.Data = this.Mapper.Map<StakeholderGetProductDto>(product);
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
            ServiceResponse<List<StakeholderGetProductDto>> response = new ServiceResponse<List<StakeholderGetProductDto>>();

            try
            {

                Product product = await this.Context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.User.Id == GetUserId());
                if (product != null)
                {
                    this.Context.Products.Remove(product);
                    await this.Context.SaveChangesAsync();
                    response.Data = this.Context.Products
                    .Where(p => p.User.Id == GetUserId())
                    .Select(p => this.Mapper.Map<StakeholderGetProductDto>(p)).ToList();
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
                var product = await this.Context.Products
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
                var material = await this.Context.Materials.FirstOrDefaultAsync(m => m.Id == newProductMaterial.MaterialId);
                if (material is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }
                product.Materials.Add(material);
                await this.Context.SaveChangesAsync();
                response.Data = this.Mapper.Map<StakeholderGetProductDto>(product);
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