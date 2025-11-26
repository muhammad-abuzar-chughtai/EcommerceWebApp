using EcommerceWebApp.Entities.App.Context;
using EcommerceWebApp.Entities.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Sevices.User_Service
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetbyIdAsync(int id);
        Task<Role> CreateAsync(Role role);
        Task<Role> UpdateAsync(Role role, int Id);
        Task<Role> DeleteAsync(int Id);
        Task<Role> HardDeleteAsync(int Id);
    }
    public class RoleService : IRoleService
    {
        private readonly AppDBContext _dbContext;
        public RoleService(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            try
            {
                    //Name
                    //Description
                    role.Users = new List<User>();
                    role.CreatedDate = DateTime.Now;
                    role.IsActive = true;

                    await _dbContext.AddAsync(role);
                    await _dbContext.SaveChangesAsync();
                    return role;
                
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Role> DeleteAsync(int Id)
        {
            var found = await GetbyIdAsync(Id);
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

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            try
            {
                    return await _dbContext.Roles.Where(r => r.IsActive).ToListAsync();
                
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Role?> GetbyIdAsync(int id)
        {
            try
            {
                    return await _dbContext.Roles.Where(r => r.IsActive && r.Id == id && r.Id != 5).FirstOrDefaultAsync();
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Role> HardDeleteAsync(int Id)
        {
            var found = await GetbyIdAsync(Id);
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

        public async Task<Role> UpdateAsync(Role role, int Id)
        {
            var found = await GetbyIdAsync(Id);
            if(found != null)
            {
                try
                {
                        found.Name = role.Name;
                        found.Description = role.Description;
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
