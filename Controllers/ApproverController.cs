using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;

namespace SEP.Controllers
{
    [Authorize]
    public class ApproverController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public ApproverController(ILogger<HomeController> logger, ApplicationDbContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            

            return View();
        }
        public string getSignedRole()
        {
            string role = "";
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Approve"))
                {
                    role = "Approver";
                }
            }
            return role;
        }
        public IActionResult pendingEmployers()
        {
            IEnumerable<ApplicationUser> applicationUsers = ((IEnumerable<ApplicationUser>)(from user in _db.Users
                                    join e in _db.Employers
                                    on user.Id equals e.UserId
									where e.isApproved == false
                                    select new
                                    {
										ApplicationUser = new ApplicationUser
                                        {
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,   
                                        }

                                    })
									);
			return View(applicationUsers);
        }


    }
}
