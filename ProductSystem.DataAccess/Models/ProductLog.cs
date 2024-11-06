using System;

namespace ProductSystem.DataAccess.Models
{
    public class ProductLog
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string? Changes { get; set; }

        public Product? Product { get; set; }
        public ApplicationUser? UpdatedByUser { get; set; }
    }
}
