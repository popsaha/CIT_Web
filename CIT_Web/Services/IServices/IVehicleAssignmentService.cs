

using CIT_Web.Models.Dto.VehicleAssignment;

namespace CIT_Web.Services.IServices
{
    public interface IVehicleAssignmentService
    {
        Task<T> GetAllTaskVehicleAssignmentAsync<T>();
        Task<T> AddGroupAssignmentAsync<T>(VehicleAssignmentRequestDTO vehicleAssignment);
    }
}
