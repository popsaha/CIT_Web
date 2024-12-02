using Microsoft.AspNetCore.Mvc.Rendering;

namespace CIT_Web.Models.Reports
{
    public class Report
    {
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string PickupTypeName { get; set; }
        public string DistanceKM { get; set; }
        public string Trip { get; set; }
        public string Bill { get; set; }
        public List<Report> result { get; set; }
    }

    public class ReportDetailsParam
    {
        public string Customerid { get; set; }
        public string Branchid { get; set; }
        public string PickupTypeid { get; set; }
        public string fromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class ViewModel
    {
        public List<SelectListItem> DroDownData { get; set; }
        public List<Report> ReportData { get; set; }
    }
}
