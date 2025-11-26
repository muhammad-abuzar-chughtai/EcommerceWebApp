using EcommerceWebApp.DTO_s.InventoryModels;
using EcommerceWebApp.DTO_s.UserModels;
using EcommerceWebApp.Entities.Entities.Inventory;
using EcommerceWebApp.Entities.Entities.UserEntities;
using EcommerceWebApp.Entities.Mapping.Inventory;

namespace ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper
{
    public static class UserModelHelper
    {
        public static UserModel? ToModel(this User user)
        {
            if (user == null)
                return null;

            var model = new UserModel();
            model.Id = user.Id;
            model.Name = user.Name;
            model.ContactNumber = user.ContactNumber;
            model.ImageURL = user.ImageURL;
            model.SecurityQuestion = user.SecurityQuestion;
            model.SecurityAnswer = user.SecurityAnswer;

            model.Email = user.Email;
            model.Password = user.Password;
            model.RoleId = user.RoleId;
            model.RoleName = user.Role.Name;

            return model;
        }
        public static UserModel? ToSafeModel(this User user)
        {
            if (user == null)
                return null;

            var model = new UserModel();
            model.Id = user.Id;
            model.Name = user.Name;
            model.ContactNumber = user.ContactNumber;
            model.ImageURL = user.ImageURL;

            model.Email = user.Email;
            model.RoleId = user.RoleId;
            model.RoleName = user.Role.Name;


            return model;
        }

        public static List<UserModel?> ToModel(this IEnumerable<User> users)
        {


            var models = new List<UserModel?>();

            if (users.Count() <= 0)
                return models;

            foreach (var user in users)
            {
                models.Add(user.ToModel());
            }

            return models;
        }

        public static User ToEntity(this UserModel model)
        {
            var entity = new User();
            entity.Id = 0;
            entity.Name = model.Name;
            entity.ContactNumber = model.ContactNumber;
            entity.ImageURL = model.ImageURL;
            entity.SecurityQuestion = model.SecurityQuestion;
            entity.SecurityAnswer = model.SecurityAnswer;

            entity.Email = model.Email;
            entity.Password= model.Password;
            entity.RoleId = model.RoleId;


            return entity;
        }

    }
}
