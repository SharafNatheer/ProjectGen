using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectIDGenerator.Models
{
    public class Project
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? CreationDate { get; set; }

		[Required]
		public int ChangeRequestId { get; set; }
	}
}
