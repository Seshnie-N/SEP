using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("StudentId")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Status { get; set; }
        
    }
}
