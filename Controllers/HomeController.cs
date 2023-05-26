using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Areas.Identity.Data;
using SEP.Models;
using System.Diagnostics;

namespace SEP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Student"))
                {
                    return RedirectToAction("StudentHome");
                } 
                else if (User.IsInRole("Employer"))
                {
                    return RedirectToAction("EmployerHome");
                }
                else if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("AdminHome");
                }

            } 

            return View();
        }

        public IActionResult StudentHome()
        {
            return View();
        }

        public IActionResult EmployerHome()
        {
            return View();
        }
        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}