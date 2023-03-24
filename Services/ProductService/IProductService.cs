using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos;
using Proj.Dtos.Product;

namespace Proj.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<StakeholderGetProductDto>>> GetAllProducts();
        Task<ServiceResponse<List<StakeholderGetProductDto>>> GetStakeholderProducts();
        Task<ServiceResponse<List<StakeholderGetProductDto>>> GetAllUnAuth();
        Task<ServiceResponse<StakeholderGetProductDto>> GetProductById(int id);
        Task<ServiceResponse<List<StakeholderGetProductDto>>> AddProduct(StakeholderAddProductDto newProduct);
        Task<ServiceResponse<StakeholderGetProductDto>> UpdateProduct(StakeholderUpdateProductDto updatedProduct);
        Task<ServiceResponse<List<StakeholderGetProductDto>>> DeleteProduct(int id);
        Task<ServiceResponse<StakeholderGetProductDto>> AddProductMaterial(StakeholderAddProductMaterialDto newProductSkill);
    }
}