using System.ComponentModel.DataAnnotations;

namespace SEP.Models.Enums
{
    public enum BusinessType
    {
        [Display(Name = "Sole Proprietorship")]
        SoleProprietorship = 1,
        Partnership = 2,
        [Display(Name = "Proprietary limited company")]
        PtyLtd = 3,
        [Display(Name = "Public Company")]
        PublicCompany = 4,
        [Display(Name = "Private Company")]
        PrivateCompany = 5,
        [Display(Name = "State Owned Company")]
        StateOwnedCompany = 6,
        [Display(Name = "Non-profit Company")]
        NPC = 7
    }
}
