using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.TaskGrouping;
using CIT_Web.Services.IServices;

namespace CIT_Web.Services
{
    public class TaskGroupService : BaseService, ITaskGroupService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public TaskGroupService(IHttpClientFactory clientFactory, IConfiguration configuration) :  base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }
        public Task<T> GetAllTaskGroupAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/TaskGroup/GetAllTaskGroup",
                //Token = token
            });
        }
       
        public Task<T> AddGroupAsync<T>(TaskGroupRequestDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = citUrl + "/api/TaskGroup/AddTaskGroup",
                //Token = token
            });
        }
    }
}
