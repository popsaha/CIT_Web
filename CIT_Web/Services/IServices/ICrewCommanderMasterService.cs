using CIT_Web.Models.Dto.CrewCommanderMaster;


namespace CIT_Web.Services.IServices
{
    public interface ICrewCommanderMasterService
    {
        Task<T> GetAllAsync<T>();
        Task<T> CreateAsync<T>(CrewUserCreateDTO userCreateDTO);
        Task<T> UpdateAsync<T>(CrewUpdateDTO dto);
        //Task<T> DeleteAsync<T>(int id);
    }
}
