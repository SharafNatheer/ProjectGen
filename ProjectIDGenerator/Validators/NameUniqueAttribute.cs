using ProjectIDGenerator.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProjectIDGenerator.Validators
{
    public class NameUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {

            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (!context.Projects.Any(a => a.Name == value.ToString()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Name exists");
        }
    }
}
