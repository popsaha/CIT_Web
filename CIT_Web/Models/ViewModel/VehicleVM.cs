using CIT_Web.Models.Dto.Vehicle;

namespace CIT_Web.Models.ViewModel
{
    public class VehicleVM
    {
        public VehicleCreateDTO VehicleForm { get; set; } // For form binding
        public List<VehicleDTO> Vehicles { get; set; }
    }
}
