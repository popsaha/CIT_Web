namespace CIT_Web.Models.Reports
{
    public class Service
    {
        public int statusCode { get; set; }
        public List<ServiceList> result { get; set; }
    }

    public class ServiceList
    {
        public string pickupTypeId { get; set; }
        public string pickupTypeName { get; set; }
    }
}
