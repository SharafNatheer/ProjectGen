using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectIDGenerator.Models
{
    public class ChangeRequests
    {
        [Key]
        public int cId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual string? ProjectID { get; set; }
        public Project? Project { get; set; }

        [Required]
        public string ChangeRequestId { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreationDate { get; set; }
    }
}
