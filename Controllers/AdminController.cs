using Microsoft.AspNetCore.Mvc;

namespace SEP.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddUser()
		{
			return View();
		}
	}
}
