using ProductSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSystem.BusinessLogic.Interface
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task Add(Product product);
        public Task<Product> GetById(int id);
        public Task Update(int Id, Product product, string userId);
        public Task DeleteById(int id);

    }
}
