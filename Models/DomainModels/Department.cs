using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Department
    { 
        [Key]
		public int DepartmentId { get; set; }
        [ForeignKey(nameof(FacultyId))]
		public int FacultyId { get; set; }
		[ValidateNever]
		public Faculty Faculty { get; set; }
        [DisplayName("Department")]
		public string DepartmentName { get; set; }
    }
}
