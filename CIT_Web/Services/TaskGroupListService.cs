using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Services.IServices;
using Microsoft.Extensions.Configuration;

namespace CIT_Web.Services
{
    public class TaskGroupListService :BaseService, ITaskGroupListService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;
        public TaskGroupListService(IHttpClientFactory clientFactory, IConfiguration configuration) :base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }
        public Task<T> GetAllGroupListAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/taskGroupList",
                //Token = token
            });
        }
    }
}
