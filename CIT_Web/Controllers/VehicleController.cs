using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using CIT_Web.Services.IServices;
using CIT_Web.Models.ViewModel;

namespace CIT_Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: VehiclesIndex - to display the list and the form
        public async Task<IActionResult> VehiclesIndex()
        {
            var vehicleList = new List<VehicleDTO>();

            // Fetch the vehicle list from the API
            var response = await _vehicleService.GetAllVehicleAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                vehicleList = JsonConvert.DeserializeObject<List<VehicleDTO>>(Convert.ToString(response.Result));
            }

            var model = new VehicleVM
            {
                VehicleForm = new VehicleCreateDTO(), // Empty form for new vehicle
                Vehicles = vehicleList // Existing vehicle list
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VehiclesIndex(VehicleVM model)
        {
       
            
                // Save the new vehicle via API
                var response = await _vehicleService.CreateAsync<APIResponse>(model.VehicleForm);
                if (response != null && response.IsSuccess)
                {
                    // Redirect to the same action to avoid form resubmission
                    TempData["SuccessMessage"] = "Vehicle saved successfully!";
                    return RedirectToAction(nameof(VehiclesIndex));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error saving vehicle.");
                }
            

            // If something goes wrong, fetch the updated vehicle list
            var vehicleListResponse = await _vehicleService.GetAllVehicleAsync<APIResponse>();
            model.Vehicles = vehicleListResponse != null && vehicleListResponse.IsSuccess
                ? JsonConvert.DeserializeObject<List<VehicleDTO>>(Convert.ToString(vehicleListResponse.Result))
                : new List<VehicleDTO>();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _vehicleService.GetAllVehicleAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                var vehicles = JsonConvert.DeserializeObject<List<VehicleUpdateDTO>>(Convert.ToString(response.Result));
                var vehicle = vehicles.FirstOrDefault(v => v.VehicleID == id);
                if (vehicle != null)
                {
                    return View(vehicle);
                }
            }

            TempData["ErrorMessage"] = "Vehicle not found!";
            return RedirectToAction(nameof(VehiclesIndex));
        }

        [HttpPost]
        public async Task<IActionResult> EditVehicle(VehicleUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input!";
                return View(dto); // Return to the edit view with the current data
            }

            var response = await _vehicleService.UpdateAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Vehicle updated successfully!";
                return RedirectToAction(nameof(VehiclesIndex));
            }

            TempData["ErrorMessage"] = "Failed to update vehicle.";
            return View(dto); // Return to the edit view with the current data
        }


        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var response = await _vehicleService.GetAsync<APIResponse>(id);
            if (response == null || !response.IsSuccess)
            {
                TempData["ErrorMessage"] = "Vehicle not found.";
                return RedirectToAction("VehiclesIndex");
            }

            var vehicle = JsonConvert.DeserializeObject<VehicleDTO>(Convert.ToString(response.Result));
            return View(vehicle);  // Pass the vehicle details to the view
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _vehicleService.DeleteAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Vehicle deleted successfully!";
                return RedirectToAction("VehiclesIndex");
            }

            TempData["ErrorMessage"] = "Failed to delete the vehicle.";
            return RedirectToAction("VehiclesIndex");
        }


    }
}
