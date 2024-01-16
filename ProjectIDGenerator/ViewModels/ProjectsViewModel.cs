using ProjectIDGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.ViewModels
{
    public class ProjectsViewModel
    {
        public List<Project> Projects { get; set; }

        public string ProjectId { get; set; }

        public int ChangeRequestID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
