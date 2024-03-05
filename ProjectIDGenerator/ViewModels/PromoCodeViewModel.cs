using ProjectIDGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.ViewModels
{
    public class PromoCodeViewModel
    {
        public List<PromoCode> PCs { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreationDate { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]

        public string SecondName { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]

        public int MobileNO { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public string MyPromoCode { get; set; }

        public string test { get; set; }
    }
}
