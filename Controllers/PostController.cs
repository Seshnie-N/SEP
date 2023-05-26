using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

			return View(postViewModel);
		}
		[HttpPost]
		public IActionResult CreatePost(PostViewModel postViewModelObject)
		{
			postViewModelObject.post.postStatus = "Pending";

			_db.Posts.Add(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}


		public IActionResult Update(int id)
		{

			Post postObj = _db.Posts.Find(id);
			IEnumerable<Faculty> faculties = _db.Faculties;
			IEnumerable<Department> departments = _db.Departments;

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

			return View(postViewModel);
		}
		[HttpPost]
		public IActionResult UpdatePost(PostViewModel postViewModelObject)
		{
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
			postViewModelObject.post.postStatus = "Withrawed";
			_db.Posts.Update(postViewModelObject.post);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		// get Departments by Id
		public JsonResult GetDepartmentById(int id)
		{
			return Json( _db.Departments.Where(d => d.FacultyId.Equals(id)) );
		}

	}
}
