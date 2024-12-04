using CIT_Web.Models.Dto.Region;
using CIT_Web.Models.Dto.RoleMaster;
using CIT_Web.Models.Dto.User;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CIT_Web.Models.ViewModel
{
    public class UserRoleVM
    {
        public List<RoleListDTO> Roles { get; set; } = new List<RoleListDTO>();
        public List<RegionDTO> Regions { get; set; } = new List<RegionDTO>();
        public List<UserDTO> Users { get; set; }
        public UserCreateDTO createDTO { get; set; } = new UserCreateDTO();

        public UserUpdateDTO updateDTO { get; set; } = new UserUpdateDTO();
    }
}
