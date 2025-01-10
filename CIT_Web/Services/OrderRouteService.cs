using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.OrderRoute;
using CIT_Web.Services.IServices;

namespace CIT_Web.Services
{
    public class OrderRouteService : BaseService, IOrderRouteService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public OrderRouteService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }

        public Task<T> CreateAsync<T>(OrderRouteCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/OrderRoute",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int routeId, int deletedBy)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{citUrl}/api/OrderRoute/{routeId}/?deletedBy={deletedBy}",
                //Token = token
            });
        }

        public Task<T> GetAllOrderRouteList<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/OrderRoute/GetAllOrderRoutes",
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(OrderRouteUpdateDTOs dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/OrderRoute/" + dto.OrderRouteId,
                //Token = token
            });
        }
    }
}
