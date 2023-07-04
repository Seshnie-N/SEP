using SEP.Models.DomainModels;


namespace SEP.Models.ViewModels
{
    public class StudentPostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public bool ProfileComplete { get; set; }
    }
}
