using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Branch;
using CIT_Web.Services.IServices;
using Microsoft.Extensions.Configuration;

namespace CIT_Web.Services
{
    public class BranchService : BaseService, IBranchService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;
        public BranchService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }
        public Task<T> CreateAsync<T>(BranchCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/Branch",
                //Token = token
            });
        }
        
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = citUrl + "/api/Branch/DeleteBranch" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Branch/GetAllbranch",
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Branch/GetBranch" + id,
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(BranchUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/Branch/" + dto.BranchID,
                //Token = token
            });
        }
    }
}
