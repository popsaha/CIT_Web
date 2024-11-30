using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Services.IServices;
using Newtonsoft.Json;

namespace CIT_Web.Services
{
    public class CrewCommanderMasterService : BaseService, ICrewCommanderMasterService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;
        public CrewCommanderMasterService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }

        public Task<T> CreateAsync<T>(CrewUserCreateDTO userCreateDTO)
        {
            Console.WriteLine($"Service URL: {citUrl}/UsersAuth/CreateUser");
            Console.WriteLine($"Payload: {JsonConvert.SerializeObject(userCreateDTO)}");

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userCreateDTO,
                Url = citUrl + "/api/UsersAuth/CreateUser",
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/UsersAuth/GetAllUser",
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CrewUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/UsersAuth/" + dto.UserId,
                //Token = token
            });
        }
    }
}
