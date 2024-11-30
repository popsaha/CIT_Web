using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.CrewCommander;
using CIT_Web.Models.Dto.CrewCommanderMaster;
using CIT_Web.Models.Dto.RoleMaster;
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

        public CrewCommanderMasterController(IRoleService roleService, ICrewCommanderMasterService crewCommanderMasterService, IMapper mapper)
        {
            _mapper = mapper;
            _roleService = roleService;
            _crewCommanderMasterService = crewCommanderMasterService;
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
            if (!ModelState.IsValid)
            {
                var roleResponse = await _roleService.GetAllRole<APIResponse>();
                var crewList = await _crewCommanderMasterService.GetAllAsync<APIResponse>();

                roleCrewVM.Roles = roleResponse != null && roleResponse.IsSuccess
                    ? JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(roleResponse.Result)) ?? new List<RoleListDTO>()
                    : new List<RoleListDTO>();

                roleCrewVM.userMasters = crewList != null && crewList.IsSuccess
                   ? JsonConvert.DeserializeObject<List<UserMasterDTO>>(Convert.ToString(crewList.Result)) ?? new List<UserMasterDTO>()
                   : new List<UserMasterDTO>();

                return View(roleCrewVM);
            }         
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
            roleCrewVM.Roles = rolesReload != null && rolesReload.IsSuccess
                ? JsonConvert.DeserializeObject<List<RoleListDTO>>(Convert.ToString(rolesReload.Result)) ?? new List<RoleListDTO>()
                : new List<RoleListDTO>();

            roleCrewVM.userMasters = crewListReload != null && crewListReload.IsSuccess
              ? JsonConvert.DeserializeObject<List<UserMasterDTO>>(Convert.ToString(crewListReload.Result)) ?? new List<UserMasterDTO>()
              : new List<UserMasterDTO>();

            return View(roleCrewVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var response = await _crewCommanderMasterService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                var crew = JsonConvert.DeserializeObject<List<CrewUpdateDTO>>(Convert.ToString(response.Result));
                var crewData = crew.FirstOrDefault(v => v.UserId == id);
                if (crewData != null)
                {
                    return View(crewData);
                }
            }

            TempData["ErrorMessage"] = "Crew not found!";
            return RedirectToAction(nameof(CrewCreate));
        }

        [HttpPost]
        public async Task<IActionResult> EditCrew(CrewUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input!";
                return View(dto); // Return to the edit view with the current data
            }

            var response = await _crewCommanderMasterService.UpdateAsync<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                TempData["SuccessMessage"] = "Crew updated successfully!";
                return RedirectToAction(nameof(CrewCreate));
            }

            TempData["ErrorMessage"] = "Failed to update Crew.";
            return View(dto); // Return to the edit view with the current data
        }

    }
}
