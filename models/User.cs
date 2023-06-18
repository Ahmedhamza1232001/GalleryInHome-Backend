using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.models
{
    public abstract class User
    {

        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; //what is the difference between string.empty and ""
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>(); //I'll change the intilization of this  and make exception //new byte[] { }
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        // public List<string>? Cart { get; set; }
        // public List<string>? Favorite { get; set; }
        //public Adress? Adress { get; set; }


        //Relationships
        public List<Product>? Products { get; set; }


    }
}