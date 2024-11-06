using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreationDate { get; set; }
        
        public DateTime? StartDate { get; set; }
        public int? Duration { get; set; }
        public decimal? Price { get; set; }
        [Required]
        [ValidateImageByteArray(1 * 1024 * 1024)]
        public byte[]? ImageData { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }
    }
}
