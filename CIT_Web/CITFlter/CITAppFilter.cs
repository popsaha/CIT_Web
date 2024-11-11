using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Login;
using CIT_Web.Models.Dto.Task;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CIT_Web.CITFlter
{
    public class CITAppFilter : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO();
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();

            var httpcontextAccessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();

            if (httpcontextAccessor.HttpContext.Session.GetString("LoginUserDetailsSession") != null)
            {
                loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(httpcontextAccessor.HttpContext.Session.GetString("LoginUserDetailsSession"));

                httpcontextAccessor.HttpContext.Session.SetString("LoginUserDetailsSession", JsonConvert.SerializeObject(loginResponseDTO));

                //var LoginSer = context.HttpContext.RequestServices.GetRequiredService<ILoginService>();

                //loginRequestDTO.UserName = loginResponseDTO.User.UserName;
                //loginRequestDTO.Password = loginResponseDTO.User.Password;

                //loginResponseDTO = LoginSer.GetLoginDetails(loginRequestDTO);

                //if (loginResponseDTO != null)
                //{
                //    string tokenValue = loginResponseDTO.Token.Replace("Bearer", "");

                //    var decodedToken = Jose.JWT.Payload(tokenValue);

                //    var Decode = JsonConvert.DeserializeObject<Root>(Convert.ToString(decodedToken));
                //    loginResponseDTO.Role = Decode.role;

                //    httpcontextAccessor.HttpContext.Session.SetString("LoginUserDetailsSession", JsonConvert.SerializeObject(loginResponseDTO));

                //}             
            }
            else
            {
                context.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}
