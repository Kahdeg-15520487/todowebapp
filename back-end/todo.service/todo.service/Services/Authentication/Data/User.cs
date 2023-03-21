using todo.service.Infrastructure.Data;

namespace todo.service.Services.Authentication.Data
{
    public class User : DataObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
