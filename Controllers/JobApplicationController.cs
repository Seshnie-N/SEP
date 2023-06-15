using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using SEP.Models.ViewModels;
using System.Data.Entity.Infrastructure;
using Microsoft.Extensions.Hosting;

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

            return View(applicationVM);
        }

        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, string description, int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var app = await _db.JobApplications.Where(a => a.ApplicationId == id).SingleOrDefaultAsync();
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
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
                }
            }
            //TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Create",new { id = app.PostId});
        }

      
        public IActionResult SubmitApplication()
        {
            return RedirectToAction("FilteredJobPosts", "Post");
        }

    }
}
