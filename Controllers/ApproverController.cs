using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using SEP.Models.ViewModels;

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
            return View();
        }



        public JsonResult getPendingEmployers()
        {
            var applicationUsers = (from user in _db.Users
                                    join e in _db.Employers
                                    on user.Id equals e.UserId
                                    where e.isApproved == false
                                    select new
                                    {
                                        id = user.Id,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        LegalName = e.BusinessName,
                                        TradingName = e.TradingName,
                                        RegNumber = e.CompanyRegistrationNumber,
                                        status = e.ApprovalStatus,
                                    }
                                    );
            return Json(applicationUsers);
        }

        public IActionResult approverUpdateEmployer(string id)
        {

            ApplicationUser user = _db.Users.Find(id);
            var employer = _db.Employers.Find(id);

            EmployerProfileViewModel employerProfile = new EmployerProfileViewModel();

            employerProfile.User = user;
            employerProfile.Employer = employer;



            return View(employerProfile);
        }

        //POST
        [HttpPost]
        public IActionResult approverAcceptEmployer(EmployerProfileViewModel employerProfile)
        {
            //var employerRecord = _db.Employers.Find(employerProfile.Employer.UserId);
            //ApplicationUser userRecord = _db.Users.Find(employerProfile.Employer.UserId);


            //userRecord.FirstName = employerProfile.User.FirstName;
            //userRecord.LastName = employerProfile.User.LastName;
            //userRecord.PhoneNumber = employerProfile.User.PhoneNumber;
            //userRecord.Email = employerProfile.User.Email;
            //employerRecord.Title = employerProfile.Employer.Title;
            //employerRecord.JobTitle = employerProfile.Employer.JobTitle;
            //employerRecord.CompanyRegistrationNumber = employerProfile.Employer.CompanyRegistrationNumber;
            //employerRecord.TradingName = employerProfile.Employer.TradingName;
            //employerRecord.BusinessName = employerProfile.Employer.BusinessName;
            //employerRecord.Address = employerProfile.Employer.Address;
            //employerRecord.BusinessType = employerProfile.Employer.BusinessType;

            //employerRecord.ApproverNote = employerProfile.Employer.ApproverNote;
            //employerRecord.isInternal = employerProfile.Employer.isInternal;
            //updated fields
            employerProfile.Employer.isApproved = true;
            employerProfile.Employer.ApprovalStatus = "Approved";
            _db.Update(employerProfile.Employer);
            _db.SaveChanges();
            return RedirectToAction("pendingEmployers");
        }

        //POST
        [HttpPost]
        public IActionResult approverRejectEmployer(EmployerProfileViewModel employerProfile)
        {

            //updated fields
            employerProfile.Employer.isApproved = employerProfile.Employer.isApproved;
            employerProfile.Employer.ApprovalStatus = "Rejected";
            _db.Update(employerProfile.Employer);
            _db.SaveChanges();
            return RedirectToAction("pendingEmployers");


        }

        public IActionResult pendingPosts()
        {
            IEnumerable<Post> posts = _db.Posts.Where(p => p.isApproved == false && p.postStatus.Equals("Pending"));
            return View(posts);
        }

        public IActionResult approverUpdatePost(int id)
        {

            Post postObj = _db.Posts.Find(id);
            var facId = postObj.facultyName;

            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));
            IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;

            PostViewModel postViewModel = new PostViewModel();

            if (postObj != null)
            {
                postViewModel.post = postObj;
            }
            else
            {
                return NotFound();
            }
            postViewModel.faculty = faculties;
            postViewModel.department = departments;
            postViewModel.partTimeHours = partTimeHours;

            return View(postViewModel);
        }
        [HttpPost]

        public IActionResult approverUpdatePost(PostViewModel postViewModelObject)
        {
            _db.Posts.Update(postViewModelObject.post);
            _db.SaveChanges();
            return RedirectToAction("pendingPosts");
        }
       
        public async Task<IActionResult> approverApprovePost(int id)
        {
            var postToBeApproved = await _db.Posts.Where(p => p.postId == id).SingleOrDefaultAsync();
            postToBeApproved.postStatus = "Approved";
            postToBeApproved.approvalStatus = "Approved";
            postToBeApproved.isApproved = true;
            _db.Posts.Update(postToBeApproved);
            _db.SaveChanges();
            return RedirectToAction("pendingPosts");
        }
       
        public async Task<IActionResult> approverRejectPost(int id)
        {
            var postToBeRejected = await _db.Posts.Where(p => p.postId == id).SingleOrDefaultAsync();
            postToBeRejected.approvalStatus = "Rejected";
            _db.Posts.Update(postToBeRejected);
            _db.SaveChanges();
            return RedirectToAction("pendingPosts");
        }
        
        public async Task<IActionResult> approverQueryPostAsync(int id)
        {
            var postToBeQueried = await _db.Posts.Where(p => p.postId == id).SingleOrDefaultAsync();
            postToBeQueried.approvalStatus = "Queried";
            _db.Posts.Update(postToBeQueried);
            _db.SaveChanges();
            return RedirectToAction("pendingPosts");
        }

    }
}
