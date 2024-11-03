using CIT_Web.Models.Dto.Login;

namespace CIT_Web.Services.IServices
{
    public interface ILoginService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate);
        public LoginResponseDTO GetLoginDetails(LoginRequestDTO objToCreate, int Refresh);
    }
}
