using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.models
{
    public class User
    {
        
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[] { }; //I'll change the intilization of this  and make exception 
        public byte[] PasswordSalt { get; set; } = new byte[] { };
        // public List<string>? Cart { get; set; } 
        // public List<string>? Favorite { get; set; }
        //public Adress? Adress { get; set; }


        //Relationships
        public List<Product>? Products { get; set; }


    }
}