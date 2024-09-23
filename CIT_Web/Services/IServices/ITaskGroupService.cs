using CIT_Web.Models.Dto.TaskGrouping;

namespace CIT_Web.Services.IServices
{
    public interface ITaskGroupService
    {
        Task<T> GetAllTaskGroupAsync<T>();
        Task<T> AddGroupAsync<T>(TaskGroupRequestDTO dto);
    }
}
