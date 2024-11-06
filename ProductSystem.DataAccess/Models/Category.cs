using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }

        public static explicit operator Category(int v)
        {
            throw new NotImplementedException();
        }
    }
}
