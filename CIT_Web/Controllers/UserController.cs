using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.Police;
using CIT_Web.Models.Dto.Region;
using CIT_Web.Models.Dto.RoleMaster;
using CIT_Web.Models.Dto.User;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIT_Web.Controllers
{
    public class UserController : Controller
    {
        public readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRegionService _regionService;

        public UserController(IRoleService roleService, IMapper mapper, IUserService userService, IRegionService regionService)
        {
            _mapper = mapper;
            _roleService = roleService;
            _userService = userService;
            _regionService = regionService;


        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserIndex()
        {
            UserRoleVM userRoleVM = new UserRoleVM();

            var roleResponse = await _roleService.GetAllRole<APIResponse>();

            if (roleResponse != null && roleResponse.IsSuccess)
            {
                userRoleVM.Roles = JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(roleResponse.Result)) ?? new List<RoleListDTO>();
            }
            else
            {
                userRoleVM.Roles = new List<RoleListDTO>();
            }

            var regionResponse = await _regionService.GetAllRegion<APIResponse>();
            if(regionResponse != null && regionResponse.IsSuccess)
            {
                userRoleVM.Regions = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(regionResponse.Result)) ?? new List<RegionDTO>();
            }
            else
            {
                userRoleVM.Regions = new List<RegionDTO>();
            }

            var userList = await _userService.GetAllAsync<APIResponse>();
            if (userList != null && userList.IsSuccess)
            {
                userRoleVM.Users = JsonConvert.DeserializeObject<List<UserDTO>>
                    (Convert.ToString(userList.Result)) ?? new List<UserDTO>();
            }
            else
            {
                userRoleVM.Users = new List<UserDTO>();
            }
            return View(userRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> UserIndex(UserRoleVM userRoleVM)
        {
            try
            {
                // Call the user creation service
                var response = await _userService.CreateAsync<APIResponse>(userRoleVM.createDTO);

                if (response != null && response.IsSuccess)
                {
                    TempData["SuccessMessage"] = "User saved successfully!";
                    return RedirectToAction(nameof(UserIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed and add a model error
                ModelState.AddModelError("", "An unexpected error occurred.");
            }

            // Reload roles and users if creation fails
            var rolesReloadOnError = await _roleService.GetAllRole<APIResponse>();
            var userListReloadOnError = await _userService.GetAllAsync<APIResponse>();
            var regionReloadOnError = await _regionService.GetAllRegion<APIResponse>();

            userRoleVM.Roles = rolesReloadOnError != null && rolesReloadOnError.IsSuccess
                ? JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(rolesReloadOnError.Result)) ?? new List<RoleListDTO>()
                : new List<RoleListDTO>();

            userRoleVM.Users = userListReloadOnError != null && userListReloadOnError.IsSuccess
                ? JsonConvert.DeserializeObject<List<UserDTO>>(Convert.ToString(userListReloadOnError.Result)) ?? new List<UserDTO>()
                : new List<UserDTO>();

            userRoleVM.Regions = regionReloadOnError != null && regionReloadOnError.IsSuccess
                ? JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(regionReloadOnError.Result)) ?? new List<RegionDTO>()
                : new List<RegionDTO>();

            return View(userRoleVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _userService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                var users = JsonConvert.DeserializeObject<List<UserUpdateDTO>>(Convert.ToString(response.Result));
                var user = users.FirstOrDefault(v => v.UserId == id);
                if (user != null)
                {
                    return View(user);
                }
            }

            TempData["ErrorMessage"] = "User not found!";
            return RedirectToAction(nameof(UserIndex));
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input!";
                return View(dto); // Return to the edit view with the current data
            }

            var response = await _userService.UpdateAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction(nameof(UserIndex));
            }

            TempData["ErrorMessage"] = "Failed to update User.";
            return View(dto); // Return to the edit view with the current data
        }
    }
}
