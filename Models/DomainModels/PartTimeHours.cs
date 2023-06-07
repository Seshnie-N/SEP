using System.ComponentModel.DataAnnotations;

namespace SEP.Models.DomainModels
{
    public class PartTimeHours
    {
        [Key]
        public int timeId { get; set; } 

        public string timeRange { get; set; }
    }
}
