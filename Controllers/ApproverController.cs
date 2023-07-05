using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP.Data;
using SEP.Models.DomainModels;
using SEP.Models.ViewModels;

namespace SEP.Controllers
{
    [Authorize(Roles ="Approver")]
    public class ApproverController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly INotyfService _toastNotification;
		public ApproverController(ApplicationDbContext db, SignInManager<ApplicationUser> signInManager, INotyfService notyfService)
        {
            _db = db;
            _signInManager = signInManager;
            _toastNotification = notyfService;
        }

        public string GetSignedRole()
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
        public IActionResult PendingEmployers()
        {
            return View();
        }

        public JsonResult GetPendingEmployers()
        {
            var applicationUsers = (from user in _db.Users
                                    join e in _db.Employers
                                    on user.Id equals e.UserId
                                    where e.IsApproved == false
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

        public IActionResult ApproverUpdateEmployer(string id)
        {

            ApplicationUser user = _db.Users.Find(id);
            var employer = _db.Employers.Find(id);

            EmployerProfileViewModel employerProfile = new()
            {
                User = user,
                Employer = employer
            };

            return View(employerProfile);
        }

        //POST
        [HttpPost]
        public IActionResult ApproverAcceptEmployer(EmployerProfileViewModel employerProfile)
        {
            //updated fields
            employerProfile.Employer.IsApproved = true;
            employerProfile.Employer.ApprovalStatus = "Approved";
            _db.Update(employerProfile.Employer);
            _db.SaveChanges();
			_toastNotification.Success("Employer is approved.");
			_toastNotification.Information("Employer can now access the system.");
            return RedirectToAction("PendingEmployers");
        }

        //POST
        [HttpPost]
        public IActionResult ApproverRejectEmployer(EmployerProfileViewModel employerProfile)
        {

            //updated fields
            employerProfile.Employer.IsApproved = employerProfile.Employer.IsApproved;	
			employerProfile.Employer.ApprovalStatus = "Rejected";
            _db.Update(employerProfile.Employer);
            _db.SaveChanges();
			_toastNotification.Success("Employer is rejected.");
			return RedirectToAction("PendingEmployers");


        }

        public IActionResult PendingPosts()
        {
            IEnumerable<Post> posts = _db.Posts.Where(p => p.IsApproved == false && p.PostStatus.Equals("Pending"));
            return View(posts);
        }

        public JsonResult GetPendingPosts()
        {
            return Json(_db.Posts.Where(p => p.IsApproved == false && p.PostStatus.Equals("Pending")));
        }

        public IActionResult ApproverUpdatePost(Guid id)
        {

            Post postObj = _db.Posts.Find(id);
            var facId = postObj.FacultyName;

            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));
            IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;

            PostViewModel postViewModel = new();

            if (postObj != null)
            {
                postViewModel.post = postObj;
            }
            else
            {
                return NotFound();
            }
            postViewModel.Faculty = faculties;
            postViewModel.Department = departments;
            postViewModel.PartTimeHours = partTimeHours;

            return View(postViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ApproverUpdatePost(PostViewModel postViewModelObject)
        {
            _db.Posts.Update(postViewModelObject.post);
            _db.SaveChanges();
			_toastNotification.Success("Job post successfully updated.");
        }
       
        public async Task<IActionResult> ApproverApprovePost(PostViewModel postViewModelObject)
        {
           //var postToBeApproved = await _db.Posts.Where(p => p.PostId == id).SingleOrDefaultAsync();
            postViewModelObject.post.PostStatus = "Approved";
            postViewModelObject.post.ApprovalStatus = "Approved";
            postViewModelObject.post.IsApproved = true;
            _db.Posts.Update(postViewModelObject.post);
            _db.SaveChanges();
			_toastNotification.Success("Job post accepted.");
			_toastNotification.Information("students can apply for this post.");
			return RedirectToAction("PendingPosts");
        }
       
        public async Task<IActionResult> ApproverRejectPost(PostViewModel postViewModelObject)
        {
            // var postToBeRejected = await _db.Posts.Where(p => p.PostId == id).SingleOrDefaultAsync();
            postViewModelObject.post.ApprovalStatus = "Rejected";
            _db.Posts.Update(postViewModelObject.post);
            _db.SaveChanges();
			_toastNotification.Success("Job post rejected.");
			return RedirectToAction("PendingPosts");
        }

        public IActionResult ApproverQueryPost(PostViewModel postViewModelObject)
        {
            //var postToBeQueried = await _db.Posts.Where(p => p.PostId == id).SingleOrDefaultAsync();
            postViewModelObject.post.ApprovalStatus = "Queried";
            _db.Posts.Update(postViewModelObject.post);
            _db.SaveChanges();
            _toastNotification.Success("Job post queried.");
            return RedirectToAction("PendingPosts");
        }
        // get all faculties
        public JsonResult GetFaculties()
        {
            return Json(_db.Faculties);
        }

        // get Departments by Id for cascading
        public JsonResult GetDepartmentListById(int id)
        {
            return Json(_db.Departments.Where(d => d.FacultyId.Equals(id)));
        }
        // get Departments by Id for View
        public JsonResult GetDepartmentNameById(int id)
        {
            return Json(_db.Departments.Where(d => d.DepartmentId.Equals(id)));
        }

        /*public IActionResult Stats()
        {
            List<Post> postsGroupByDepartment = _db.Posts
                                                .GroupBy(p => p.DepartmentName)
                                                .Select(p => new Post
                                                {
                                                    DepartmentName = p.First().DepartmentName,
                                                    HourlyRate = p.Average(p => p.HourlyRate),
                                                }).ToList();
            List<string> departments = new();
            List<decimal> averageHourlyRate = new();
            foreach (Post post in postsGroupByDepartment)
            {
                departments.Add(post.DepartmentName);
                averageHourlyRate.Add(post.HourlyRate);
            }
            var statsViewModel = new DepartmentHourlyRateStatViewModel()
            {
                Departments = departments,
                HourlyRates = averageHourlyRate
            };
            return View(statsViewModel);
        }*/

        //[HttpGet]
        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> PostData()
        //{
        //    var posts = _db.Posts.Distinct().Select(p =>new { p.DepartmentName , p.HourlyRate}).ToList();
        //    return Json(posts);
        //}

    }
}
