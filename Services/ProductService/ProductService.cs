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
        public IMapper Mapper { get; }
        public DataContext Context { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public ProductService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
            this.Context = context;
            this.Mapper = mapper;

        }

        private int GetUserId() => int.Parse(this.HttpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceresponse = new ServiceResponse<List<GetProductDto>>();
            Product product = this.Mapper.Map<Product>(newProduct);
            product.User = await this.Context.Users.FirstOrDefaultAsync(p => p.Id == GetUserId());
            this.Context.Add(product); //why didn't we use Addproductdto instate of product
            await this.Context.SaveChangesAsync();
            serviceresponse.Data = await this.Context.Products
            .Where(p => p.User.Id == GetUserId())
            .Select(p => this.Mapper.Map<GetProductDto>(p))
            .ToListAsync();
            return serviceresponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            var response = new ServiceResponse<List<GetProductDto>>();
            var dbProducts = await this.Context.Products
            .Where(p => p.User.Id == GetUserId())
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .ToListAsync();
            response.Data = dbProducts.Select(p => this.Mapper.Map<GetProductDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<GetProductDto>();
            var dbProduct = await this.Context.Products
            .Include(p => p.Factory)
            .Include(p => p.Materials)
            .FirstOrDefaultAsync(p => p.Id == id && p.User.Id == GetUserId());
            serviceresponse.Data = this.Mapper.Map<GetProductDto>(dbProduct);
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();

            try
            {

                var product = await this.Context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                if (product.User.Id == GetUserId())
                {
                    this.Mapper.Map(updatedProduct, product);
                    await this.Context.SaveChangesAsync();

                    response.Data = this.Mapper.Map<GetProductDto>(product);
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
            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();

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
                    .Select(p => this.Mapper.Map<GetProductDto>(p)).ToList();
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

        public async Task<ServiceResponse<GetProductDto>> AddProductMaterial(AddProductMaterialDto newProductMaterial)
        {
            var response = new ServiceResponse<GetProductDto>();
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
                response.Data = this.Mapper.Map<GetProductDto>(product);
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