using EcommerceWebApp.Entities.App.Context;
using EcommerceWebApp.Entities.Entities.Inventory;
using EcommerceWebApp.Entities.Entities.Products;
using EcommerceWebApp.Entities.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Sevices.Inventory_Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category category);
        Task<Category> GetbyIdAsync(int Id);
        Task<Category> UpdateAsync(Category category, int Id);
        Task<Category> DeleteAsync(int Id);
        Task<Category> HardDeleteAsync(int Id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly AppDBContext _dbContext;
        public CategoryService(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            try
            {
                    //Name
                    //Desription
                    category.Products = new List<Product>();
                    category.Id = 0;
                    category.CreatedDate = DateTime.Now;
                    category.IsActive = true;

                    await _dbContext.AddAsync(category);
                    await _dbContext.SaveChangesAsync();
                    return category;

            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Category> DeleteAsync(int Id)
        {
            var found = await GetbyIdAsync(Id);
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                
                    return await _dbContext.Categories.Where(c => c.IsActive).OrderByDescending(c => c.CreatedDate).ToListAsync();
                
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Category?> GetbyIdAsync(int Id)
        {
            try
            {
                
                    return await _dbContext.Categories.Where(c => c.IsActive && c.Id == Id).FirstOrDefaultAsync();
                
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Category> HardDeleteAsync(int Id)
        {
            var found = await GetbyIdAsync(Id);
            if (found != null)
            {
                try
                {
                    using (AppDBContext dBContext = new AppDBContext())
                    {
                        _dbContext.Remove(found);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX.Message);
                    throw;
                }
            }
            return found;
        }

        public async Task<Category> UpdateAsync(Category category, int Id)
        {
            var found = await GetbyIdAsync(Id);
            if (found != null)
            {
                try
                {
                    
                        found.Name = category.Name;
                        found.Description = category.Description;
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
