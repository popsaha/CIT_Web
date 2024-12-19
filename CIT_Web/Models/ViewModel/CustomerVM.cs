using CIT_Web.Models.Dto.Customer;

namespace CIT_Web.Models.ViewModel
{
    public class CustomerVM
    {
        //public List<CustomerListDTO> customerslist {  get; set; }
        public CustomerCreateDTO createDTO { get; set; }
        public List<Dto.Customer.CustomerDTO> customers {  get; set; }
    }
}
