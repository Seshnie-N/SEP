using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Plugins;
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

        //GET
        public async Task<IActionResult> CreateEmployer()
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
        public IActionResult CreateEmployer(EmployerProfileViewModel employerProfile)
        {
            
             employerProfile.Employer.ApprovalStatus = "Pending";
           
            _db.Employers.Add(employerProfile.Employer);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        //GET
        public async Task<IActionResult> CreateStudentAsync()
        {
			IEnumerable<Faculty> faculties = _db.Faculties;
			IEnumerable<Department> departments = _db.Departments;

			ApplicationUser user = await _userManager.GetUserAsync(User);

			var student = await _db.Students.Where(e => e.UserId == user.Id).SingleOrDefaultAsync();

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
				User = user,
				faculty = faculties,
				department = departments
			};

			return View(studentProfile);
		}
        //POST
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateStudent(Student student)
		{
			_db.Students.Add(student);
			_db.SaveChanges();
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> UpdateStudent()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var student = await _db.Students.Where(s => s.UserId == user.Id).SingleOrDefaultAsync();

            var facId = student.Faculty;

			IEnumerable<Faculty> faculties = _db.Faculties;
			IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));

			IEnumerable<Qualification> qualifications = _db.Qualifications.Where(q => q.StudentId == user.Id);
			IEnumerable<WorkExperience> workExperiences = _db.WorkExperiences.Where(w => w.StudentId == user.Id);
			IEnumerable<Referee> referees = _db.Referees.Where(r => r.StudentId == user.Id);

			if (student == null)
            {
                student = new Student
                {
                    User = user,
                    UserId = user.Id
                };
            }

            StudentProfileViewModel studentProfile = new StudentProfileViewModel();
            studentProfile.Student = student;
            studentProfile.User = user;
            studentProfile.faculty = faculties;
            studentProfile.department = departments;
            studentProfile.Qualifications = qualifications;
            studentProfile.WorkExperience = workExperiences;
            studentProfile.Referee = referees;


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
                studentRecord.IsSouthAfrican = studentProfile.Student.IsSouthAfrican;
                studentRecord.YearOfStudy = studentProfile.Student.YearOfStudy;
                studentRecord.Faculty = studentProfile.Student.Faculty;
                studentRecord.Department = studentProfile.Student.Department;
                studentRecord.Skills = studentProfile.Student.Skills;
                studentRecord.Achievements = studentProfile.Student.Achievements;
                studentRecord.Interests = studentProfile.Student.Interests;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");

        }

        //cascading drop-down
        public JsonResult GetDepartmentById(int id)
        {
            return Json(_db.Departments.Where(d => d.FacultyId.Equals(id)));
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
                
                employerRecord.ApprovalStatus = "Pending";
                employerRecord.isApproved = false;
               
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
		public IActionResult AwaitingApproval()
		{
			return View();
		}

        //GET
        public IActionResult AddQualification()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQualificationAsync(Qualification qualification)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            qualification.StudentId = user.Id;
            _db.Qualifications.Add(qualification);
            _db.SaveChanges();
            return RedirectToAction("UpdateStudent");
        }
        //GET
        public IActionResult ViewQualification(int id)
        {
            var selectedQualification = _db.Qualifications.Find(id);
            return View(selectedQualification);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQualification(Qualification qualification) 
        {
            var qualificationRecord = _db.Qualifications.Find(qualification.QualificationId);
            if (qualificationRecord != null)
            {
                qualificationRecord.Institution = qualification.Institution;
                qualificationRecord.StartDate = qualification.StartDate;
                qualificationRecord.EndDate = qualification.EndDate;
                qualificationRecord.QualificationName = qualification.QualificationName;
                qualificationRecord.Subjects = qualification.Subjects;
                qualificationRecord.Majors = qualification.Majors;
                qualificationRecord.SubMajors = qualification.SubMajors;
                qualificationRecord.Research = qualification.Research;
                _db.SaveChanges();
                return RedirectToAction("UpdateStudent");
            }
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQualification(Qualification qualification) 
        {
            var qualificationRecord = _db.Qualifications.Find(qualification.QualificationId);
            if (qualificationRecord != null)
            {
                _db.Qualifications.Remove(qualificationRecord);
                _db.SaveChanges();
                return RedirectToAction("UpdateStudent");
            }
			return RedirectToAction("UpdateStudent"); ;
        }
        //GET
        public IActionResult AddWorkExperience()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWorkExperienceAsync(WorkExperience workExperience)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            workExperience.StudentId = user.Id;
            _db.WorkExperiences.Add(workExperience);
            _db.SaveChanges();
            return RedirectToAction("UpdateStudent");
        }
		//GET
		public IActionResult ViewWorkExperience(int id)
		{
			var SelectedWorkExperience = _db.WorkExperiences.Find(id);
			return View(SelectedWorkExperience);
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditWorkExperience(WorkExperience workExperience)
        {
            var obj = _db.WorkExperiences.Find(workExperience.WorkExperienceId);
            if (obj != null)
            {
                obj.EmployerName = workExperience.EmployerName;
                obj.StartDate = workExperience.StartDate;
                obj.EndDate = workExperience.EndDate;
                obj.JobTitle = workExperience.JobTitle;
                obj.TasksAndResponsibilities = workExperience.TasksAndResponsibilities;
                _db.SaveChanges();
                return RedirectToAction("UpdateStudent");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWorkExperience(WorkExperience workExperience)
        {
            var obj = _db.WorkExperiences.Find(workExperience.WorkExperienceId);
            if (obj != null)
            {
                _db.WorkExperiences.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("UpdateStudent");
            }
            return View();
        }

		//GET
		public IActionResult AddReferee()
		{
			return View();
		}
		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRefereeAsync(Referee referee)
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			referee.StudentId = user.Id;
			_db.Referees.Add(referee);
			_db.SaveChanges();
			return RedirectToAction("UpdateStudent");
		}
		//GET
		public IActionResult ViewReferee(int id)
		{
			return View(_db.Referees.Find(id));
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditReferee(Referee referee)
		{
			var obj = _db.Referees.Find(referee.RefereeId);
			if (obj != null)
			{
				obj.Name = referee.Name;
                obj.JobTitle = referee.JobTitle;
                obj.Institution = referee.Institution;
                obj.Cell = referee.Cell;
                obj.Email = referee.Email;
				_db.SaveChanges();
				return RedirectToAction("UpdateStudent");
			}

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteReferee(Referee referee)
		{
			var obj = _db.Referees.Find(referee.RefereeId);
			if (obj != null)
			{
				_db.Referees.Remove(obj);
				_db.SaveChanges();
				return RedirectToAction("UpdateStudent");
			}
			return View();
		}
	}
}
