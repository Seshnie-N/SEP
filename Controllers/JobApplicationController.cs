﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using SEP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using SEP.Data;

namespace SEP.Controllers
{
    [Authorize]
    public class JobApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public JobApplicationController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        private readonly string[] permittedExtensions = { ".jpg", ".pdf", ".png" };

        public async Task<IActionResult> Create(Guid id) 
        {
            var post = await _db.Posts.Where(p => p.PostId == id).SingleOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.GetUserAsync(User);

            var application = await _db.JobApplications.Where(a => a.StudentId == user.Id && a.PostId == post.PostId).SingleOrDefaultAsync();
            if (application == null)
            {
                application = new JobApplication
                {
                    PostId = post.PostId,
                    StudentId = user.Id,
                    Status = "Incomplete"
                };
                _db.JobApplications.Add(application);
                _db.SaveChanges();
            } 
            var documents = await _db.Documents.Where(d => d.JobApplicationId == application.ApplicationId).ToListAsync();

            var applicationVM = new ApplicationViewModel
            {
                Application = application,
                JobTitle = post.JobTitle,
                Documents = documents
            };
            ViewBag.Message = TempData["Message"];
            ViewBag.MessageType = TempData["MessageType"];
            return View(applicationVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadDocument(List<IFormFile> files, string description, Guid id)
        {
            var application = await _db.JobApplications.Where(a => a.ApplicationId == id).SingleOrDefaultAsync();
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                fileName += id.ToString();
                var filePath = Path.Combine(basePath, fileName);
                if (!permittedExtensions.Contains(extension))
                {
                    TempData["Message"] = "Invalid file type. You can only upload .png, .jpg and .pdf files.";
                    TempData["MessageType"] = "Danger";
                    return RedirectToAction("Create", new { id = application.PostId });
                }
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new ApplicationDocument
                    {
                        CreatedDate = DateTime.Now,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        Description = description,
                        FilePath = filePath,
                        JobApplicationId = id
                    };
                    _db.Documents.Add(fileModel);
                    _db.SaveChanges();
                    TempData["Message"] = "File successfully uploaded.";
                    TempData["MessageType"] = "Success";
                } else
                {
                    TempData["Message"] = "File has already been uploaded.";
                    TempData["MessageType"] = "Danger";
                }
            }
  
            return RedirectToAction("Create",new { id = application.PostId});
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> ApplicationHistory()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var applications = await _db.JobApplications.Include(a => a.Post).Where(a => a.StudentId == user.Id).ToListAsync();
            
            return View(applications);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var application = await _db.JobApplications.Where(a => a.ApplicationId == id).Include("Post").SingleOrDefaultAsync();

            var facId = application.Post.FacultyName;
            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));
            IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;
            var documents = await _db.Documents.Where(d => d.JobApplicationId == application.ApplicationId).ToListAsync();

            ApplicationDetailsViewModel applicationDetails = new()
            {
                JobApplication = application,
                Faculty = faculties,
                Department = departments,
                PartTimeHours = partTimeHours,
                Documents = documents
            };
            return View(applicationDetails);
        }

        public async Task<IActionResult> ViewDocumentAsync(Guid id)
        {
            var file = await _db.Documents.Where(x => x.DocumentId == id).FirstOrDefaultAsync();

            if (file == null) return NotFound();
            var stream = new FileStream(file.FilePath, FileMode.Open);
            return File(stream, file.FileType);
        }
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            var file = await _db.Documents.Include("JobApplication").Where(d => d.DocumentId == id).FirstOrDefaultAsync();
            if (file == null) return NotFound();
            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            _db.Documents.Remove(file);
            _db.SaveChanges();
            TempData["Message"] = $"Removed {file.Name + file.Extension} successfully.";
            TempData["MessageType"] = "Success";
            return RedirectToAction("Create", new { id = file.JobApplication.PostId });
        }
        
        public async Task<IActionResult> SubmitApplication(Guid id)
        {
            //change application status to pending 
            var application = await _db.JobApplications.Where(a => a.ApplicationId == id).SingleOrDefaultAsync();
            if (application == null)
            {
                return NotFound();
            }
            var documents = await _db.Documents.Where(d => d.JobApplicationId == id).ToListAsync();
            if (documents.Count > 0)
            {
                application.Status = "Pending";
                _db.Update(application);
                _db.SaveChanges();
                return RedirectToAction("FilteredJobPosts", "Student");
            } else
            {
                TempData["Message"] = "You cannot submit an empty application. Please upload the necessary documents.";
                TempData["MessageType"] = "Danger";
                return RedirectToAction("Create", new { id = application.PostId });
            }
        }

        //after lookup tables added, can include faculty and department to get the actual values instead of numbers
        public async Task<IActionResult> ViewApplicants(Guid id)
        {
            var applications = await _db.JobApplications.Where(a => a.PostId == id).Where(a => !a.Status.Equals("Incomplete")).Include(a => a.Student).ThenInclude(s => s.User).ToListAsync();
            var post = await _db.Posts.Where(p => p.PostId == id).SingleOrDefaultAsync();

            var viewmodel = new ViewApplicantsViewModel
            {
                PostId = id,
                Applications = applications,
                JobTitle = post.JobTitle,
                JobDescription = post.JobDescription
            };
            return View(viewmodel);
        }

        public async Task<IActionResult> ViewApplicantDetails(Guid id)
        {
            var application = await _db.JobApplications.Where(a => a.ApplicationId == id).Include("Post").Include(a => a.Student).ThenInclude(s => s.User).SingleOrDefaultAsync();
            var documents = await _db.Documents.Where(d => d.JobApplicationId == application.ApplicationId).ToListAsync();
            var faculty = await _db.Faculties.Where(f => f.FacultyId == application.Student.Faculty).SingleOrDefaultAsync();
            IEnumerable<Qualification> qualifications = _db.Qualifications.Where(q => q.StudentId == application.Student.UserId);
            IEnumerable<WorkExperience> workExperiences = _db.WorkExperiences.Where(w => w.StudentId == application.Student.UserId);
            IEnumerable<Referee> referees = _db.Referees.Where(r => r.StudentId == application.Student.UserId);
            var statusList = new List<SelectListItem>
            {
                new SelectListItem {Text="Interview" ,Value="Interview"},
                new SelectListItem {Text="On Hold" ,Value="On Hold" },
                new SelectListItem {Text="Rejected",Value= "Rejected" },
                new SelectListItem {Text="Appointed",Value="Appointed" }
            };
            var viewmodel = new ApplicantDetailsViewModel
            {
                PostId = application.Post.PostId,
                Application = application,
                JobTitle = application.Post.JobTitle,
                JobDescription = application.Post.JobDescription,
                Documents = documents,
                Qualifications = qualifications,
                Referee = referees,
                WorkExperience = workExperiences,
                Status = application.Status,
                statusList = statusList,
                Faculty = faculty.FacultyName
            };

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApplicationStatus(Guid id, ApplicantDetailsViewModel vm)
        {
            var application = await _db.JobApplications.Where(a => a.ApplicationId == id).SingleOrDefaultAsync();
            //if (ModelState.IsValid)
            //{
                var status = vm.Status;
                if (status != application.Status)
                {
                    application.Status = status;
                    _db.JobApplications.Update(application);
                    _db.SaveChanges();
                }
                //return RedirectToAction("ViewApplicants", new { id = application.PostId });
            //}
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            return RedirectToAction("ViewApplicants",new {id = application.PostId});
        }

        public async Task<IActionResult> QualificationDetails(Guid id)
        {
            var qualification = await _db.Qualifications.Where(q => q.QualificationId == id).SingleOrDefaultAsync();
            if (qualification == null) return NotFound();
            return View(qualification);
        }

        public async Task<IActionResult> WorkExperienceDetails(Guid id)
        {
            var workExperience = await _db.WorkExperiences.Where(q => q.WorkExperienceId == id).SingleOrDefaultAsync();
            if (workExperience == null) return NotFound();
            return View(workExperience);
        }

        public async Task<IActionResult> RefereeDetails(Guid id)
        {
            var referee = await _db.Referees.Where(q => q.RefereeId == id).SingleOrDefaultAsync();
            if (referee == null) return NotFound();
            return View(referee);
        }
    }
}
