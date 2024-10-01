using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.CrewCommander;
using CIT_Web.Models.Dto.Task;
using CIT_Web.Models.Dto.TaskList;
using CIT_Web.Models.Dto.Vehicle;
using CIT_Web.Models.ViewModel;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace CIT_Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ItaskService _taskService;
        private readonly ITaskListService _taskListService;
        private readonly IMapper _mapper;
        private readonly IVehicleService _vehicleService;
        private readonly ICrewCommanderService _crewCommanderService;
        public TaskController(ItaskService taskService, ITaskListService taskListService,ICrewCommanderService crewCommanderService,IVehicleService vehicleService, IMapper mapper)
        {
            _taskService = taskService;
            _taskListService = taskListService;
            _crewCommanderService = crewCommanderService;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TaskCreateVM taskVM = new TaskCreateVM();
            //TaskMainVM taskMain = new TaskMainVM();

            var OrderType_response = await _taskService.GetAllOrderTypeAsync<APIResponse>();
            if (OrderType_response != null && OrderType_response.IsSuccess)
            {
                taskVM.OrderTypelist = JsonConvert.DeserializeObject<List<OrderType>>(Convert.ToString(OrderType_response.Result));
                taskVM.OrderTypelist.Insert(0, new OrderType { OrderTypeID = 0, TypeName = "Select Order Type" });
            }

                List<PriorityMaster> Prioritymasterlist = new List<PriorityMaster>{
                new PriorityMaster{ PriorityId =0,PriorityName="select"},
                new PriorityMaster{ PriorityId =1,PriorityName="Low"},
                new PriorityMaster{ PriorityId =2,PriorityName="Medium"},
                new PriorityMaster{ PriorityId =3,PriorityName="High"}
            };
                taskVM.PriorityMasterlist = Prioritymasterlist;

                List<PickTypeMaster> PickTypeMasterlist = new List<PickTypeMaster>{
                new PickTypeMaster{ PickUpTypeId =0,PickUpTypeName="Select Pickup Type"},
                new PickTypeMaster{ PickUpTypeId =1,PickUpTypeName="CIT"},
                new PickTypeMaster{ PickUpTypeId =2,PickUpTypeName="BSS"},
                new PickTypeMaster{ PickUpTypeId =3,PickUpTypeName="ATM"},
                new PickTypeMaster{ PickUpTypeId =3,PickUpTypeName="Airlift"}
            };
                taskVM.Picktypemasterlst = PickTypeMasterlist;

                List<RepeatsTaskMaster> Repeatstaskmasterlist = new List<RepeatsTaskMaster>{
                new RepeatsTaskMaster{ RepeatId =0,RepeatName="select"},
                new RepeatsTaskMaster{ RepeatId =1,RepeatName="Daily"},
                new RepeatsTaskMaster{ RepeatId =2,RepeatName="Weekly"},
                new RepeatsTaskMaster{ RepeatId =3,RepeatName="Monthly"},
            };
                taskVM.repeatsaskmasterslist = Repeatstaskmasterlist;

                List<RepeatsInDaysMaster> repeatsInDaysmaster = new List<RepeatsInDaysMaster>{
                new RepeatsInDaysMaster{ RepeatDaysName ="0",RepeatInDay="select"},
                new RepeatsInDaysMaster{ RepeatDaysName ="Sunday",RepeatInDay="Sunday"},
                new RepeatsInDaysMaster{ RepeatDaysName ="Monday",RepeatInDay="Monday"},
                new RepeatsInDaysMaster{ RepeatDaysName ="Tuesday",RepeatInDay="Tuesday"},
                new RepeatsInDaysMaster{ RepeatDaysName ="Wednesday",RepeatInDay="Wednesday"},
                new RepeatsInDaysMaster{ RepeatDaysName ="Thursday",RepeatInDay="Thursday"},
                new RepeatsInDaysMaster{ RepeatDaysName ="Friday",RepeatInDay="Friday"},
                new RepeatsInDaysMaster{ RepeatDaysName ="Saturday",RepeatInDay="Saturday"},
            };
                taskVM.repeatsInDaysMasterslist = repeatsInDaysmaster;

                var Customer_response = await _taskService.GetAllAsync<APIResponse>();
                if (Customer_response != null && Customer_response.IsSuccess)
                {
                    taskVM.customerslist = JsonConvert.DeserializeObject<List<CustomerDTO>>(Convert.ToString(Customer_response.Result));
                    taskVM.customerslist.Insert(0, new CustomerDTO { CustomerId = 0, CustomerName = "Select Sender" });
                }

            var IsVaultLocation_response = await _taskService.GetAllVaultLocationAsync<APIResponse>();
            if (IsVaultLocation_response != null && IsVaultLocation_response.IsSuccess)
            {
                taskVM.vaultLovationMasters = JsonConvert.DeserializeObject<List<VaultLovationMaster>>(Convert.ToString(IsVaultLocation_response.Result));
                taskVM.vaultLovationMasters.Insert(0, new VaultLovationMaster { VaultID = 0, VaultName = "Select Sender's Location" });
            }

            var Orderrouteslst_response = await _taskService.GetOrderRoutesAsync<APIResponse>();
            if (Orderrouteslst_response != null && Orderrouteslst_response.IsSuccess)
            {
                taskVM.Orderrouteslst = JsonConvert.DeserializeObject<List<OrderRoutes>>(Convert.ToString(Orderrouteslst_response.Result));
                taskVM.Orderrouteslst.Insert(0, new OrderRoutes { OrderRouteId = 0, RouteName = "Select Recipient" });
            }


            var response = await _taskListService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                taskVM.taskDTOlsts = JsonConvert.DeserializeObject<List<TaskDTOlst>>(Convert.ToString(response.Result));
            }

            var vehicleResponse = await _vehicleService.GetAllVehicleAsync<APIResponse>();
            if (vehicleResponse != null && vehicleResponse.IsSuccess)
            {
                taskVM.vehicledtolst = JsonConvert.DeserializeObject<List<VehicleDTO>>(Convert.ToString(vehicleResponse.Result)) ?? new List<VehicleDTO>();
            }
            else
            {
                taskVM.vehicledtolst = new List<VehicleDTO>(); // Initialize empty list if the API fails
            }

            var crewResponse = await _crewCommanderService.GetAllCrewCommanderList<APIResponse>();
            if (crewResponse != null && crewResponse.IsSuccess)
            {
                taskVM.crews = JsonConvert.DeserializeObject<List<CrewCommanderDTO>>(Convert.ToString(crewResponse.Result)) ?? new List<CrewCommanderDTO>();
            }
            else
            {
                taskVM.crews = new List<CrewCommanderDTO>(); // Initialize empty list if the API fails
            }

            return View(taskVM);
        }
        public async Task<JsonResult> GetBranchNameById(int CustomerId)
        {
            var Res = 0;
            TaskCreateVM taskVM = new TaskCreateVM();
            try
            {
                var response = await _taskService.GetAsync<APIResponse>(CustomerId);
                if (response != null && response.IsSuccess)
                {
                    taskVM.taskbranchlist = JsonConvert.DeserializeObject<List<TaskBranch>>(Convert.ToString(response.Result));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(taskVM.taskbranchlist);
        }
        public IActionResult GroupData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TaskCreateVM taskcreateModel)
        {
            try
            {
                TaskCreateDTO taskCreateDTO = new TaskCreateDTO();
                taskCreateDTO.OrderId = "1";
                taskCreateDTO.OrderTypeID = taskcreateModel.OrderTypeID;
                taskCreateDTO.PriorityId = taskcreateModel.PriorityId;
                taskCreateDTO.PickUpTypeId = taskcreateModel.PickUpTypeId;
                taskCreateDTO.CustomerId = taskcreateModel.CustomerId;
                taskCreateDTO.BranchID = taskcreateModel.BranchID;
                taskCreateDTO.CustomerRecipiantId = taskcreateModel.CustomerRecipiantId;
                taskCreateDTO.CustomerRecipiantLocationId = taskcreateModel.CustomerRecipiantLocationId;
                ////taskCreateDTO.RepeatId = taskcreateModel.RepeatId;
                ////taskCreateDTO.RepeatDaysName = taskcreateModel.RepeatDaysName;
                taskCreateDTO.RepeatId = 1;
                taskCreateDTO.RepeatDaysName = "Monday";
                taskCreateDTO.OrderCreateDate = taskcreateModel.OrderCreateDate;
                taskCreateDTO.EndOnDate = DateTime.Now.ToString();
                //taskCreateDTO.EndOnDate = taskcreateModel.OrderCreateDate;
                taskCreateDTO.VaultID = taskcreateModel.VaultID;
                taskCreateDTO.isVault = taskcreateModel.isVault;
                taskCreateDTO.isVaultFinal = taskcreateModel.isVaultFinal;
                taskCreateDTO.OrderRouteId = taskcreateModel.OrderRouteId;
                taskCreateDTO.NewVehicleRequired = taskcreateModel.NewVehicleRequired;
                taskCreateDTO.fullDayCheck = taskcreateModel.fullDayCheck;

                var TaskcreateDTO = _mapper.Map<TaskCreateDTO>(taskCreateDTO);

                var response = await _taskService.CreateAsync<APIResponse>(TaskcreateDTO);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index","Task");
        }

        public async Task<JsonResult> GetAllOrderTask(string OrderNumber)
        {
            TaskCreateVM taskCreateVM = new TaskCreateVM();
            TaskCreateDTO taskCreateDTO = new TaskCreateDTO();
            try
            {             
                var response = await _taskService.GetOrderTaskAsync<APIResponse>(OrderNumber);
                if (response != null && response.IsSuccess)
                {
                    taskCreateDTO = JsonConvert.DeserializeObject<TaskCreateDTO>(Convert.ToString(response.Result));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(taskCreateDTO);
        }
    }
}
