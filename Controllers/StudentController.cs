using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Data;
using SEP.Models.DomainModels;
using SEP.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SEP.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        //GET
        public async Task<IActionResult> Create()
        {
            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments;

            ApplicationUser user = await _userManager.GetUserAsync(User);

            var student = await _db.Students.Where(e => e.UserId == user.Id).SingleOrDefaultAsync();

            student ??= new Student
            {
                User = user,
                UserId = user.Id
            };

            StudentProfileViewModel studentProfile = new()
            {
                Student = student,
                User = user,
                Faculty = faculties,
                Department = departments
            };

            return View(studentProfile);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ApplicationUser user = await _userManager.GetUserAsync(User);
            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments;
            StudentProfileViewModel studentProfile = new()
            {
                Student = student,
                User = user,
                Faculty = faculties,
                Department = departments
            };
            return View("CreateStudent", studentProfile);
        }
        //GET
        public async Task<IActionResult> Update()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var student = await _db.Students.Where(s => s.UserId == user.Id).SingleOrDefaultAsync();

            var facId = student.Faculty;

            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));

            IEnumerable<Qualification> qualifications = _db.Qualifications.Where(q => q.StudentId == user.Id);
            IEnumerable<WorkExperience> workExperiences = _db.WorkExperiences.Where(w => w.StudentId == user.Id);
            IEnumerable<Referee> referees = _db.Referees.Where(r => r.StudentId == user.Id);

            if (student == null) return NotFound();

            StudentProfileViewModel studentProfile = new()
            {
                Student = student,
                User = user,
                Faculty = faculties,
                Department = departments,
                Qualifications = qualifications,
                WorkExperience = workExperiences,
                Referee = referees
            };


            return View(studentProfile);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateStudentViewModel updateDetails)
        {
            if (!ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);

                var student = await _db.Students.SingleOrDefaultAsync(s => s.UserId == user.Id);

                var facId = student.Faculty;

                IEnumerable<Faculty> faculties = _db.Faculties;
                IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));

                IEnumerable<Qualification> qualifications = _db.Qualifications.Where(q => q.StudentId == user.Id);
                IEnumerable<WorkExperience> workExperiences = _db.WorkExperiences.Where(w => w.StudentId == user.Id);
                IEnumerable<Referee> referees = _db.Referees.Where(r => r.StudentId == user.Id);
                var studentProfile = new StudentProfileViewModel()
                {
                    Student = student,
                    User = user,
                    Faculty = faculties,
                    Department = departments,
                    Qualifications = qualifications,
                    WorkExperience = workExperiences,
                    Referee = referees
                };
                return View(studentProfile);
            }

            var studentRecord = _db.Students.Find(updateDetails.Student.UserId);
            ApplicationUser userRecord = await _userManager.GetUserAsync(User);

            if (studentRecord != null)
            {
                userRecord.FirstName = updateDetails.User.FirstName;
                userRecord.LastName = updateDetails.User.LastName;
                userRecord.PhoneNumber = updateDetails.User.PhoneNumber;
                userRecord.Email = updateDetails.User.Email;
                studentRecord.Address = updateDetails.Student.Address;
                studentRecord.IdNumber = updateDetails.Student.IdNumber;
                studentRecord.DriversLicense = updateDetails.Student.DriversLicense;
                studentRecord.CareerObjective = updateDetails.Student.CareerObjective;
                studentRecord.Gender = updateDetails.Student.Gender;
                studentRecord.Race = updateDetails.Student.Race;
                studentRecord.IsSouthAfrican = updateDetails.Student.IsSouthAfrican;
                studentRecord.YearOfStudy = updateDetails.Student.YearOfStudy;
                studentRecord.Faculty = updateDetails.Student.Faculty;
                studentRecord.Department = updateDetails.Student.Department;
                studentRecord.Skills = updateDetails.Student.Skills;
                studentRecord.Achievements = updateDetails.Student.Achievements;
                studentRecord.Interests = updateDetails.Student.Interests;
                _db.SaveChanges();
            }
            return RedirectToAction("StudentHome", "Home");

        }

        //cascading drop-down
        public JsonResult GetDepartmentById(int id)
        {
            return Json(_db.Departments.Where(d => d.FacultyId.Equals(id)));
        }

        //qualification 
        //GET
        public IActionResult AddQualification()
        {
            return View();
        }
        //GET
        //public JsonResult GetQualificationsList(int id)
        //{
        //    return Json(_db.Qualifications.Where());
        //}
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQualification(Qualification qualification)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            qualification.StudentId = user.Id;
            _db.Qualifications.Add(qualification);
            _db.SaveChanges();
            return RedirectToAction("Update");
        }
        //GET
        public IActionResult ViewQualification(Guid id)
        {
            var selectedQualification = _db.Qualifications.Find(id);
            if (selectedQualification == null) { return NotFound(); }
            if (selectedQualification.StudentId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }
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
                return RedirectToAction("Update");
            }

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQualification(Qualification qualification)
        {
            var qualificationRecord = _db.Qualifications.Find(qualification.QualificationId);
            if (qualificationRecord != null)
            {
                _db.Qualifications.Remove(qualificationRecord);
                _db.SaveChanges();
                return RedirectToAction("Update");
            }
            return RedirectToAction("Update"); ;
        }

        //work experience
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
            return RedirectToAction("Update");
        }
        //GET
        public IActionResult ViewWorkExperience(Guid id)
        {
            var selectedWorkExperience = _db.WorkExperiences.Find(id);
            if (selectedWorkExperience == null) { return NotFound(); }
            if (selectedWorkExperience.StudentId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }
            return View(selectedWorkExperience);
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
                return RedirectToAction("Update");
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
                return RedirectToAction("Update");
            }
            return View();
        }

        //referees
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
            return RedirectToAction("Update");
        }
        //GET
        public IActionResult ViewReferee(Guid id)
        {
            var obj = _db.Referees.Find(id);
            if (obj.StudentId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }
            return View(obj);
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
                return RedirectToAction("Update");
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
                return RedirectToAction("Update");
            }
            return View();
        }

    }
}
