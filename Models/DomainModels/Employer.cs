using SEP.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    [Table("Employer")]
    public class Employer 
    {
        [Key,ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string JobTitle { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string TradingName { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }

        //public BusinessType BusinessType { get; set; }
        public bool isApproved { get; set; }
        public string? ApproverNote { get; set; }

    }
}
