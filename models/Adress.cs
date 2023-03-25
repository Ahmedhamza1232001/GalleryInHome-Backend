using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.models
{
    public class Adress
    {
        public int Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        //Relationships & NavigatoinProperties
        public User? User { get; set; }
        public int UserId { get; set; } //ForeignKey
    }
}