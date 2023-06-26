using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
	public class PostViewModel
	{
		public Post post { get; set; }

		public IEnumerable<Faculty> Faculty { get; set; }

		public IEnumerable<Department> Department { get; set; }

		public IEnumerable<PartTimeHours> PartTimeHours
		{ get; set; }	
	}
}
