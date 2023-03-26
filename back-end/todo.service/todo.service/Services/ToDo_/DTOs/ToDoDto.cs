namespace todo.service.Services.ToDo_.DTOs
{
    public class ToDoDto
    {
        public Guid Id { get; set; }
        public Guid OriginalPost { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
