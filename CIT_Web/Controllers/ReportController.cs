using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using CIT_Web.Models.Reports;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using CIT_Web.Models;

namespace CIT_Web.Controllers
{
    public class ReportController : Controller
    {
        
        private readonly IReportService _ReportService;
        ViewModel _ViewModel = new ViewModel();
       
        public ReportController(IReportService reportService)
        {           
            _ReportService = reportService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View(getAlllist());
        }

        public async Task<ViewModel> getAlllist()
        {
            List<SelectListItem> listCust = new List<SelectListItem>();
            listCust = _ReportService.GetallList();
            _ViewModel.DroDownData = listCust;
            return _ViewModel;
        }


        public List<Report> GetAllReportData()
        {
            List<Report> _Report = new List<Report>();           
            _Report = _ReportService.GetAllReportDetails();
            return _Report;
        }


        public List<Report> GetFilterReports([FromBody] ReportDetailsParam _ObjParam)
        {
            List<Report> _Report = new List<Report>();
            _Report = _ReportService.GetAllReportDetails(_ObjParam);
            return _Report;
        }

    }   
}
