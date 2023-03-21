namespace todo.service.Services.ToDo.Data
{
    public class ToDo
    {
        public Guid OwnerId { get; set; }
        public string Content { get; set; }
        public string TimeStamp { get; set; }
    }
}
