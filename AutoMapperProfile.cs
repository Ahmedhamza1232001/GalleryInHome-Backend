using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Proj.Dtos;
using Proj.Dtos.Product;
using Proj.Dtos.Factory;
using Proj.Dtos.Material;

namespace Proj
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Factory, GetFactoryDto>();
            CreateMap<Material, GetMaterialDto>();

        }
    }
}