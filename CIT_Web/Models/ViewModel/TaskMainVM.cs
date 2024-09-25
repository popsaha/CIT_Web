using CIT_Web.Models.Dto.TaskList;
using System.Reflection;

namespace CIT_Web.Models.ViewModel
{
    public class TaskMainVM
    {
        public TaskCreateVM taskCreatvm  { get; set; }
        public List<TaskDTOlst>  taskDTOlsts  { get; set; }

        //public CIT_Web.Models.ViewModel.TaskCreateVM Taskcreatvm { get; set; }
        //public List<CIT_Web.Models.Dto.TaskList.TaskListDTO> TasklistDTOs { get; set; }
    }

}
