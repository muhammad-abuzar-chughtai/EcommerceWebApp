using EcommerceWebApp.Entities.App.Context;
using EcommerceWebApp.Entities.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Entities.Entities.Products
{
    public class Product : BaseEntity
    {
        public string? Description { get; set; }
        public int  Price { get; set; }
        public int  Stock { get; set; }
        public string ImageUrl { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Product() { }
    }
}
