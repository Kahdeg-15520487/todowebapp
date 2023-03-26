using AutoMapper;
using todo.service.Services.Authentication.Data;
using todo.service.Services.Authentication.DTOs;

namespace todo.service.Services.Authentication.DataMapping
{
    public class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            UserInfoProfile();
        }
        private void UserInfoProfile()
        {
            this.CreateMap<User, UserInfoDto>()
                .ForMember(
                            ent => ent.Id,
                            map => map.MapFrom(src => src.Id))
                .ForMember(
                            ent => ent.UserName,
                            map => map.MapFrom(src => src.Username))
                ;
        }
    }
}
