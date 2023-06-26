using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    public class JobApplication
    {
        [Key]
        public Guid ApplicationId { get; set; }
        [ForeignKey("StudentId")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("PostId")]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        [DisplayName("Application Status")]
        public string Status { get; set; }
        
    }
}
