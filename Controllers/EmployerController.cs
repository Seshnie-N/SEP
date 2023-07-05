using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP.Data;
using SEP.Models.DomainModels;
using SEP.Models.ViewModels;

namespace SEP.Controllers
{
    [Authorize(Roles ="Employer")]
    public class EmployerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly INotyfService _toastNotification;

		public EmployerController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, INotyfService notyfService)
        {
            _db = db;
            _userManager = userManager;
            _toastNotification = notyfService; 
        }

        public IActionResult Index()
        {

            return View();
        }

        //GET
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var employer = await _db.Employers.Where(e => e.UserId == user.Id).SingleOrDefaultAsync();

            employer ??= new Employer
                {
                    User = user,
                    UserId = user.Id
                };

            EmployerProfileViewModel employerProfile = new()
            {
                Employer = employer,
                User = user
            };

            return View(employerProfile);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployerProfileViewModel employerProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(employerProfile);
            }
             employerProfile.Employer.ApprovalStatus = "Pending"; 
            _db.Employers.Add(employerProfile.Employer);
            _db.SaveChanges();
            _toastNotification.Success("Profile successfully created.");

            return RedirectToAction("Index", "Home");
        }

        //cascading drop-down
        public JsonResult GetDepartmentById(int id)
        {
            return Json(_db.Departments.Where(d => d.FacultyId.Equals(id)));
        }
        public async Task<IActionResult> Update()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var employer = await _db.Employers.Where(e => e.UserId == user.Id).SingleOrDefaultAsync();

            employer ??= new Employer
                {
                    User = user,
                    UserId = user.Id
                }; 

            EmployerProfileViewModel employerProfile = new()
            {
                Employer = employer,
                User = user
            };

            return View(employerProfile);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployerProfileViewModel employerProfile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var employerRecord = _db.Employers.Find(employerProfile.Employer.UserId);
            ApplicationUser userRecord = await _userManager.GetUserAsync(User);

            if (employerRecord != null)
            {
                userRecord.FirstName = employerProfile.User.FirstName;
                userRecord.LastName = employerProfile.User.LastName;
                userRecord.PhoneNumber = employerProfile.User.PhoneNumber;
                userRecord.Email = employerProfile.User.Email;
                employerRecord.Title = employerProfile.Employer.Title;
                employerRecord.JobTitle = employerProfile.Employer.JobTitle;
                employerRecord.CompanyRegistrationNumber = employerProfile.Employer.CompanyRegistrationNumber;
                employerRecord.TradingName = employerProfile.Employer.TradingName;
                employerRecord.BusinessName = employerProfile.Employer.BusinessName;
                employerRecord.Address = employerProfile.Employer.Address;
                employerRecord.BusinessType = employerProfile.Employer.BusinessType;
                employerRecord.IsApproved = employerProfile.Employer.IsApproved;
                employerRecord.ApproverNote = employerProfile.Employer.ApproverNote;
                employerRecord.IsInternal = employerProfile.Employer.IsInternal;
                
                employerRecord.ApprovalStatus = "Pending";
                employerRecord.IsApproved = false;
               
                _db.SaveChanges();
				_toastNotification.Success("Profile successfully updated.");
				return RedirectToAction("Index", "Home");
            }
			return RedirectToAction("EmployerHome", "Home");

		}
		public IActionResult AwaitingApproval()
		{
			return View();
		}
	}
}
