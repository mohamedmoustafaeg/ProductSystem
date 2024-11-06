using Microsoft.EntityFrameworkCore;
using ProductSystem.DataAccess.Data;
using ProductSystem.DataAccess.Interface;
using ProductSystem.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task Update(Product product, string userId)
        {
            var originalProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);

            if (originalProduct != null && !originalProduct.Equals(product))
            {
                var changes = $"Name: {originalProduct.Name} -> {product.Name}, Price: {originalProduct.Price} -> {product.Price}";
                var log = new ProductLog
                {
                    ProductId = product.Id,
                    UpdatedByUserId = userId,
                    UpdateDateTime = DateTime.Now,
                    Changes = changes
                };
                await _context.ProductLogs.AddAsync(log);
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
