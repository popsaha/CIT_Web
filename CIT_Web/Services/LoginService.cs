using CIT_Utility;
using CIT_Web.Models.Dto.Login;
using CIT_Web.Models;
using CIT_Web.Services.IServices;
using CIT_Web.Models.ViewModel;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Polly;

namespace CIT_Web.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string citUrl;
       
        public LoginService(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
            _httpContextAccessor = httpContextAccessor;
        }

        public LoginResponseDTO GetLoginDetails(LoginRequestDTO objToCreate, int Refresh)
        {
            LoginResponseDTO loginresponseDTO = new LoginResponseDTO();

            if (_httpContextAccessor.HttpContext.Session.GetString("LoginUserDetailsSession") != null)
                loginresponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(_httpContextAccessor.HttpContext.Session.GetString("LoginUserDetailsSession"));

            if (objToCreate.Password != null && Refresh == 0)
            {
                var Res = LoginAsync<APIResponse>(objToCreate);
                var response = Res.Result;
                if (response != null && response.IsSuccess)
                {
                    loginresponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                }
            }
            return loginresponseDTO;
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = citUrl + "/api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = citUrl + "/api/v1/UsersAuth/register"
            });
        }
    }
}
