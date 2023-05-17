using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;

namespace SEP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<HomeController> _logger;

		public ProfileController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
			_logger = logger;
			_db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            
			return View();
        }

        public async Task<IActionResult> Update()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var student = await _db.Students.Where(s => s.UserId == user.Id).SingleOrDefaultAsync();

            if (student == null)
            {
                student = new Student
                {
                    UserId = user.Id
                };
            }
           
            return View(student);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Student student)
        {
            //if (ModelState.IsValid)
            //{
            //    _db.Students.Update(student);
            //    _db.SaveChanges();
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    return View();
            //}

            _db.Students.Update(student);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

    }
}
