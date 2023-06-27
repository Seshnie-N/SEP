using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
    public class ApplicationDocument
    {
        [Key]
        public Guid DocumentId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [DisplayName("File Type")]
        public string FileType { get; set; }
        [DisplayName("File Extention")]
        public string Extension { get; set; }
        public string Description { get; set; }
        [DisplayName("Created On")]
        public DateTime? CreatedDate { get; set; }
        [DisplayName("File Path")]
        public string FilePath { get; set; }

        [ForeignKey(nameof(JobApplicationId))]
        public Guid JobApplicationId { get; set; }
        [ValidateNever]
        public JobApplication JobApplication { get; set; }
    }
}
