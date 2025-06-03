namespace SimpleTodoApi.Models
{
    public class Todo
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}