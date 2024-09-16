using CIT_Web.Models.Dto.Branch;


namespace CIT_Web.Services.IServices
{
    public interface IBranchService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(BranchCreateDTO dto);
        Task<T> UpdateAsync<T>(BranchUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
