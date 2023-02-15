using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.Services.ProductService
{
    public class ProductService : IProductService
    {
        private static List<Product> ProductList = new List<Product>()
        {
            new Product(),
            new Product(){Name = "Chair"}
        };

        public async Task<ServiceResponse<List<Product>>> AddProduct(Product newProduct)
        {
            var serviceresponse = new ServiceResponse<List<Product>>();
            ProductList.Add(newProduct);
            serviceresponse.Data = ProductList;
            return serviceresponse;
        }

        public async Task<ServiceResponse<List<Product>>> GetAllProducts()
        {
            return new ServiceResponse<List<Product>>(){Data = ProductList};
        }

        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            var serviceresponse = new ServiceResponse<Product>();
            var product = ProductList.FirstOrDefault(x => x.Id == id);
            serviceresponse.Data = product;
            return serviceresponse ;
        }
    }
}