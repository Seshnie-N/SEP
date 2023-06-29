using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP.Data;
using SEP.Models.DomainModels;
using SEP.Models.ViewModels;

namespace SEP.Controllers
{
    [Authorize]
    public class PostController : Controller
	{
		private readonly ApplicationDbContext _db;

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<HomeController> _logger;
        //ApplicationUser user = await _userManager.GetUserAsync(User);

        public PostController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_db = db;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			IEnumerable<Post> posts = _db.Posts.Where(p => p.EmployerId.Equals(user.Id));

			return View(posts);
		}

		public async Task<JsonResult> allUserPosts() {
			ApplicationUser user = await _userManager.GetUserAsync(User);
			return Json(_db.Posts.Where(p => p.UserID.Equals(user.Id)));
        }

		public async Task<IActionResult> Create()
		{

			IEnumerable<Faculty> faculties = _db.Faculties;
			IEnumerable<Department> departments = _db.Departments;
			IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;

			ApplicationUser user = await _userManager.GetUserAsync(User);


			PostViewModel postViewModel = new();
			Post newPost = new();
			newPost.EmployerId = user.Id;
			newPost.StartDate = DateTime.Now;
			newPost.EndDate = DateTime.Now;
			newPost.ApplicationClosingDate = DateTime.Now;
			postViewModel.post = newPost;
			postViewModel.Faculty = faculties;
			postViewModel.Department = departments;
			postViewModel.PartTimeHours = partTimeHours;

			return View(postViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Create(PostViewModel postViewModelObject)
		{
			ModelState.Remove("Post.PostStatus");
			ModelState.Remove("Post.ApprovalStatus");
			if (!ModelState.IsValid)
			{
				IEnumerable<Faculty> faculties = _db.Faculties;
				IEnumerable<Department> departments = _db.Departments;
				IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;

				ApplicationUser user = await _userManager.GetUserAsync(User);


				PostViewModel postViewModel = new();
				Post newPost = new();
				newPost.EmployerId = user.Id;
				newPost.StartDate = DateTime.Now;
				newPost.EndDate = DateTime.Now;
				newPost.ApplicationClosingDate = DateTime.Now;
				postViewModel.post = newPost;
				postViewModel.Faculty = faculties;
				postViewModel.Department = departments;
				postViewModel.PartTimeHours = partTimeHours;
				return View(postViewModel);
			}
			postViewModelObject.post.PostStatus = "Pending";
			postViewModelObject.post.ApprovalStatus = "Pending";

			_db.Posts.Add(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}


		public IActionResult Update(int id)
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
			postViewModelObject.post.PostStatus = "Closed";
			_db.Posts.Update(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult WithdrawPost(PostViewModel postViewModelObject)
		{
			postViewModelObject.post.PostStatus = "Withdrawn";
			_db.Posts.Update(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		// get all faculties
		public JsonResult GetFaculties()
		{
			return Json(_db.Faculties);
		}
		// get Departments by Id
		public JsonResult GetDepartmentById(int id)
		{
			return Json( _db.Departments.Where(d => d.FacultyId.Equals(id)) );
		}

		public async Task<ActionResult> Details(Guid id)
		{
			var post = await _db.Posts.FirstAsync(p => p.PostId == id);
			if (post == null)
			{
				return NotFound();
			}

            var facId = post.FacultyName;
            IEnumerable<Faculty> faculties = _db.Faculties;
            IEnumerable<Department> departments = _db.Departments.Where(d => d.FacultyId.Equals(facId));
            IEnumerable<PartTimeHours> partTimeHours = _db.partTimeHours;

			PostViewModel postViewModel = new()
			{
				post = post,
				Faculty = faculties,
				Department = departments,
				PartTimeHours = partTimeHours
			};

            return View(postViewModel);
		}

	}
}
