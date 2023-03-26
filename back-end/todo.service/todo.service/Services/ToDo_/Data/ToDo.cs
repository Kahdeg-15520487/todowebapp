using todo.service.Infrastructure.Data;

namespace todo.service.Services.ToDo_.Data
{
    public class ToDo : DataObject
    {
        public Guid OwnerId { get; set; }
        public Guid OriginalPost { get; set; }
        public string Content { get; set; }
        public string CreatedTimeStamp { get; set; }
        public string UpdatedTimeStamp { get; set; }
        public bool IsLatest { get; set; }
    }
}
