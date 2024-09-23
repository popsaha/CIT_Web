namespace CIT_Web.Services.IServices
{
    public interface ICrewCommanderService
    {
        Task<T> GetAllCrewCommanderList<T>();
    }
}
