using Microsoft.AspNetCore.Mvc;

namespace CIT_Web.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GroupData()
        {
            return View();
        }
    }
}
