//using AutoMapper;
//using CIT_Web.Models.Dto.Customer;
//using CIT_Web.Models;
//using CIT_Web.Services;
//using CIT_Web.Services.IServices;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using CIT_Web.Models.Dto.TaskList;

//namespace CIT_Web.Controllers
//{
//    public class TaskListController : Controller
//    {
//        private readonly ITaskListService _taskListService;
//        private readonly IMapper _mapper;

//        public TaskListController(ITaskListService taskListService, IMapper mapper)
//        {
//            _taskListService = taskListService;
//            _mapper = mapper;
//        }
//        public async Task<IActionResult> Index()
//        {
//            List<TaskListDTO> list = new();

//            var response = await _taskListService.GetAllAsync<APIResponse>();
//            if (response != null && response.IsSuccess)
//            {
//                list = JsonConvert.DeserializeObject<List<TaskListDTO>>(Convert.ToString(response.Result));
//            }
          
//            return View(list);
//        }
//    }
//}
