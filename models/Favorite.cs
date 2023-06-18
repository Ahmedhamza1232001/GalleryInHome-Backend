using System.Net.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proj.Api.models
{
    public class Favorite
    {
        public int Id { get; set; }
        [Required]
        public List<Client> Clients { get; set; } = new();
    }
}