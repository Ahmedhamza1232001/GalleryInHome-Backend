using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set; }
    }
}