using ProductSystem.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Interface
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Update(Product product, string userId);
        Task Delete(int id);
    }
}
