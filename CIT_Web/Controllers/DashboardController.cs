using Microsoft.AspNetCore.Mvc;

namespace CIT_Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
