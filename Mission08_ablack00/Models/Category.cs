using System.ComponentModel.DataAnnotations;

namespace Mission08_ablack00.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
