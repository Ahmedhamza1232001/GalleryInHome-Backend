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
        public ProductService(IMapper mapper , DataContext context)
        {
            this.Context = context;
            this.Mapper = mapper;

        }
        

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceresponse = new ServiceResponse<List<GetProductDto>>();
            Product product = this.Mapper.Map<Product>(newProduct);
            this.Context.Add(product); //why didn't we use Addproductdto instate of product
            await this.Context.SaveChangesAsync();
            serviceresponse.Data = await this.Context.Products
            .Select(p => this.Mapper.Map<GetProductDto>(p))
            .ToListAsync();
            return serviceresponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            var response = new ServiceResponse<List<GetProductDto>>();
            var dbProducts = await this.Context.Products.ToListAsync();
            response.Data = dbProducts.Select(p => this.Mapper.Map<GetProductDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<GetProductDto>();
            var dbProduct = await this.Context.Products.FirstOrDefaultAsync(x => x.Id == id);
            serviceresponse.Data = this.Mapper.Map<GetProductDto>(dbProduct);
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();

            try
            {

                var product = await this.Context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                this.Mapper.Map(updatedProduct, product);
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

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();

            try
            {

                Product product =await this.Context.Products.FirstAsync(p => p.Id == id);
                this.Context.Products.Remove(product);
                await this.Context.SaveChangesAsync();
                response.Data = this.Context.Products.Select(p => this.Mapper.Map<GetProductDto>(p)).ToList();

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