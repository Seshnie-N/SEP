using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP.Areas.Identity.Data;
using SEP.Models.DomainModels;
using SEP.Models.ViewModels;

namespace SEP.Controllers
{
    public class PostController : Controller
	{
		private readonly ApplicationDbContext _db;

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<HomeController> _logger;

		public PostController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_db = db;
			_userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			IEnumerable<Post> posts = _db.Posts.Where(p => p.UserID.Equals(user.Id));

			return View(posts);
		}

		public async Task<IActionResult> Create()
		{

			IEnumerable<Faculty> faculties = _db.Faculties;
			IEnumerable<Department> departments = _db.Departments;
			IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;

			ApplicationUser user = await _userManager.GetUserAsync(User);


			PostViewModel postViewModel = new PostViewModel();
			Post newPost = new Post();
			newPost.UserID = user.Id;
			newPost.startDate = DateTime.Now;
			newPost.endDate = DateTime.Now;
			newPost.applicationClosingDate = DateTime.Now;
			postViewModel.post = newPost;
			postViewModel.faculty = faculties;
			postViewModel.department = departments;
			postViewModel.partTimeHours = partTimeHours;

			return View(postViewModel);
		}
		[HttpPost]
		public IActionResult CreatePost(PostViewModel postViewModelObject)
		{
			postViewModelObject.post.postStatus = "Pending";
			postViewModelObject.post.approvalStatus = "Pending";

			_db.Posts.Add(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}


		public IActionResult Update(int id)
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
		public IActionResult UpdatePost(PostViewModel postViewModelObject)
		{
			//change post status to pending and wait for approval
			//if post is approved, do not allow update
			_db.Posts.Update(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult ClosePost(PostViewModel postViewModelObject)
		{
			postViewModelObject.post.postStatus = "Closed";
			_db.Posts.Update(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult WithdrawPost(PostViewModel postViewModelObject)
		{
			postViewModelObject.post.postStatus = "Withdrawn";
			_db.Posts.Update(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		// get Departments by Id
		public JsonResult GetDepartmentById(int id)
		{
			return Json( _db.Departments.Where(d => d.FacultyId.Equals(id)) );
		}

        public async Task<IActionResult> FilteredJobPosts()
		{
			var user = await _userManager.GetUserAsync(User);
            var student = await _db.Students.Where(s => s.UserId == user.Id).SingleOrDefaultAsync();

			var predicate = PredicateBuilder.New<Post>();
			//predicate = predicate.And(p => p.isApproved);
			predicate = predicate.And(p => p.postStatus.Equals("Approved"));
			//filter if student is not a south african citizen
			if (!student.IsSouthAfrican)
			{
				predicate = predicate.And(p => !p.limitedToSA);
			}

			//filter by student's year of study
            string YearOfStudy = student.YearOfStudy.ToString();
			switch (YearOfStudy)
			{
				case "FirstYear":
					predicate = predicate.And(p => p.limitedTo1stYear);
					break;
                case "SecondYear":
                    predicate = predicate.And(p => p.limitedTo2ndYear);
                    break;
                case "ThirdYear":
                    predicate = predicate.And(p => p.limitedTo3rdYear);
                    break;
                case "Honours":
                    predicate = predicate.And(p => p.limitedToHonours);
                    break;
                case "Graduate":
                    predicate = predicate.And(p => p.limitedToGraduate);
                    break;
                case "Masters":
                    predicate = predicate.And(p => p.limitedToMasters);
                    break;
				case "PhD":
                    predicate = predicate.And(p => p.limitedToPhd);
                    break;
				case "Postdoc":
                    predicate = predicate.And(p => p.limitedToPostdoc);
                    break;
            }
            predicate = predicate.Or(p => /*p.isApproved &&*/ p.postStatus.Equals("Approved") && !p.limitedTo1stYear && !p.limitedTo2ndYear && !p.limitedTo3rdYear && !p.limitedToHonours && !p.limitedToGraduate && !p.limitedToMasters && !p.limitedToPhd && !p.limitedToPostdoc);


			//filter out job posts that have already been applied to
			var postsAppliedToIds = _db.JobApplications.Where(a => a.StudentId == user.Id).Select(a => a.PostId);

			var posts = _db.Posts.Where(predicate).ToList();
			posts = posts.Where(p => !postsAppliedToIds.Contains(p.postId)).ToList();
			

            List<StudentPostViewModel> studentPosts = new();

            foreach (var post in posts )
			{
				var studentPostsVm = new StudentPostViewModel
				{
					postId = post.postId,
					jobTitle = post.jobTitle,
					departmentName = post.departmentName,
					jobType = post.jobType,
					startDate = post.startDate,
					endDate	= post.endDate,
					partTimeHour = post.partTimeHour,
					hourlyRate = post.hourlyRate
				};
				studentPosts.Add(studentPostsVm);
			}


			return View(studentPosts);
		}

		public async Task<ActionResult> Details(int id)
		{
			var post = await _db.Posts.FirstAsync(p => p.postId == id);
			if (post == null)
			{
				return NotFound();
			}

            var facId = post.facultyName;
            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));
            IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;

			PostViewModel postViewModel = new()
			{
				post = post,
				faculty = faculties,
				department = departments,
				partTimeHours = partTimeHours
			};

            return View(postViewModel);
		}

	}
}
