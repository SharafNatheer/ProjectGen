using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.ViewModels
{
    public class ChangesViewModel
    {
        public string? ProjectId { get; set; }
        [Required]
        public string ChangeDescription { get; set; }
        public string Requestedby { get; set; }
        public string ProjectSponser { get; set; }

        public string RelatedProject { get; set; }

        public string StakeHolder { get; set; }

        public string RelatedSystem { get; set; }
        public DateTime? ChangeCreationDate { get; set; }

    }
}
