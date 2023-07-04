using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
    public enum JobType
    {
        [Display(Name ="Full-Time")]
        FullTime,
        [Display(Name = "Part-Time")]
        PartTime
    }
}
