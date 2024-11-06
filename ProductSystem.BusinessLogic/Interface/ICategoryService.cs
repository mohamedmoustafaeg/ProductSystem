using ProductSystem.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSystem.BusinessLogic.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
    }
}
