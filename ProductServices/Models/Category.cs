using System.ComponentModel.DataAnnotations;

namespace ProductServices.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }
    }
}
