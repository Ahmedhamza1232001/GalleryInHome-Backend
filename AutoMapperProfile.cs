using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Proj.Dtos;
using Proj.Dtos.Product;
using Proj.Dtos.Factory;
using Proj.Dtos.Material;
using Proj.Dtos.User;

namespace Proj
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, StakeholderGetProductDto>();
            CreateMap<StakeholderAddProductDto, Product>();
            CreateMap<StakeholderUpdateProductDto, Product>();
            CreateMap<Factory, GetFactoryDto>();
            CreateMap<Material, GetMaterialDto>();
            CreateMap<User, UserDto>();

        }
    }
}