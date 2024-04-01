
using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.Models
{
    public class PromoCode
    {
        public int id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreationDate { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

  
        public string ? MobileNO { get; set; }
        [Required]

        public string MyPromoCode { get; set; }
    }
}
