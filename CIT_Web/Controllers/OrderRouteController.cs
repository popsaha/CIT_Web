using CIT_Web.Models.Dto.Police;
using CIT_Web.Models.ViewModel;
using CIT_Web.Models;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CIT_Web.Models.Dto.OrderRoute;

namespace CIT_Web.Controllers
{
    public class OrderRouteController : Controller
    {
        private readonly IOrderRouteService _orderRouteService;

        public OrderRouteController(IOrderRouteService orderRouteService)
        {
            _orderRouteService = orderRouteService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> OrderRouteIndex()
        {
            var routeList = new List<OrderRouteDTO>();
            var routeListResponse = await _orderRouteService.GetAllOrderRouteList<APIResponse>();

            if (routeListResponse != null && routeListResponse.IsSuccess)
            {
                routeList = JsonConvert.DeserializeObject<List<OrderRouteDTO>>(Convert.ToString(routeListResponse.Result));
            }

            var model = new OrderRouteVM
            {
                orderRouteCreateDTOs = new OrderRouteCreateDTO(), // Empty form for new vehicle
                OrderRoutesList = routeList // Existing vehicle list
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> OrderRouteIndex(OrderRouteVM model)
        {
            var response = await _orderRouteService.CreateAsync<APIResponse>(model.orderRouteCreateDTOs);
            if (response != null && response.IsSuccess)
            {
                // Redirect to the same action to avoid form resubmission
                TempData["SuccessMessage"] = "Route saved successfully!";
                return RedirectToAction(nameof(OrderRouteIndex));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error saving Route.");
            }

            // If something goes wrong, fetch the updated Police list
            var orderRouteListResponse = await _orderRouteService.GetAllOrderRouteList<APIResponse>();
            model.OrderRoutesList = orderRouteListResponse != null && orderRouteListResponse.IsSuccess
                ? JsonConvert.DeserializeObject<List<OrderRouteDTO>>(Convert.ToString(orderRouteListResponse.Result))
                : new List<OrderRouteDTO>();
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var response = await _orderRouteService.GetAllOrderRouteList<APIResponse>();
            if (response != null && response.IsSuccess)
            {  
                var routes = JsonConvert.DeserializeObject<List<OrderRouteUpdateDTOs>>(Convert.ToString(response.Result));
                var route = routes.FirstOrDefault(v => v.OrderRouteId == id);
                if (route != null)
                {
                    return View(route);
                }
            }

            TempData["ErrorMessage"] = "Route not found!";
            return RedirectToAction(nameof(OrderRouteIndex));
        }




        [HttpPost]
        public async Task<IActionResult> EditRoute(OrderRouteUpdateDTOs dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input!";
                return View(dto); // Return to the edit view with the current data
            }

            var response = await _orderRouteService.UpdateAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Order Route updated successfully!";
                return RedirectToAction(nameof(OrderRouteIndex));
            }

            TempData["ErrorMessage"] = "Failed to update Route.";
            return View(dto); // Return to the edit view with the current data
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoute(int orderRouteId)
        {
            int userId = 1;
            if (orderRouteId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid User ID.";
                return RedirectToAction(nameof(OrderRouteIndex));
            }

            var response = await _orderRouteService.DeleteAsync<APIResponse>(orderRouteId, userId);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Route deleted successfully!";
                return RedirectToAction(nameof(OrderRouteIndex));
            }

            TempData["ErrorMessage"] = "Failed to delete Route.";
            return RedirectToAction(nameof(OrderRouteIndex));
        }
    }
}
