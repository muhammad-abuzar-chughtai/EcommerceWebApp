using EcommerceWebApp.DTO_s.InventoryModels;
using EcommerceWebApp.Entities.Entities.Products;
using EcommerceWebApp.Entities.Mapping.Inventory;

namespace ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper
{
    public static class ProductModelHelper
    {
        public static ProductModel? ToModel(this Product product)
        {
            if (product == null)
                return null;

            var model = new ProductModel();
            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Stock = product.Stock;
            model.ImageUrl = product.ImageUrl;
            model.CategoryId = product.CategoryId;

            return model;
        }

        public static List<ProductModel?> ToModel(this IEnumerable<Product> products)
        {
            var models = new List<ProductModel?>();

            if (products.Count() <= 0)
                return models;

            foreach (var product in products)
            {
                models.Add(product.ToModel());
            }

            return models;
        }

        public static Product ToEntity(this ProductModel model)
        {
            var entity = new Product();
            entity.Id = 0;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Stock = model.Stock;
            entity.ImageUrl = model.ImageUrl;
            entity.CategoryId = model.CategoryId;

            return entity;
        }

    }
}
