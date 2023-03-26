using AutoMapper;
using System.Linq;
using todo.service.Infrastructure.Data.Interfaces;
using todo.service.Services.ToDo_.Data;
using todo.service.Services.ToDo_.DTOs;
using todo.service.Services.ToDo_.Interfaces;

namespace todo.service.Services.ToDo_.Implementations
{
    public class ToDoService : IToDoService
    {
        private readonly IRepository<ToDo> todoRepo;
        private readonly IMapper mapper;

        public ToDoService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            this.todoRepo = repositoryFactory.GetRepository<ToDo>();
            this.mapper = mapper;
        }

        public ToDoDto GetById(Guid id, Guid userId)
        {
            var result = this.todoRepo.GetById(id);
            if (result.OwnerId == userId)
            {
                return mapper.Map<ToDoDto>(result);
            }
            return null;
        }

        public IEnumerable<ToDoDto> GetByUser(Guid userId)
        {
            return mapper.ProjectTo<ToDoDto>(this.todoRepo.Query(td => td.OwnerId == userId && td.IsLatest == true).AsQueryable());
        }

        public ToDoDto Create(ToDoDto dto, Guid userId)
        {
            var now = DateTime.Now.ToString();
            var id = Guid.NewGuid();
            var newToDo = new ToDo()
            {
                Id = id,
                OwnerId = userId,
                Content = dto.Content,
                CreatedTimeStamp = now,
                UpdatedTimeStamp = now,
                OriginalPost = id,
                IsLatest = true,
            };
            return mapper.Map<ToDoDto>(this.todoRepo.Add(newToDo));
        }

        public ToDoDto Update(ToDoDto dto, Guid userId)
        {
            var update = this.todoRepo.GetById(dto.Id);
            if (update.OwnerId == userId)
            {
                var updated = new ToDo()
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    OriginalPost = dto.Id,
                    Content = dto.Content,
                    UpdatedTimeStamp = DateTime.Now.ToString(),
                    IsLatest = true,
                };
                update.IsLatest = false;
                this.todoRepo.Update(update);
                return mapper.Map<ToDoDto>(this.todoRepo.Add(updated));
            }
            return null;
        }

        public bool Delete(Guid id, Guid userId)
        {
            if (GetByUser(userId).FirstOrDefault(td => td.Id == id) != null)
            {
                return this.todoRepo.Delete(id);
            }
            return false;
        }
    }
}
