using AutoMapper;
using todo.service.Services.ToDo_.Data;
using todo.service.Services.ToDo_.DTOs;

namespace todo.service.Services.ToDo_.DataMapping
{
    public class ToDoMappingProfile : Profile
    {
        public ToDoMappingProfile()
        {
            ToDoProfile();
        }

        private void ToDoProfile()
        {
            this.CreateMap<ToDo, ToDoDto>()
                .ForMember(
                            ent => ent.Id,
                            map => map.MapFrom(src => src.Id))
                .ForMember(
                            ent => ent.OriginalPost,
                            map => map.MapFrom(src => src.OriginalPost))
                .ForMember(
                            ent => ent.Content,
                            map => map.MapFrom(src => src.Content))
                .ForMember(
                            ent => ent.TimeStamp,
                            map => map.MapFrom(src => DateTime.Parse(src.UpdatedTimeStamp)))
                ;
        }
    }
}
