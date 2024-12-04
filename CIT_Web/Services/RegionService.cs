using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Services.IServices;

namespace CIT_Web.Services
{
    public class RegionService : BaseService, IRegionService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public RegionService(IHttpClientFactory clientFactory, IConfiguration configuration) :base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }

        public Task<T> GetAllRegion<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Region",
            });
        }
    }
}
