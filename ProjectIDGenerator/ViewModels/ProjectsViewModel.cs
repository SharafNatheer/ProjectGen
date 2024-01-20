using ProjectIDGenerator.Models;
using ProjectIDGenerator.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.ViewModels
{
    public class ProjectsViewModel
    {
        public string ProjectId { get; set; }

        public int ChangeRequestID { get; set; }

        [Required(ErrorMessage = "This field can not be empty.")]
        [NameUnique]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<ChangeRequests>? ChangeRequests { get; set; }

        public ChangesViewModel? changesViewModel { get; set; }

        public string ChangeDescription { get; set; }
    }
}
