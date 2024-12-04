using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.User;

namespace CIT_Web.Services.IServices
{
    public interface IUserService
    {
        Task<T> GetAllAsync<T>();
        Task<T> CreateAsync<T>(UserCreateDTO userCreate);
        Task<T> UpdateAsync<T>(UserUpdateDTO dto);
        Task<T> GetByIdAsync<T>(int id);
        Task<T> DeleteAsync<T>(int userId, int deletedBy);
    }
}
