using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.DTO_s.InventoryModels
{
    public class ProductModel : BaseDto
    {
        public string? Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }



        public ProductModel() { }
    }
}
