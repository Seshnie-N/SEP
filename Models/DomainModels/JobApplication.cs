using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
		[ValidateNever]
		public Student Student { get; set; }
        [ForeignKey("PostId")]
        public Guid PostId { get; set; }
		[ValidateNever]
		public Post Post { get; set; }
        [DisplayName("Application Status")]
        public string Status { get; set; }
        
    }
}
