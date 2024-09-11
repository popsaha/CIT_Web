using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIT_Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexCustomer()
        {
            List<CustomerDTO> list = new();

            var response = await _customerService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CustomerDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }




    }
}
