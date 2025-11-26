using EcommerceWebApp.DTO_s.InventoryModels;
using EcommerceWebApp.Entities.Entities.Inventory;
using EcommerceWebApp.Entities.Mapping.Inventory;

namespace ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper
{
    public static class CategoryModelHelper
    {
        public static CategoryModel? ToModel(this Category category)
        {
            if (category == null)
                return null;

            var model = new CategoryModel();
            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;

            return model;
        }

        public static List<CategoryModel?> ToModel(this IEnumerable<Category> categorys)
        {
            var models = new List<CategoryModel?>();

            if (categorys.Count() <= 0)
                return models;

            foreach (var category in categorys)
            {
                models.Add(category.ToModel());
            }

            return models;
        }

        public static Category ToEntity(this CategoryModel model)
        {
            var entity = new Category();
            entity.Id = 0;
            entity.Name = model.Name;
            entity.Description = model.Description;

            return entity;
        }

    }
}
