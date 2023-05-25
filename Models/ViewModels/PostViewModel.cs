using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
	public class PostViewModel
	{
		public Post post { get; set; }

		public IEnumerable<Faculty> faculty { get; set; }

		public IEnumerable<Department> department { get; set; }
	}
}
