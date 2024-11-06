using Microsoft.EntityFrameworkCore;
using ProductSystem.DataAccess.Data;
using ProductSystem.DataAccess.Interface;
using ProductSystem.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

    }
}
