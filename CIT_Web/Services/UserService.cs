using CIT_Utility;
using CIT_Web.Models.Dto.User;
using CIT_Web.Services.IServices;
using static CIT_Utility.SD;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using CIT_Web.Models;
using Newtonsoft.Json;

namespace CIT_Web.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public UserService(IHttpClientFactory clientFactory, IConfiguration configuration):base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }
        public Task<T> CreateAsync<T>(UserCreateDTO userCreate)
        {
            Console.WriteLine($"Service URL: {citUrl}/UsersAuth/CreateUser");
            Console.WriteLine($"Payload: {JsonConvert.SerializeObject(userCreate)}");

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userCreate,
                Url = citUrl + "/api/LocalUser/CreateLocalUser",
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/LocalUser",
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(UserUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = citUrl + "/api/LocalUser/" + dto.UserId,
                //Token = token
            });
        }
    }
}
