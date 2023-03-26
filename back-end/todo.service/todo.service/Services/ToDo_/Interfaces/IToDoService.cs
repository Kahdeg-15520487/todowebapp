using todo.service.Services.ToDo_.DTOs;

namespace todo.service.Services.ToDo_.Interfaces
{
    public interface IToDoService
    {
        IEnumerable<ToDoDto> GetByUser(Guid userId);
        ToDoDto GetById(Guid id, Guid userId);
        ToDoDto Create(ToDoDto dto, Guid userId);
        ToDoDto Update(ToDoDto dto, Guid userId);
        bool Delete(Guid id, Guid userId);
    }
}
