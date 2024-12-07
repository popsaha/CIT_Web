using CIT_Utility;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Login;
using CIT_Web.Models.Reports;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;


namespace CIT_Web.Services
{
    public class ReportService : BaseService,IReportService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string citUrl;

        public ReportService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            citUrl = configuration.GetValue<string>("ServiceUrls:CitAPI");
        }

        public List<Report> GetAllReportDetails()
        {
            //List<Report> _Report = new List<Report>();
            var ReportData = CallReportData<Report>();
           // _Report = JsonConvert.DeserializeObject<List<Report>>(Convert.ToString(ReportData));
            return ReportData.Result.result;
        }

        public List<Report> GetAllReportDetails(ReportDetailsParam obj)
        {
            //List<Report> _Report = new List<Report>();
            var ReportData = CallFilterReportData<Report>(obj);
            //_Report = JsonConvert.DeserializeObject<List<Report>>(Convert.ToString(ReportData));
            return ReportData.Result.result;
        }


        public List<SelectListItem> GetallList()
        {
            List<SelectListItem> listCust = new List<SelectListItem>();

            var CustomerGroup = new SelectListGroup { Name = "Customer" };
            var ServicesGroup = new SelectListGroup { Name = "Service" };
            var BranchGroup = new SelectListGroup { Name = "Branch" };
            
            var Custresponse =  GetCustomerList<Customer>();
            if (Custresponse != null)
            {
                for (int i = 0; i < Custresponse.Result.result.Count; i++)
                {
                    listCust.Add(new SelectListItem { Text = Custresponse.Result.result[i].customerName, Value = Custresponse.Result.result[i].customerId, Group = CustomerGroup });
                }
            }

            var Branchresponse = CallBranchList<Branch>();
            if (Branchresponse != null)
            {
               for (int i = 0; i < Branchresponse.Result.result.Count; i++)
                {
                    listCust.Add(new SelectListItem { Text = Branchresponse.Result.result[i].branchName, Value = Branchresponse.Result.result[i].branchID, Group = BranchGroup });
                }
            }

            var Serviceresponse = CallPickupTypeList<Service>();
            if (Serviceresponse != null)
            {
                for (int i = 0; i < Serviceresponse.Result.result.Count; i++)
                {
                    listCust.Add(new SelectListItem { Text = Serviceresponse.Result.result[i].pickupTypeName, Value = Serviceresponse.Result.result[i].pickupTypeId, Group = ServicesGroup });
                }
            }

            return listCust;
        }


        public Task<T> CallReportData<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Report/GetReportsData", //"http://localhost:5112/GetReportsData",
            });
        }

        public Task<T> CallFilterReportData<T>(ReportDetailsParam obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = citUrl + "/api/Report/GetFilterReportsData", //"http://localhost:5112/GetFilterReportsData",
            });
        }

        public Task<T> GetCustomerList<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Customer",
            });
        }


        public Task<T> CallBranchList<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/Branch/GetAllbranch",
            });
        }


        public Task<T> CallPickupTypeList<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = citUrl + "/api/PickupType",
            });
        }

       
    }
}
