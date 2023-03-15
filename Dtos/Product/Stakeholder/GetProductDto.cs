using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos.Factory;
using Proj.Dtos.Material;

namespace Proj.Dtos.Product
{
    public class StakeholderGetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Discount { get; set; }
        public ColorClass Color { get; set; } = ColorClass.White;
        public List<Image> Images { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public string MadeIn { get; set; } = string.Empty;
        public int Warranty { get; set; }
        public bool IsFavo { get; set; }
        public GetFactoryDto Factory { get; set; }
        public List<GetMaterialDto> Materials { get; set; }
        public bool InStock { get; set; } = true;
        public string Model { get; set; } = string.Empty;
        public string Review { get; set; } = string.Empty;

    }
}