using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Proj.Dtos;
using Proj.Dtos.Product;
using Proj.Dtos.Material;
using Proj.Dtos.User;

namespace Proj
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Material, GetMaterialDto>();
            CreateMap<User, GetUserDto>();

        }
    }
}