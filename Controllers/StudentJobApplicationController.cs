using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP.Data;
using SEP.Models.FrontEndModels;
using SEP.Mapper;
using SEP.Models.DomainModels;

namespace SEP.Controllers
{
    public class StudentJobApplicationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public StudentJobApplicationController(IMapper mapper, ApplicationDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }


        public IActionResult Index()
        {
            Post post = _db.Posts.Find(1);
            var st = _mapper.Map<StudentApplicationDetailsDto>(post);
            return View(st);
        }

        [HttpGet]
        public Post getPost()
        {
            Post post = _db.Posts.Find(1);
            return post;
        }

        public IActionResult uploadDoc(string? id)
        {
            return View();
        }
    }
}
