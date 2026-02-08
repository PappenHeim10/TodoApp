using System.ComponentModel.DataAnnotations;

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

        public string? UserId { get; set; }

    }
}