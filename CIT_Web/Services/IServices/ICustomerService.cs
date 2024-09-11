using CIT_Web.Models.Dto.Customer;

namespace CIT_Web.Services.IServices
{
    public interface ICustomerService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CustomerCreateDTO dto);
        Task<T> UpdateAsync<T>(CustomerUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);

        //Task<T> GetAllAsync<T>(string token);
        //Task<T> GetAsync<T>(int id, string token);
        //Task<T> CreateAsync<T>(VillaCreateDTO dto, string token);
        //Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token);
        //Task<T> DeleteAsync<T>(int id, string token);
    }
}
