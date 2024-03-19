using ProjectIDGenerator.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjectIDGenerator.CustomValidators
{
    public class UniqueMobileNumber :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
       ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (value != null)
            {

                if (!context.PromoCodes.Any(a => a.MobileNO == value.ToString()))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(" رقم الهاتف موجود مسبقاً");
            }
            else return new ValidationResult("يجب ادخال رقم الهاتف");
        }
    }
}
