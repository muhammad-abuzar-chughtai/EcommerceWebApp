using EcommerceWebApp.Entities.App.Context;
using EcommerceWebApp.Entities.Entities.UserEntities;
using EcommerceWebApp.Sevices.User_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Sevices.User_Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetbyEmailAsync(string email, string pass);
        Task<User> CreateAsync(User user, int Id);
        Task<User> UpdateAsync(User user, int Id);
        Task<User> DeleteAsync(int id);
        Task<User> HardDeleteAsync(int id);
    }
    public class UserService : IUserService
    {
        private readonly AppDBContext _dbContext;
        public UserService(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }


        public async Task<User> CreateAsync(User user, int Id)
        {
            try
            {
                //Name
                //Email + Password
                //Contact No.
                //ImageURL
                // Security Ques/Ans

                user.Id = 0;
                user.RoleId = Id;
                user.CreatedDate = DateTime.Now;
                user.IsActive = true;

                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return await _dbContext.Users.Where(u => u.Id == user.Id).Include(u => u.Role).FirstOrDefaultAsync();
                //return user.;
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<User> DeleteAsync(int id)
        {
            var found = await GetByIdAsync(id);
            if (found != null)
            {
                try
                {
                    found.IsActive = false;

                    _dbContext.Update(found);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX.Message);
                    throw;
                }
            }
            return found;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Users.Where(u => u.IsActive).OrderByDescending(u => u.CreatedDate).ToListAsync();
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Users.Where(u => u.IsActive && u.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                throw;
            }
        }

        public async Task<User?> GetbyEmailAsync(string email, string pass)
        {
            try
            {
                return await _dbContext.Users.Where(u => u.IsActive && u.Email == email && u.Password == pass).Include(u => u.Role).FirstOrDefaultAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                throw;
            }
        }

        public async Task<User> HardDeleteAsync(int id)
        {
            var found = await GetByIdAsync(id);
            if (found != null)
            {
                try
                {
                    _dbContext.Remove(found);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX.Message);
                    throw;
                }
            }
            return found;
        }

        public async Task<User> UpdateAsync(User user, int Id)
        {
            var found = await GetByIdAsync(Id);
            if (found != null)
            {
                try
                {
                    found.Name = user.Name;
                    found.Role = user.Role;
                    found.Email = user.Email;
                    found.Password = user.Password;
                    found.ContactNumber = user.ContactNumber;
                    found.ImageURL = user.ImageURL;
                    found.SecurityQuestion = user.SecurityQuestion;
                    found.SecurityAnswer = user.SecurityAnswer;

                    found.LastModifiedDate = DateTime.Now;

                    _dbContext.Update(found);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX.Message);
                    throw;
                }
            }
            return found;
        }
    }
}





