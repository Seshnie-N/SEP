using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using SEP.Models.ViewModels;

namespace SEP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> UpdateStudent()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var student = await _db.Students.Where(s => s.UserId == user.Id).SingleOrDefaultAsync();

            if (student == null)
            {
                student = new Student
                {
                    User = user,
                    UserId = user.Id
                };
            }

            StudentProfileViewModel studentProfile = new StudentProfileViewModel
            {
                Student = student,
                User = user
            };

            return View(studentProfile);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStudent(StudentProfileViewModel studentProfile)
        {
            var studentRecord = _db.Students.Find(studentProfile.Student.UserId);
            ApplicationUser userRecord = await _userManager.GetUserAsync(User);

            if (studentRecord != null)
            {
                userRecord.FirstName = studentProfile.User.FirstName;
                userRecord.LastName = studentProfile.User.LastName;
                userRecord.PhoneNumber = studentProfile.User.PhoneNumber;
                userRecord.Email = studentProfile.User.Email;
                studentRecord.Address = studentProfile.Student.Address;
                studentRecord.IdNumber = studentProfile.Student.IdNumber;
                studentRecord.DriversLicense = studentProfile.Student.DriversLicense;
                studentRecord.CareerObjective = studentProfile.Student.CareerObjective;
                studentRecord.Gender = studentProfile.Student.Gender;
                studentRecord.Race = studentProfile.Student.Race;
                studentRecord.isSouthAfrican = studentProfile.Student.isSouthAfrican;
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _db.Students.Add(studentProfile.Student);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

        }

        public async Task<IActionResult> UpdateEmployer()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var employer = await _db.Employers.Where(e => e.UserId == user.Id).SingleOrDefaultAsync();

            if (employer == null)
            {
                employer = new Employer
                {
                    User = user,
                    UserId = user.Id
                };
            }

            EmployerProfileViewModel employerProfile = new EmployerProfileViewModel
            {
                Employer = employer,
                User = user
            };

            return View(employerProfile);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployer(EmployerProfileViewModel employerProfile)
        {
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
                employerRecord.isApproved = employerProfile.Employer.isApproved;
                employerRecord.ApproverNote = employerProfile.Employer.ApproverNote;
                employerRecord.isInternal = employerProfile.Employer.isInternal;
                if(employerRecord.ApprovalStatus == null)
                {
                    employerRecord.ApprovalStatus = "Pending";
                }
                else
                {
                    employerRecord.ApprovalStatus = employerProfile.Employer.ApprovalStatus;
                }
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _db.Employers.Add(employerProfile.Employer);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }


        }
    }
}
