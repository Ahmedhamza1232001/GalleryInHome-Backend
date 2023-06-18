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
        Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();
        Task<ServiceResponse<List<GetProductDto>>> GetStakeholderProducts();
        //Task<ServiceResponse<List<StakeholderGetProductDto>>> GetAll();
        Task<ServiceResponse<GetProductDto>> GetProductById(int id);
        //Task<ServiceResponse<List<StakeholderGetProductDto>>> AddProduct(StakeholderAddProductDto newProduct);
        Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct);
        Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);
        // Task<ServiceResponse<StakeholderGetProductDto>> AddProductMaterial(StakeholderAddProductMaterialDto newProductSkill);
    }
}