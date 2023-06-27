using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models.DomainModels
{
    public class PartTimeHours
    {
        [Key]
        public int TimeId { get; set; }
        [DisplayName("Range")] 
        
        public string TimeRange { get; set; }
    }
}
