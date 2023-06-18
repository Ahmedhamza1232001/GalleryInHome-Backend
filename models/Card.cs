using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.Api.models
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public List<Client> Clients { get; set; } = new();
    }
}