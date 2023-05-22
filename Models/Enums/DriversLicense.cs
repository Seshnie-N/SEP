using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
    public enum DriversLicense
    {
        [Display(Name ="Code A: Motorcycles")]
        CodeA = 1,
        [Display(Name ="Code B: Light Motor Vehicles")]
        CodeB = 2,
        [Display(Name ="Code C: Heavy Motor Vehicles")]
        CodeC = 3,
        [Display(Name ="Code D: Combination and Articulated Vehicles")]
        CodeD = 4
    }

    //public enum BusinessType
    //{

    //}

    //public enum WeekHour
    //{

    //}
}
