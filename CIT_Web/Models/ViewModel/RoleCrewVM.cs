using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.Region;
using CIT_Web.Models.Dto.RoleMaster;
using CIT_Web.Models.Dto.User;

namespace CIT_Web.Models.ViewModel
{
    public class RoleCrewVM
    {
        public List<RoleListDTO> Roles { get; set; } = new List<RoleListDTO>(); // Initialize with empty list
        public CrewUserCreateDTO CrewUser { get; set; } = new CrewUserCreateDTO(); // Ensure CrewUser is initialized
        public List<UserMasterDTO> userMasters { get; set; } = new List<UserMasterDTO>();

        public List<RegionDTO> Regions { get; set; } = new List<RegionDTO>();

        public CrewUpdateDTO updateDTO { get; set; } = new CrewUpdateDTO();

    }
}
