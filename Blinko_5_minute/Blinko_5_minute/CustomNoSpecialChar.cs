using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Blinko_5_minute
{
    public class CustomNoSpecialChar: ValidationAttribute
    {
        public override bool RequiresValidationContext => true;

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            string val = value as string;

            if(!string.IsNullOrEmpty(val) || Regex.IsMatch(val, @"[^a-zA-Z0-9 ]"))
                {
                return new ValidationResult(FormatErrorMessage(context.DisplayName));
            }

            return ValidationResult.Success;
        }


    }
}
