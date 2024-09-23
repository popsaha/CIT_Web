

namespace CIT_Web.Services.IServices
{
    public interface ITaskListService
    {
        
        Task<T> GetAllAsync<T>();
      
    }
}
