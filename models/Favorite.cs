using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.models
{
    public class Favorite
    {
        public float Stars { get; set; }

        //Navigation properties
        public User? User { get; set; }
        public Product? Product { get; set; }

        //foreign keys
        public int UserId { get; set; }
        public int ProductId { get; set; }

    }
}