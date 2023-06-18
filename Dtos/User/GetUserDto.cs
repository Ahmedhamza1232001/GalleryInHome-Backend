using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos.Product;

namespace Proj.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public List<GetProductDto>? Products { get; set; }
    }
}