using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Core.Entities
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Updated { get; set; }

        public bool Status { get; set; }

        [ForeignKey("User")]
        public required string Author { get; set; }

        [ForeignKey("User")]
        public required User UserId { get; set; }
    }
}