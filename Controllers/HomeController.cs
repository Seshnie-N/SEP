using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Data;
using SEP.Models;
using System.Diagnostics;

namespace SEP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public HomeController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
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
                        return RedirectToAction("Create", "Student");
                    }  
                } 
                else if (User.IsInRole("Employer"))
                {
					ApplicationUser user = await _userManager.GetUserAsync(User);
                    var employer = await _db.Employers.FindAsync(user.Id);
                    if (employer != null)
                    {
                        //employer profile  created
                        if (employer.ApprovalStatus == "Approved")
                        {
                            return RedirectToAction("EmployerHome");
                        } else if (employer.ApprovalStatus == "Rejected")
                        {
                            return RedirectToAction("UpdateEmployer", "Profile");
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
        [Authorize(Roles = "Student")]
        public IActionResult StudentHome()
        {
            return View();
        }
        [Authorize(Roles = "Employer")]
        public IActionResult EmployerHome()
        {
            return View();
        }
        [Authorize(Roles = "Approver")]
        public IActionResult ApproverHome() { 
            return View();
        }
        [Authorize(Roles = "Admin")]
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