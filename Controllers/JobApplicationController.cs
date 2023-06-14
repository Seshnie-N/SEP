using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using SEP.Models.ViewModels;
using System.Data.Entity.Infrastructure;

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
        private async Task<List<ApplicationDocument>> LoadFiles()
        {
            //List<ApplicationDocument> uploadedDocuments = await _db.Documents.Where(d => d.JobApplicationId == id).ToListAsync();
            List<ApplicationDocument> uploadedDocuments = await _db.Documents.ToListAsync();
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
            var student = await _db.Students.Where(s => s.UserId == user.Id).SingleOrDefaultAsync();
            var documents = await LoadFiles();

            var applicationVM = new ApplicationViewModel
            {
                Post = post,
                Student = student,
                Status = "Pending",
                Documents = documents
            };

            return View(applicationVM);
        }

        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
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
                    };
                    _db.Documents.Add(fileModel);
                    _db.SaveChanges();
                }
            }
            //TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Create");
        }

    }
}
