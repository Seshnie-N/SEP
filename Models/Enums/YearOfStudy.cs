using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
	public enum YearOfStudy
	{
		[Display(Name = "1st Year")]
		FirstYear = 1,
		[Display(Name = "2nd Year")]
		SecondYear = 2,
		[Display(Name = "3rd Year")]
		ThirdYear = 3,
		Honours = 4,
		Graduate = 5,
		Masters = 6,
		PhD = 7,
		Postdoc	= 8
	}
}
