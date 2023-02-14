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

        public List<Product> AddProduct(Product newProduct)
        {
            ProductList.Add(newProduct);
            return ProductList;
        }

        public List<Product> GetAllProducts()
        {
            return ProductList;
        }

        public Product GetProductById(int id)
        {
            return ProductList.FirstOrDefault(x => x.Id == id);
        }
    }
}