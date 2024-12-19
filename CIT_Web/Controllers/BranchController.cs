using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Branch;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CIT_Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        public BranchController(IBranchService branchService, IMapper mapper)
        {
            _branchService = branchService;
            _mapper = mapper;

        }
        public async Task<IActionResult> IndexBranch()
        {
            var branchlist = new List<BranchDTO>();
            var response = await _branchService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                branchlist = JsonConvert.DeserializeObject<List<BranchDTO>>(Convert.ToString(response.Result));
            }
            var model = new BranchVM
            {
                branchCreateDTO = new BranchCreateDTO(), // Empty form for new vehicle
                branchDTOs = branchlist
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexBranch(BranchVM model)
        {
            var response = await _branchService.CreateAsync<APIResponse>(model.branchCreateDTO);

            if (response != null && response.IsSuccess)
            {
                // Redirect to the same action to avoid form resubmission
                TempData["SuccessMessage"] = "Branch saved successfully!";
                return RedirectToAction(nameof(IndexBranch));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error saving Branch.");
            }

            var branchListResponse = await _branchService.GetAllAsync<APIResponse>();
            model.branchDTOs = branchListResponse != null && branchListResponse.IsSuccess
                ? JsonConvert.DeserializeObject<List<BranchDTO>>(Convert.ToString(branchListResponse.Result))
                : new List<BranchDTO>();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _branchService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                var branches = JsonConvert.DeserializeObject<List<BranchUpdateDTO>>(Convert.ToString(response.Result));
                var branch = branches.FirstOrDefault(v => v.BranchID == id);
                if (branch != null)
                {
                    return View(branch);
                }
            }

            TempData["ErrorMessage"] = "Branch not found!";
            return RedirectToAction(nameof(IndexBranch));
        }

        [HttpPost]
        public async Task<IActionResult> EditBranch(BranchUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input!";
                return View(dto); // Return to the edit view with the current data
            }

            var response = await _branchService.UpdateAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Branch updated successfully!";
                return RedirectToAction(nameof(IndexBranch));
            }

            TempData["ErrorMessage"] = "Failed to update Branch.";
            return View(dto); // Return to the edit view with the current data
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            int userId = 1;
            if (branchId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Branch ID.";
                return RedirectToAction(nameof(IndexBranch));
            }

            var response = await _branchService.DeleteAsync<APIResponse>(branchId);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Branch deleted successfully!";
                return RedirectToAction(nameof(IndexBranch));
            }

            TempData["ErrorMessage"] = "Failed to delete Branch.";
            return RedirectToAction(nameof(IndexBranch));
        }
    }
}
