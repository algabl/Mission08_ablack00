using System;
using System.ComponentModel.DataAnnotations;

namespace Mission08_ablack00.Models
{
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [Range(1,4)]
        public int Quadrant { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool Completed { get; set; }

    }
}
