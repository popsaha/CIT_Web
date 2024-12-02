using CIT_Web.Models.Reports;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CIT_Web.Services.IServices
{
    public interface IReportService
    {
        public List<Report> GetAllReportDetails();
        public List<Report> GetAllReportDetails(ReportDetailsParam obj);
        public List<SelectListItem> GetallList();
        public Task<T> CallReportData<T>();
        public Task<T> CallFilterReportData<T>(ReportDetailsParam obj);
        public Task<T> GetCustomerList<T>();
        public Task<T> CallBranchList<T>();
        public Task<T> CallPickupTypeList<T>();

    }
}
