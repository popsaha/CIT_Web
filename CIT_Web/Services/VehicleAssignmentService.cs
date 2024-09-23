using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.VehicleAssignment;
using CIT_Web.Services.IServices;
using Microsoft.Extensions.Configuration;

namespace CIT_Web.Services
{
    public class VehicleAssignmentService : BaseService, IVehicleAssignmentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;
        public VehicleAssignmentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }
        public Task<T> AddGroupAssignmentAsync<T>(VehicleAssignmentRequestDTO vehicleAssignment)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = vehicleAssignment,
                Url = citUrl + "/api/VehicleAssignment/AddAssignOrder",
                //Token = token
            });
        }

        public Task<T> GetAllTaskVehicleAssignmentAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/VehicleAssignment/GetAllAssignOrder",
                //Token = token
            });
        }
    }
}
