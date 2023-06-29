using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
    public enum BusinessType
    {
        [Display(Name = "Private Company (Pty.) Ltd")]
        PrivateCompany = 1,
        [Display(Name = "Public Company (Ltd)")]
        PublicCompany = 2,
        [Display(Name = "Personal Liability Companies (Inc)")]
        PersonalLiability = 3,
        [Display(Name = "Non-profit Company (NPC)")]
        NonProfit = 4,
        [Display(Name = "State Owned Company (SOC Ltd)")]
        StateOwnedCompany = 5,
        [Display(Name = "Foreiegn/External Company")]
        ForeignOrExternal = 6
    }
}
