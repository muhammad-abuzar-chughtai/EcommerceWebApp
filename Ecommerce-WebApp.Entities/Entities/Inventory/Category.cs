using EcommerceWebApp.Entities.App.Context;
using EcommerceWebApp.Entities.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Entities.Entities.Inventory
{
    public class Category : BaseEntity
    {
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; }

        public Category() { }
    }
}
