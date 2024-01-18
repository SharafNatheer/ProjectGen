using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectIDGenerator.Models
{
    public class ChangeRequests
    {
        [Key]
        public int cId { get; set; }
        public virtual string? ProjectID { get; set; }
        [ForeignKey("Id")]
        public Project? Project { get; set; }

        [Required]
        public int ChangeRequestId { get; set; }
    }
}
