using AutoMapper;
using toDoList.Core.Models;
using toDoList.Core.Models.DTO.Register;

namespace toDoList.Api.Mapping
{
    public class MappingForUser : Profile
    {
        public MappingForUser()
        {

            CreateMap<RegisterRequestDTO, User>();

        }
    }
}
