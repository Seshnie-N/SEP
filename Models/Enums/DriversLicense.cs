using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
    public enum DriversLicense
    {
        None = 1,
        [Display(Name = "Code A: Motorcycles")]
        CodeA = 2,
        [Display(Name = "Code B: Light Motor Vehicles")]
        CodeB = 3,
        [Display(Name = "Code C: Heavy Motor Vehicles")]
        CodeC = 4,
        [Display(Name = "Code D: Combination and Articulated Vehicles")]
        CodeD = 5
    }

    //public enum WeekHour
    //{

    //}
}
