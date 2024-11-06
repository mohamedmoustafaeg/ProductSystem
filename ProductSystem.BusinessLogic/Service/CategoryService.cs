using Microsoft.EntityFrameworkCore;
using ProductSystem.BusinessLogic.Interface;
using ProductSystem.DataAccess.Data;
using ProductSystem.DataAccess.Interface;
using ProductSystem.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSystem.BusinessLogic.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _context;
        public CategoryService(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.GetAllCategories();
        }
    }
}
