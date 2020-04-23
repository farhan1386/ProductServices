using System.ComponentModel.DataAnnotations;

namespace ProductServices.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
