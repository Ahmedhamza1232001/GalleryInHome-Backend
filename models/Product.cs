using System.Runtime.ConstrainedExecution;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; } //need to be float not double 
        public int  Discount { get; set; }
        public ColorClass Color { get; set; } = ColorClass.White; //when should I use Enums
        public List<Image> Images { get; set; } = new List<Image>();
        public int Height { get; set; }   //need to be float
        public int Width { get; set; }    //need to be float 
        public int Depth { get; set; }      //need to be float 
        public string MadeIn { get; set; } = string.Empty;
        public int Warranty { get; set; }
        public List<Material> Materials { get; set; } = new List<Material>();
        public bool IsFavo { get; set; } //delete this 
        //public User? User { get; set; }
        public Factory? Factory { get; set; } 
        public bool InStock { get; set; } = true;
        public string Model { get; set; } = string.Empty;
        public string Review { get; set; } = string.Empty;
        public string Reel { get; set; } = string.Empty;
        //one to many relationship
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();

        //many to many relationship
        public List<User> Users { get; set; } = new List<User>();

        //this is from mergebranch test 
        


    }
}
