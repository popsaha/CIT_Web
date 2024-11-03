namespace CIT_Web.Services.IServices
{
    public interface IOrderRouteService
    {
        Task<T> GetAllOrderRouteList<T>();
    }
}
