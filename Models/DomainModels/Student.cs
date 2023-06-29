using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SEP.Data;
using SEP.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    [Table("Student")]
    public class Student 
    {
        [Key,ForeignKey(nameof(User))]
        public string UserId { get; set; }
		//navigation property
		[ValidateNever]
		public virtual ApplicationUser User { get; set; }
   
        public string Address { get; set; }
        [Required(ErrorMessage ="Please enter your ID number")]
        [DisplayName("ID Number")]
        [RegularExpression("(([0-9]{2})(0|1)([0-9])([0-3])([0-9]))([ ]?)(([ 0-9]{4})([ ]?)([ 0-1][8]([ ]?)[ 0-9]))", ErrorMessage ="Invalid ID number")]
        public string IdNumber { get; set; }
		[Required(ErrorMessage = "Please select an option")]
		[DisplayName("Drivers License")]
        public DriversLicense DriversLicense { get; set; }
        [DisplayName("Career Objective")]
        public string? CareerObjective { get; set; }
        [Required(ErrorMessage = "Please select an option")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please select an option")]
        public Race Race { get; set; }
        [DisplayName("South African?")]
        public bool IsSouthAfrican { get; set; }
        [DisplayName("Year of study")]
        [Required(ErrorMessage = "Select your year of study")]
        public YearOfStudy YearOfStudy { get; set; }
        public int Faculty { get; set; }
        public string Department { get; set; }
        public string? Skills { get; set; }
        public string? Achievements { get; set; }
        public string? Interests { get; set; }

	}
}
