using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum DriverLicense
    {
        [Display(Name ="Code A: Motorcycles")]
        CodeA,
        [Display(Name ="Code B: Light Motor Vehicles")]
        CodeB,
        [Display(Name ="Code C: Heavy Motor Vehicles")]
        CodeC,
        [Display(Name ="Code D: Combination and Articulated Vehicles")]
        CodeD
    }

    public enum Race
    {
        Black,
        White,
        Coloured,
        Indian,
        Asian
    }

    public enum JobType
    {
        [Display(Name ="Full-Time")]
        FullTime,
        [Display(Name = "Part-Time")]
        PartTime
    }

    //public enum BusinessType
    //{

    //}

    //public enum WeekHour
    //{

    //}
}
