using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SEP.Controllers
{
	[Authorize(Roles ="Admin")]
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
