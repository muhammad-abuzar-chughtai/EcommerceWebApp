using EcommerceWebApp.Entities.App.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Entities.Entities.UserEntities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }

        public string ContactNumber { get; set; }
        public string ImageURL { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }

        public User() { }
    }
}
