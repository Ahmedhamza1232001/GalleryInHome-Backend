using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Proj.Dtos;
using Proj.Dtos.Product;

namespace Proj.Services.ProductService
{
    public class ProductService : IProductService
    {
        public IMapper Mapper { get; }
        public ProductService(IMapper mapper)
        {
            this.Mapper = mapper;

        }
        private static List<Product> ProductList = new List<Product>()
        {
            new Product(),
            new Product(){Name = "Chair"}
        };

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceresponse = new ServiceResponse<List<GetProductDto>>();
            Product product = this.Mapper.Map<Product>(newProduct);
            product.Id = ProductList.Max(p => p.Id) + 1;
            ProductList.Add(product); //why didn't we use Addproductdto instate of product
            serviceresponse.Data = ProductList.Select(p => this.Mapper.Map<GetProductDto>(p)).ToList();
            return serviceresponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            return new ServiceResponse<List<GetProductDto>>() { Data = ProductList.Select(p => this.Mapper.Map<GetProductDto>(p)).ToList() };
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<GetProductDto>();
            var product = ProductList.FirstOrDefault(x => x.Id == id);
            serviceresponse.Data = this.Mapper.Map<GetProductDto>(product);
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();

            try
            {

                Product product = ProductList.FirstOrDefault(p => p.Id == updatedProduct.Id);
                this.Mapper.Map(updatedProduct, product);
                // product.Name = updatedProduct.Name;
                // product.Color = updatedProduct.Color;
                // product.Depth = updatedProduct.Depth;
                // product.Description = updatedProduct.Description;
                // product.Discount = updatedProduct.Discount;
                // product.Height = updatedProduct.Height;
                // product.Width = updatedProduct.Width;
                // product.Price = updatedProduct.Price;

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

                Product product = ProductList.First(p => p.Id == id);
                ProductList.Remove(product);
                response.Data = ProductList.Select(p => this.Mapper.Map<GetProductDto>(p)).ToList();

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