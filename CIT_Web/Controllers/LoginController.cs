using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Login;
using CIT_Web.Models.Dto.Task;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace CIT_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        private readonly IMapper mapper;
        public LoginController(ILoginService login_Service, IMapper _mapper)
        {
            loginService = login_Service;
            mapper = _mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM loginVM)
        {
            try
            {
                LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO();

                loginRequestDTO.UserName = loginVM.UserName;
                loginRequestDTO.Password = loginVM.Password;
                int Refresh = 0;

                var TaskcreateDTO = mapper.Map<LoginRequestDTO>(loginRequestDTO);

                loginResponseDTO =  loginService.GetLoginDetails(TaskcreateDTO, Refresh);

                //var response = await loginService.LoginAsync<APIResponse>(TaskcreateDTO);

                //if (response != null && response.IsSuccess)
                //{
                //    loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                //}
                if (loginResponseDTO != null)
                {
                    string tokenValue = loginResponseDTO.Token.Replace("Bearer", "");

                    var decodedToken = Jose.JWT.Payload(tokenValue);

                    var Decode = JsonConvert.DeserializeObject<Root>(Convert.ToString(decodedToken));
                    loginResponseDTO.Role = Decode.role;
                    loginResponseDTO.User.Password = loginVM.Password;

                    HttpContext.Session.SetString("LoginUserDetailsSession", JsonConvert.SerializeObject(loginResponseDTO));
                }
                return RedirectToAction("Index", "Task");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Logout()
        {
            //Removes all keys and values from the session-state collection.
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
