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
            var userResponse = await _userService.GetByIdAsync<APIResponse>(id); // Fetch specific user
            if (userResponse?.IsSuccess != true)
            {
                TempData["ErrorMessage"] = "User not found!";
                return RedirectToAction(nameof(UserIndex));
            }

            var user = JsonConvert.DeserializeObject<UserUpdateDTO>(Convert.ToString(userResponse.Result));

            var roleResponse = await _roleService.GetAllRole<APIResponse>();
            var regionResponse = await _regionService.GetAllRegion<APIResponse>();

            if (roleResponse?.IsSuccess != true || regionResponse?.IsSuccess != true)
            {
                TempData["ErrorMessage"] = "Failed to load roles or regions!";
                return RedirectToAction(nameof(UserIndex));
            }

            var roles = JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(roleResponse.Result));
            var regions = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(regionResponse.Result));

            var viewModel = new UserRoleVM
            {
                Roles = roles,
                Regions = regions,
                createDTO = new UserCreateDTO
                {   UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    RoleName = user.RoleName,
                    RegionName = user.RegionName
                }
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(UserRoleVM viewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    // Reload roles and regions for dropdowns
            //    var roleResponse = await _roleService.GetAllRole<APIResponse>();
            //    var regionResponse = await _regionService.GetAllRegion<APIResponse>();

            //    viewModel.Roles = JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(roleResponse.Result));
            //    viewModel.Regions = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(regionResponse.Result));

            //    TempData["ErrorMessage"] = "Invalid input!";
            //    return View(viewModel);
            //}

            var dto = new UserUpdateDTO
            {
                UserId = viewModel.createDTO.UserId,
                UserName = viewModel.createDTO.UserName,
                Password = viewModel.createDTO.Password,
                RoleName = viewModel.createDTO.RoleName,
                RegionName = viewModel.createDTO.RegionName
            };

            var response = await _userService.UpdateAsync<APIResponse>(dto);
            if (response?.IsSuccess == true)
            {
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction(nameof(UserIndex));
            }

            TempData["ErrorMessage"] = "Failed to update User.";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            int deletedBy = 1;
            if (userId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid User ID.";
                return RedirectToAction(nameof(UserIndex));
            }

            var response = await _userService.DeleteAsync<APIResponse>(userId, deletedBy);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "User deleted successfully!";
                return RedirectToAction(nameof(UserIndex));
            }

            TempData["ErrorMessage"] = "Failed to delete user.";
            return RedirectToAction(nameof(UserIndex));
        }

    }
}
