using System.ComponentModel.DataAnnotations;

namespace _2_TodoListWithLocalDb.Domains
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }
    }
}
