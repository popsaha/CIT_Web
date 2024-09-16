using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.OrderType;
using CIT_Web.Services.IServices;

namespace CIT_Web.Services
{
    public class OrderTypeService : BaseService, IOrderTypeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;
        public OrderTypeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }
        public Task<T> CreateAsync<T>(OrderTypeCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "api/OrderType",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = citUrl + "/api/OrderType" + id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/OrderType",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/OrderType" + id ,
            });
        }

        public Task<T> UpdateAsync<T>(OrderTypeUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/OrderType" + dto.OrderTypeID,
            });
        }
    }
}
