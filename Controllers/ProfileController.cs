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

        public async Task<IActionResult> Index()
        {
            

			return View();
        }

        public async Task<IActionResult> UpdateAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            Student? student = await _db.Students.SingleOrDefaultAsync(s => s.User.Id.Equals(user.Id));

            if (student == null)
            {
                student = new Student
                {
                    User = user,
                    UserId = user.Id
                };
            }
            else
            {
                _logger.LogInformation($"student for {user.FirstName} exists");
            }
            return View(student);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Student student)
        {
            _db.Students.Update(student);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
