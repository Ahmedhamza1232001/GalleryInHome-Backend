using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos.Product;

namespace Proj.Services.ProductService
{
    public class ProductService : IProductService
    {
        private static List<Product> ProductList = new List<Product>()
        {
            new Product(),
            new Product(){Name = "Chair"}
        };

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceresponse = new ServiceResponse<List<GetProductDto>>();
            ProductList.Add(newProduct);
            serviceresponse.Data = ProductList;
            return serviceresponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            return new ServiceResponse<List<GetProductDto>>(){Data = ProductList};
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<GetProductDto>();
            var product = ProductList.FirstOrDefault(x => x.Id == id);
            serviceresponse.Data = product;
            return serviceresponse ;
        }
    }
}