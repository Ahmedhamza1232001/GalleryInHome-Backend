using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos.Product;

namespace Proj.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();
        Task<ServiceResponse<GetProductDto>> GetProductById(int id);
        Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);
    }
}