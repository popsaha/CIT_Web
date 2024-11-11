﻿using CIT_Web.Models.Dto.Customer;
using CIT_Web.Models.Dto.Task;

namespace CIT_Web.Services.IServices
{
    public interface ItaskService
    {
        Task<T> GetAllOrderTypeAsync<T>();
        Task<T> GetAllAsync<T>();
        Task<T> GetAllVaultLocationAsync<T>();
        Task<T> GetAsync<T>(int CustomerId);
        Task<T> CreateAsync<T>(TaskCreateDTO dto);
        Task<T> GetOrderRoutesAsync<T>();
        Task<T> GetOrderTaskAsync<T>(string OrderNumber);
        Task<T> GetEditTask_DetailsAsync<T>(int taskId);
    }
}
