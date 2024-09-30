using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Models.Dto.Task;
using CIT_Web.Services.IServices;

namespace CIT_Web.Services
{
    public class TaskService : BaseService, ItaskService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public TaskService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }

        public Task<T> GetAllOrderTypeAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/OrderType",
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Customer",
            });
        }
        public Task<T> GetAsync<T>(int CustomerId)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Task/GetBranchById?CustomerId=" + CustomerId,
                //Token = token
            });
        }

        public Task<T> CreateAsync<T>(TaskCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/Task",
                //Token = token
            });
        }

        public Task<T> GetAllVaultLocationAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Task/GetVaultLocation",
            });
        }
        public Task<T> GetOrderRoutesAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Task/GetOrderRoutes",
            });
        }

        public Task<T> GetOrderTaskAsync<T>(string OrderNumber)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Task/GetOrderTaskData?OrderNumber=" + OrderNumber,
            });
        }
    }
}
