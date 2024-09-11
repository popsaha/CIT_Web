using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Services.IServices;

namespace CIT_Web.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public CustomerService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");

        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Customer",
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Customer/" + id,
                //Token = token
            });
        }

        public Task<T> CreateAsync<T>(CustomerCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/Customer",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = citUrl + "/api/Customer" + id,
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CustomerUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/Customer/" + dto.CustomerId,
                //Token = token
            });
        }
    }
}
