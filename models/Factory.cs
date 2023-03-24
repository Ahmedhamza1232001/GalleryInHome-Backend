using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.models
{
    //one to one relationship
    public class Factory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public int ProductId { get; set; }

    }
}