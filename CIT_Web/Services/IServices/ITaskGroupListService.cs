namespace CIT_Web.Services.IServices
{
    public interface ITaskGroupListService
    {
        Task<T> GetAllGroupListAsync<T>();
    }
}
