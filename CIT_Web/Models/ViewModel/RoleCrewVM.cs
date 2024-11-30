using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.RoleMaster;

namespace CIT_Web.Models.ViewModel
{
    public class RoleCrewVM
    {
        public List<RoleListDTO> Roles { get; set; } = new List<RoleListDTO>(); // Initialize with empty list
        public CrewUserCreateDTO CrewUser { get; set; } = new CrewUserCreateDTO(); // Ensure CrewUser is initialized
        public List<UserMasterDTO> userMasters { get; set; } = new List<UserMasterDTO>();

    }
}
