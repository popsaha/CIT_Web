using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.CrewCommander;
using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.Region;
using CIT_Web.Models.Dto.RoleMaster;
using CIT_Web.Models.Dto.User;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CIT_Web.Controllers
{
    public class CrewCommanderMasterController : Controller
    {
        public readonly IRoleService _roleService;
        public readonly ICrewCommanderMasterService _crewCommanderMasterService;
        private readonly IMapper _mapper;
        private readonly IRegionService _regionService;

        public CrewCommanderMasterController(IRoleService roleService, ICrewCommanderMasterService crewCommanderMasterService, IMapper mapper, IRegionService regionService)
        {
            _mapper = mapper;
            _roleService = roleService;
            _crewCommanderMasterService = crewCommanderMasterService;
            _regionService = regionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CrewCreate()
        {
            RoleCrewVM roleCrewVM = new RoleCrewVM();

            var roleResponse = await _roleService.GetAllRole<APIResponse>();

            if (roleResponse != null && roleResponse.IsSuccess)
            {
                roleCrewVM.Roles = JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(roleResponse.Result)) ?? new List<RoleListDTO>();
            }
            else
            {
                roleCrewVM.Roles = new List<RoleListDTO>();
            }

            var regionResponse = await _regionService.GetAllRegion<APIResponse>();
            if (regionResponse != null && regionResponse.IsSuccess)
            {
                roleCrewVM.Regions = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(regionResponse.Result)) ?? new List<RegionDTO>();
            }
            else
            {
                roleCrewVM.Regions = new List<RegionDTO>();
            }

            var crewList = await _crewCommanderMasterService.GetAllAsync<APIResponse>();
            if (crewList != null && crewList.IsSuccess)
            {
                roleCrewVM.userMasters = JsonConvert.DeserializeObject<List<UserMasterDTO>>
                    (Convert.ToString(crewList.Result)) ?? new List<UserMasterDTO>();
            }
            else
            {
                roleCrewVM.userMasters = new List<UserMasterDTO>();
            }
            return View(roleCrewVM);
        }

        [HttpPost]
        public async Task<IActionResult> CrewCreate(RoleCrewVM roleCrewVM)
        {
            //if (!ModelState.IsValid)
            //{
            //    var roleResponse = await _roleService.GetAllRole<APIResponse>();
            //    var crewList = await _crewCommanderMasterService.GetAllAsync<APIResponse>();

            //    roleCrewVM.Roles = roleResponse != null && roleResponse.IsSuccess
            //        ? JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(roleResponse.Result)) ?? new List<RoleListDTO>()
            //        : new List<RoleListDTO>();

            //    roleCrewVM.userMasters = crewList != null && crewList.IsSuccess
            //       ? JsonConvert.DeserializeObject<List<UserMasterDTO>>(Convert.ToString(crewList.Result)) ?? new List<UserMasterDTO>()
            //       : new List<UserMasterDTO>();

            //    return View(roleCrewVM);
            //}
            try
            {
                var response = await _crewCommanderMasterService.CreateAsync<APIResponse>(roleCrewVM.CrewUser);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("CrewCreate");
                }
               
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "An unexpected error occurred.");
            }

            var rolesReload = await _roleService.GetAllRole<APIResponse>();
            var crewListReload = await _crewCommanderMasterService.GetAllAsync<APIResponse>();
            var regionReloadOnError = await _regionService.GetAllRegion<APIResponse>();

            roleCrewVM.Roles = rolesReload != null && rolesReload.IsSuccess
                ? JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(rolesReload.Result)) ?? new List<RoleListDTO>()
                : new List<RoleListDTO>();

            roleCrewVM.userMasters = crewListReload != null && crewListReload.IsSuccess
              ? JsonConvert.DeserializeObject<List<UserMasterDTO>>(Convert.ToString(crewListReload.Result)) ?? new List<UserMasterDTO>()
              : new List<UserMasterDTO>();

            roleCrewVM.Regions = regionReloadOnError != null && regionReloadOnError.IsSuccess
               ? JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(regionReloadOnError.Result)) ?? new List<RegionDTO>()
               : new List<RegionDTO>();
            return View(roleCrewVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var response = await _crewCommanderMasterService.GetByIdAsync<APIResponse>(id);
        
            if (response?.IsSuccess != true)
            {
                TempData["ErrorMessage"] = "Crew not found!";
                return RedirectToAction(nameof(CrewCreate));
            }

            
            var user = JsonConvert.DeserializeObject<CrewUpdateDTO>(Convert.ToString(response.Result));

            var roleResponse = await _roleService.GetAllRole<APIResponse>();
            var regionResponse = await _regionService.GetAllRegion<APIResponse>();

            if (roleResponse?.IsSuccess != true || regionResponse?.IsSuccess != true)
            {
                TempData["ErrorMessage"] = "Failed to load roles or regions!";
                return RedirectToAction(nameof(CrewCreate));
            }

            var roles = JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(roleResponse.Result));
            var regions = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(regionResponse.Result));

            var viewModel = new RoleCrewVM
            {
                Roles = roles,
                Regions = regions,
                CrewUser = new CrewUserCreateDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    RoleName = user.RoleName,
                    RegionName = user.RegionName
                }
            };

            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCrew(RoleCrewVM dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    TempData["ErrorMessage"] = "Invalid input!";
            //    return View(dto); // Return to the edit view with the current data
            //}

            var dtoData = new CrewUpdateDTO
            {
                UserId = dto.CrewUser.UserId,
                UserName = dto.CrewUser.UserName,
                Password = dto.CrewUser.Password,
                RoleName = dto.CrewUser.RoleName,
                RegionName = dto.CrewUser.RegionName,
                IsActive = dto.CrewUser.IsActive
            };

            var response = await _crewCommanderMasterService.UpdateAsync<APIResponse>(dtoData);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Crew updated successfully!";
                return RedirectToAction(nameof(CrewCreate));
            }

            TempData["ErrorMessage"] = "Failed to update Crew.";
            return View(dto); // Return to the edit view with the current data
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCrew(int userId)
        {
            //int deletedBy = 1;
            if (userId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid User ID.";
                return RedirectToAction(nameof(CrewCreate));
            }

            var response = await _crewCommanderMasterService.DeleteAsync<APIResponse>(userId);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Crew deleted successfully!";
                return RedirectToAction(nameof(CrewCreate));
            }

            TempData["ErrorMessage"] = "Failed to delete Crew.";
            return RedirectToAction(nameof(CrewCreate));
        }
    }
}
