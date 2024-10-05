namespace CIT_Web.Services.IServices
{
    public interface IOrderService
    {
        Task<T> GetAllAsync<T>();
    }
}
