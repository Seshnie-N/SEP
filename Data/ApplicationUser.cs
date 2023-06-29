using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SEP.Models.DomainModels;

namespace SEP.Data;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Required(ErrorMessage ="Please enter your first name.")]
    public string FirstName { get; set; }
    [PersonalData]
    [Required(ErrorMessage = "Please enter your last name.")]
    public string LastName { get; set; }
	[ValidateNever]
	public virtual Student Student { get; set; }
	[ValidateNever]
	public virtual Employer Employer { get; set; }
}

