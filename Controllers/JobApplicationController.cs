using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using SEP.Models.ViewModels;

namespace SEP.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public JobApplicationController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //potentially move to a utility class
        private async Task<List<ApplicationDocument>> LoadFiles(int id)
        {
            List<ApplicationDocument> uploadedDocuments = await _db.Documents.Where(d => d.JobApplicationId == id).ToListAsync();
            return uploadedDocuments;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int id) 
        {
            var post = await _db.Posts.Where(p => p.postId == id).SingleOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.GetUserAsync(User);

            var application = await _db.JobApplications.Where(a => a.StudentId == user.Id && a.PostId == post.postId).SingleOrDefaultAsync();
            if (application == null)
            {
                application = new JobApplication
                {
                    PostId = post.postId,
                    StudentId = user.Id,
                    Status = "Incomplete"
                };
                _db.JobApplications.Add(application);
                _db.SaveChanges();
            }
            var documents = await LoadFiles(application.ApplicationId);

            var applicationVM = new ApplicationViewModel
            {
                Application = application,
                JobTitle = post.jobTitle,
                Documents = documents
            };
            ViewBag.Message = TempData["Message"];
            ViewBag.MessageType = TempData["MessageType"];
            return View(applicationVM);
        }

        private string[] permittedExtensions = { ".jpg", ".pdf", ".png" };

        [HttpPost]
        public async Task<IActionResult> UploadDocument(List<IFormFile> files, string description, int id)
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
                    TempData["Message"] = "File successfully uploaded to File System.";
                    TempData["MessageType"] = "Success";
                } else
                {
                    TempData["Message"] = "File has already been uploaded.";
                    TempData["MessageType"] = "Danger";
                }
            }
  
            return RedirectToAction("Create",new { id = application.PostId});
        }

        public async Task<IActionResult> ApplicationHistory()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var applications = await _db.JobApplications.Include("Post").Where(a => a.StudentId == user.Id).ToListAsync();
            
            return View(applications);
        }

        public async Task<IActionResult> Details(int id)
        {
            var application = await _db.JobApplications.Where(a => a.ApplicationId == id).Include("Post").SingleOrDefaultAsync();

            var facId = application.Post.facultyName;
            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));
            IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;
            var documents = await LoadFiles(application.ApplicationId);

            ApplicationDetailsViewModel applicationDetails = new()
            {
                JobApplication = application,
                faculty = faculties,
                department = departments,
                partTimeHours = partTimeHours,
                Documents = documents
            };
            return View(applicationDetails);
        }

        public async Task<IActionResult> ViewDocumentAsync(Guid id)
        {
            var file = await _db.Documents.Where(x => x.DocumentId == id).FirstOrDefaultAsync();
            if (file == null) return null;
            var stream = new FileStream(file.FilePath, FileMode.Open);
            return File(stream, file.FileType);
        }
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            var file = await _db.Documents.Include("JobApplication").Where(d => d.DocumentId == id).FirstOrDefaultAsync();
            if (file == null) return null;
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

        public async Task<IActionResult> SubmitApplication(int id)
        {
            //change application status to pending 
            var application = await _db.JobApplications.Where(a => a.ApplicationId == id).SingleOrDefaultAsync();
            if (application == null)
            {
                return NotFound();
            }
            var documents = await _db.Documents.Where(d => d.JobApplicationId == id).ToListAsync();
            if (documents.Count() > 0)
            {
                application.Status = "Pending";
                _db.Update(application);
                _db.SaveChanges();
                return RedirectToAction("FilteredJobPosts", "Post");
            } else
            {
                TempData["Message"] = "You cannot submit an empty application. Please upload the necessary documents.";
                TempData["MessageType"] = "Danger";
                return RedirectToAction("Create", new { id = application.PostId });
            }
        }

    }
}
