
using ProjectIDGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.ViewModels
{
    public class PromoCodeViewModel
    {
        public List<PromoCode> PCs { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreationDate { get; set; }
       
        [Required(ErrorMessage = "Please enter your name.")]
        public string FirstName { get; set; }
       

        [Required(ErrorMessage = "Please enter your second name.")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
       
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Please enter mobile number")]
       
        public string MobileNO { get; set; }
        
        public string MyPromoCode { get; set; }

    }
}
