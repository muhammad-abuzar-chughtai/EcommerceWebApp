using EcommerceWebApp.Entities.App.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Entities.Entities.UserEntities
{
    public class Role : BaseEntity
    {
        public string? Description { get; set; }
        public ICollection<User> Users { get; set; }

        public Role() { }

    }
}
