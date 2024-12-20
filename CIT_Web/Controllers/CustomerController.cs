using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services;
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
            var list = new List<CIT_Web.Models.Dto.Customer.CustomerDTO>();

            var response = await _customerService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CIT_Web.Models.Dto.Customer.CustomerDTO>>(Convert.ToString(response.Result));
            }
            var model = new CustomerVM
            {
                createDTO = new CustomerCreateDTO(), // Empty form for new vehicle
                customers = list
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexCustomer(CustomerVM model)
        {
            var response = await _customerService.CreateAsync<APIResponse>(model.createDTO);

            if (response != null && response.IsSuccess)
            {
                // Redirect to the same action to avoid form resubmission
                TempData["SuccessMessage"] = "Customer saved successfully!";
                return RedirectToAction(nameof(IndexCustomer));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error saving Customer.");
            }

            var customerListResponse = await _customerService.GetAllAsync<APIResponse>();
            model.customers = customerListResponse != null && customerListResponse.IsSuccess
                ? JsonConvert.DeserializeObject<List<CIT_Web.Models.Dto.Customer.CustomerDTO>>(Convert.ToString(customerListResponse.Result))
                : new List<CIT_Web.Models.Dto.Customer.CustomerDTO>();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customerService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                var customer = JsonConvert.DeserializeObject<List<CustomerUpdateDTO>>(Convert.ToString(response.Result));
                var cust = customer.FirstOrDefault(v => v.CustomerId == id);
                if (cust != null)
                {
                    return View(cust);
                }
            }

            TempData["ErrorMessage"] = "Customer not found!";
            return RedirectToAction(nameof(IndexCustomer));
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(CustomerUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input!";
                return View(dto); // Return to the edit view with the current data
            }

            var response = await _customerService.UpdateAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Customer updated successfully!";
                return RedirectToAction(nameof(IndexCustomer));
            }

            TempData["ErrorMessage"] = "Failed to update Customer.";
            return View(dto); // Return to the edit view with the current data
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            int userId = 1;
            if (customerId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Customer ID.";
                return RedirectToAction(nameof(IndexCustomer));
            }

            var response = await _customerService.DeleteAsync<APIResponse>(customerId, userId);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Customer deleted successfully!";
                return RedirectToAction(nameof(IndexCustomer));
            }

            TempData["ErrorMessage"] = "Failed to delete Customer.";
            return RedirectToAction(nameof(IndexCustomer));
        }

    }
}
