using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<List<Product>> AddProduct(Product newProduct);
    }
}