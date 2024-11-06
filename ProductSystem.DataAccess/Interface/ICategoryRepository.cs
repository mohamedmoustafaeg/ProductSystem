using ProductSystem.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
    }
}
