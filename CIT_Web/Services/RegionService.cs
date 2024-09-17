using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Region;
using CIT_Web.Services.IServices;

namespace CIT_Web.Services
{
    public class RegionService :BaseService, IRegionService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;
        public RegionService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(httpClient)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }
        public Task<T> CreateAsync<T>(RegionCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "api/Region"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = citUrl + "api/Region" + id,

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET ,
                Url = citUrl + "api/Region",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "api/Region" + id
            });
        }

        public Task<T> UpdateAsync<T>(RegionUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "api/Region" + dto.RegionID,
            });
        }
    }
}
