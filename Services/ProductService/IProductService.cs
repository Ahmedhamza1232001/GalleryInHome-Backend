using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.Services.ProductService
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        List<Product> AddProduct(Product newProduct);
    }
}