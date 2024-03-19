using ProjectIDGenerator.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.Models
{
    public class PromoCode
    {
        public int id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreationDate { get; set; }
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        [UniqueMobileNumber]
        public string ? MobileNO { get; set; }
        [Required]

        public string MyPromoCode { get; set; }
    }
}
