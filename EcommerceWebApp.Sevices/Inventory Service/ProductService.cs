using EcommerceWebApp.Entities.App.Context;
using EcommerceWebApp.Entities.Entities.Inventory;
using EcommerceWebApp.Entities.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Sevices.Inventory_Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product product, int Id);
        Task<Product> GetbyIdAsync(int Id);
        Task<Product> UpdateAsync(Product product, int Id);
        Task<Product> DeleteAsync(int Id);
        Task<Product> HardDeleteAsync(int Id);
    }
    public class ProductService : IProductService
    {
        private readonly AppDBContext _dbContext;
        public ProductService(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Product> CreateAsync(Product product, int Id)
        {
            try
            {
                //Name
                //Description
                //Price
                //Stock
                //ImageUrl
                //Category + CategoryId

                product.Id = 0;
                product.CategoryId = Id;
                product.CreatedDate = DateTime.Now;
                product.IsActive = true;

                await _dbContext.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return product;

            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Product> DeleteAsync(int Id)
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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Products.Where(p => p.IsActive).OrderByDescending(p => p.CreatedDate).ToListAsync();

            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Product?> GetbyIdAsync(int Id)
        {
            try
            {

                return await _dbContext.Products.Where(p => p.IsActive && p.Id == Id).FirstOrDefaultAsync();

            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                throw;
            }
        }

        public async Task<Product> HardDeleteAsync(int Id)
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

        public async Task<Product> UpdateAsync(Product product, int Id)
        {
            var found = await GetbyIdAsync(Id);
            if (found != null)
            {
                try
                {
                    found.Name = product.Name;
                    found.Description = product.Description;
                    found.Price = product.Price;
                    found.Stock = product.Stock;
                    found.ImageUrl = product.ImageUrl;
                    found.CategoryId = product.CategoryId;

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
