using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Services.IServices;
using Newtonsoft.Json;

namespace CIT_Web.Services
{
    public class VehicleService : BaseService, IVehicleService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public VehicleService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }

        public Task<T> CreateAsync<T>(VehicleCreateDTO dto)
        {
            Console.WriteLine($"Request URL: {citUrl}/api/Vehicle");
            Console.WriteLine($"Payload: {JsonConvert.SerializeObject(dto)}");

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/Vehicle",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = citUrl + "/api/Vehicle/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllVehicleAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Vehicle",
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

        public Task<T> UpdateAsync<T>(VehicleUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/Vehicle/" + dto.VehicleID,
                //Token = token
            });
        }
        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Vehicle/" + id, 
                                                     
            });
        }
    }
}
