using EcommerceWebApp.DTO_s.InventoryModels;
using EcommerceWebApp.DTO_s.UserModels;
using EcommerceWebApp.Entities.Entities.Inventory;
using EcommerceWebApp.Entities.Entities.UserEntities;
using EcommerceWebApp.Entities.Mapping.Inventory;

namespace ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper
{
    public static class RoleModelHelper
    {
        public static RoleModel? ToModel(this Role role)
        {
            if (role == null)
                return null;

            var model = new RoleModel();
            model.Id = role.Id;
            model.Name = role.Name;
            model.Description = role.Description;

            return model;
        }

        public static List<RoleModel?> ToModel(this IEnumerable<Role> roles)
        {
            var models = new List<RoleModel?>();

            if (roles.Count() <= 0)
                return models;

            foreach (var Role in roles)
            {
                models.Add(Role.ToModel());
            }

            return models;
        }

        public static Role ToEntity(this RoleModel model)
        {
            var entity = new Role();
            entity.Id = 0;
            entity.Name = model.Name;
            entity.Description = model.Description;

            return entity;
        }

    }
}
