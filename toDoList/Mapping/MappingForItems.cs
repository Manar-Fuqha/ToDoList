using AutoMapper;
using toDoList.Core.Models;
using toDoList.Core.Models.DTO.Items;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace toDoList.Api.Mapping
{
    public class MappingForItems : Profile
    {
        public MappingForItems()
        {

            CreateMap<ToDoItem, ItemForRetriveDTO>();
            CreateMap<ItemForRetriveDTO, ToDoItem>();
            CreateMap<ItemForEditDTO, ToDoItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) => (srcMember != null )));
            
        }
    }
}
