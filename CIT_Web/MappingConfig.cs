using AutoMapper;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Models.Dto.Task;

namespace CIT_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CustomerDTO, CustomerCreateDTO>().ReverseMap();
            CreateMap<CustomerDTO, CustomerUpdateDTO>().ReverseMap();
            CreateMap<TaskDTO, TaskCreateDTO>().ReverseMap();
        }
    }
}
