using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Police;
using CIT_Web.Services.IServices;
using static CIT_Utility.SD;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace CIT_Web.Services
{
    public class PoliceService : BaseService, IPoliceService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public PoliceService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }

        public Task<T> CreateAsync<T>(PoliceCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/Police",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = citUrl + "/api/Police/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllPoliceAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Police",
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(PoliceUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/Police/" + dto.PoliceId,
                //Token = token
            });
        }
    }
}
