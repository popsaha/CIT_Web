using CIT_Web.Models;
using CIT_Web.Models.Dto.Police;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIT_Web.Controllers
{
    public class PoliceController : Controller
    {
        private readonly IPoliceService _policeService;

        public PoliceController(IPoliceService policeService)
        {
            _policeService = policeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PoliceIndex()
        {
            var policeList = new List<PoliceDTO>();
            var policeResponse = await _policeService.GetAllPoliceAsync<APIResponse>();

            if (policeResponse != null && policeResponse.IsSuccess)
            {
                policeList = JsonConvert.DeserializeObject<List<PoliceDTO>>(Convert.ToString(policeResponse.Result));
            }
            var model = new PoliceVM
            {
                PoliceCreateDTO = new PoliceCreateDTO(), // Empty form for new vehicle
                PoliceDTOs = policeList // Existing vehicle list
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PoliceIndex(PoliceVM model)
        {
            var response = await _policeService.CreateAsync<APIResponse>(model.PoliceCreateDTO);
            if (response != null && response.IsSuccess)
            {
                // Redirect to the same action to avoid form resubmission
                TempData["SuccessMessage"] = "Police saved successfully!";
                return RedirectToAction(nameof(PoliceIndex));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error saving Police.");
            }

            // If something goes wrong, fetch the updated Police list
            var policeListResponse = await _policeService.GetAllPoliceAsync<APIResponse>();
            model.PoliceDTOs = policeListResponse != null && policeListResponse.IsSuccess
                ? JsonConvert.DeserializeObject<List<PoliceDTO>>(Convert.ToString(policeListResponse.Result))
                : new List<PoliceDTO>();
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var response = await _policeService.GetAllPoliceAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                var polices = JsonConvert.DeserializeObject<List<PoliceUpdateDTO>>(Convert.ToString(response.Result));
                var police = polices.FirstOrDefault(v => v.PoliceId == id);
                if (police != null)
                {
                    return View(police);
                }
            }

            TempData["ErrorMessage"] = "Police not found!";
            return RedirectToAction(nameof(PoliceIndex));
        }

        [HttpPost]
        public async Task<IActionResult> EditPolice(PoliceUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input!";
                return View(dto); // Return to the edit view with the current data
            }

            var response = await _policeService.UpdateAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Police updated successfully!";
                return RedirectToAction(nameof(PoliceIndex));
            }

            TempData["ErrorMessage"] = "Failed to update Police.";
            return View(dto); // Return to the edit view with the current data
        }
    }
}
