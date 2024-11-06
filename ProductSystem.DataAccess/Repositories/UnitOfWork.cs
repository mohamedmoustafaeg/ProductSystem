using ProductSystem.DataAccess.Data;
using ProductSystem.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public UnitOfWork(ApplicationDbContext context, IProductRepository products, ICategoryRepository categories)
        {
            _context = context;
            Products = products;
            Categories = categories;
        }
        public async Task<int> Compelete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
