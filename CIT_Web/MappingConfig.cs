using AutoMapper;
using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Models.Dto.RoleMaster;
using CIT_Web.Models.Dto.Task;
using CIT_Web.Models.Dto.Vehicle;

namespace CIT_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CustomerDTO, CustomerCreateDTO>().ReverseMap();
            CreateMap<CustomerDTO, CustomerUpdateDTO>().ReverseMap();
            CreateMap<TaskDTO, TaskCreateDTO>().ReverseMap();
            CreateMap<CrewUserDTO, CrewUserCreateDTO>().ReverseMap();
            CreateMap<RoleMasterDTO, RoleListDTO>().ReverseMap();
            CreateMap<VehicleDTO, VehicleCreateDTO>().ReverseMap();
            CreateMap<VehicleDTO, VehicleUpdateDTO>().ReverseMap();
        }
    }
}
