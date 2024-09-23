namespace CIT_Web.Models
{
    public class TaskGroupVehicleViewModel
    {
        public List<CIT_Web.Models.Dto.TaskGroupList.TaskGroupListDTO> TaskGroups { get; set; }
        public List<CIT_Web.Models.Dto.Vehicle.VehicleDTO> Vehicles { get; set; }

        public List<CIT_Web.Models.Dto.CrewCommander.CrewCommanderDTO> CrewCommanders { get; set; }
    }
}
