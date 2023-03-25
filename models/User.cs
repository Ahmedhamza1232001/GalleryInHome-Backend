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
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[] { }; //I'll change this and make exception
        public byte[] PasswordSalt { get; set; } = new byte[] { };
        //public Adress? Adress { get; set; }


        //Relationships & navigationProperty

        //one to many
        public List<Product>? Products { get; set; }
        //need to put here ForeignKey

        public Adress Adress { get; set; } = new Adress();
        


    }
}