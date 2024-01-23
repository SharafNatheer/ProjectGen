using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.ViewModels
{
    public class ChangesViewModel
    {
        public string? ProjectId { get; set; }
        [Required]
        public string ChangeDescription { get; set; }
        public DateTime? ChangeCreationDate { get; set; }

    }
}
