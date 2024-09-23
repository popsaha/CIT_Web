using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.CrewCommander;
using CIT_Web.Models.Dto.TaskGrouping;
using CIT_Web.Models.Dto.TaskGroupList;
using CIT_Web.Models.Dto.TaskList;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CIT_Web.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ITaskGroupListService _taskGroupList;
        private readonly IVehicleService _vehicleService;
        private readonly ICrewCommanderService _crewCommanderList;
        private readonly IMapper _mapper;

        public AssignmentController(ITaskGroupListService taskGroupList, IVehicleService vehicleService, ICrewCommanderService crewCommander, IMapper mapper)
        {
            _taskGroupList = taskGroupList;
            _vehicleService = vehicleService;
            _crewCommanderList = crewCommander;
            _mapper = mapper;
           
        }


        public async Task<IActionResult> Index()
        {
            // Task Group List
            List<TaskGroupListDTO> taskGroups = new();
            var response = await _taskGroupList.GetAllGroupListAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                taskGroups = JsonConvert.DeserializeObject<List<TaskGroupListDTO>>(Convert.ToString(response.Result));
            }

            // Vehicle List
            List<VehicleDTO> vehileList = new();

            var vehicleResponse = await _vehicleService.GetAllVehicleAsync<APIResponse>();
            if (vehicleResponse != null && vehicleResponse.IsSuccess)
            {
                vehileList = JsonConvert.DeserializeObject<List<VehicleDTO>>(Convert.ToString(vehicleResponse.Result));
            }


            // Crew Commander List
            List<CrewCommanderDTO> crewList = new();

            var crewCommanderResponse = await _crewCommanderList.GetAllCrewCommanderList<APIResponse>();
            if (crewCommanderResponse != null && crewCommanderResponse.IsSuccess)
            {
                crewList = JsonConvert.DeserializeObject<List<CrewCommanderDTO>>(Convert.ToString(crewCommanderResponse.Result));
            }

            var taskGroupVehicle = new TaskGroupVehicleViewModel
            {
                TaskGroups = taskGroups,
                Vehicles = vehileList ,
                CrewCommanders = crewList
            };
            return View(taskGroupVehicle);
        }
    }
}
