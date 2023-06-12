using Microsoft.AspNetCore.Mvc;

namespace SEP.Controllers
{
    public class JobApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
