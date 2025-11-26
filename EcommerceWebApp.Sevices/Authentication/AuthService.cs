using EcommerceWebApp.Entities.App.Context;
using EcommerceWebApp.Entities.Entities.UserEntities;
using EcommerceWebApp.Sevices.User_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b._EcommerceWebApp.Sevices.Authentication
{
    public interface IAuthService
    {
        Task<User?> GetUserByEmailAsync(string email, string pass);
        Task<User?> AddUserAsync(User user, int id);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDBContext _dbContext;
        private readonly IUserService _userService;

        public AuthService(AppDBContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<User?> AddUserAsync(User user, int id)
        {
            return await _userService.CreateAsync(user, id);
        }

        public async Task<User?> GetUserByEmailAsync(string email, string pass)
        {
            return await _userService.GetbyEmailAsync(email, pass);
        }
    }
}
