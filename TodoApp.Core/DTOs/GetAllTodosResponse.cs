namespace TodoApp.Core.DTOs
{
    public class GetAllTodosResponse
    {
        public string UserId { get; set; }

        public List<TodoDto> Todos { get; set; }
    }
}