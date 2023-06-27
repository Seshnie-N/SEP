using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
    public enum Gender
    {
        [Display(Name ="Prefer not to specify")]
        Unspecified = 1,
        Male = 2,
        Female = 3
    }

}
