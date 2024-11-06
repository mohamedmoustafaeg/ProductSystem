using ProductSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProductSystem.BusinessLogic.ViewModels
{
    public class SelectOption
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<SelectOption> Categories { get; set; }
        public List<SelectOption> Users { get; set; }
    }
}
