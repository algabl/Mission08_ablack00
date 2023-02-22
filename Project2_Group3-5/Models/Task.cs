using System.ComponentModel.DataAnnotations;

namespace Project2_Group3_5.Models
{
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }

        public string DueDate { get; set; }

        [Required]
        [Range(1,4)]
        public int Quadrant { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool Completed { get; set; }

    }
}
