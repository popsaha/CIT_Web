using CIT_Web.Models.Dto.TaskGrouping;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CIT_Web.Models;

namespace CIT_Web.Controllers
{
    public class TaskGroupController : Controller
    {
        private readonly ITaskGroupService _taskGroupService;
        private readonly IMapper _mapper;

        public TaskGroupController(ITaskGroupService taskGroupService, IMapper mapper)
        {
            _taskGroupService = taskGroupService;
            _mapper = mapper;
        }

        [HttpPost("AddTaskGroup")]
        public async Task<IActionResult> AddTaskGroup([FromBody] List<TaskGroupRequestDTO> taskGroupDtos)
        {
            if (taskGroupDtos == null || taskGroupDtos.Count == 0)
            {
                return BadRequest("No tasks selected.");
            }
            foreach (var dto in taskGroupDtos)
            {
                var taskGroup = new TaskGroupRequestDTO
                {
                    TaskId = dto.TaskId,
                    GroupName = dto.GroupName,
                  
                };
                await _taskGroupService.AddGroupAsync<TaskGroupRequestDTO>(taskGroup);
            }
           
            return Ok(new { message = "Group successfully saved." });
        }
    }
}
