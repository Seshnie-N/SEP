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
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string FilePath { get; set; }

        [ForeignKey(nameof(JobApplicationId))]
        public int JobApplicationId { get; set; }
        public JobApplication JobApplication { get; set; }
    }
}
