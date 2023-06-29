using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SEP.Models.DomainModels;

namespace SEP.Models.ViewModels
{
	public class PostViewModel
	{
		public Post post { get; set; }
		[ValidateNever]
		public IEnumerable<Faculty> Faculty { get; set; }
		[ValidateNever]
		public IEnumerable<Department> Department { get; set; }
		[ValidateNever]
		public IEnumerable<PartTimeHours> PartTimeHours
		{ get; set; }	
	}
}
