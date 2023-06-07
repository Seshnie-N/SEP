using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Student"))
                {
                    ApplicationUser user = await _userManager.GetUserAsync(User);
                    var student = await _db.Students.FindAsync(user.Id);
                    if (student != null)
                    {
                        return RedirectToAction("StudentHome");
                    } else
                    {
                        //direct to complete profile 
                        return RedirectToAction("CreateStudent", "Profile");
                    }  
                } 
                else if (User.IsInRole("Employer"))
                {
					ApplicationUser user = await _userManager.GetUserAsync(User);
                    var employer = await _db.Employers.FindAsync(user.Id);
                    if (employer != null)
                    {
                        //employer profile  created
                        if (employer.isApproved)
                        {
                            return RedirectToAction("EmployerHome");
                        }
                        return RedirectToAction("AwaitingApproval", "Profile");

                    } else
                    {
                        //direct to complete profile 
						return RedirectToAction("CreateEmployer", "Profile");
					}
                }
				else if (User.IsInRole("Approver"))
				{
					return RedirectToAction("ApproverHome");
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
        public IActionResult ApproverHome() { 
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