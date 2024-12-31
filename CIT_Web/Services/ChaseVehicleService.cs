using CIT_Utility;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Models;
using CIT_Web.Services.IServices;
using Newtonsoft.Json;
using CIT_Web.Models.Dto.ChaseVehicle;

namespace CIT_Web.Services
{
    public class ChaseVehicleService :BaseService, IChaseVehicleService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public ChaseVehicleService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }


        public Task<T> CreateAsync<T>(ChaseVehicleCreateDTO dto)
        {
            Console.WriteLine($"Request URL: {citUrl}/api/ChaseVehicle");
            Console.WriteLine($"Payload: {JsonConvert.SerializeObject(dto)}");

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/ChaseVehicle",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int vehicleId, int userId)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,

                Url = $"{citUrl}/api/ChaseVehicle/{vehicleId}?deletedBy={userId}",
                //Token = token
            });
        }

        public Task<T> GetAllVehicleAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/ChaseVehicle",
                //Token = token
            });
        }

        //public Task<T> GetAsync<T>(int id)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.GET,
        //        Url = citUrl + "/api//" + id,
        //        //Token = token
        //    });
        //}

        public Task<T> UpdateAsync<T>(ChaseVehicleUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/ChaseVehicle/" + dto.VehicleID,
                //Token = token
            });
        }
        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/ChaseVehicle/" + id,

            });
        }
    }
}
