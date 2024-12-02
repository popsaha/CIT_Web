namespace CIT_Web.Models.Reports
{
    public class Customer
    {
        public int statusCode { get; set; }
        public List<CustomerDetails> result { get; set; }

    }

    public class CustomerDetails
    {
        public string customerId { get; set; }
        public string customerName { get; set; }
        public string address { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
    }
}
