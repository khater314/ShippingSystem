using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BL.Validation
{
    public partial class LanguageValidation(string language) : ValidationAttribute
    {
        [GeneratedRegex(@"^[\u0600-\u06FF\s\-']+$")]
        private static partial Regex ArabicRegex();

        [GeneratedRegex(@"^[a-zA-Z\s\-']+$")]
        private static partial Regex EnglishRegex();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string strValue || string.IsNullOrWhiteSpace(strValue))
                return ValidationResult.Success; // Allow empty or non-string values to be handled by other validators like [Required]

            bool isValid = language.ToLower() switch
            {
                "ar" => ArabicRegex().IsMatch(strValue),
                "en" => EnglishRegex().IsMatch(strValue),
                _ => false
            };

            if (!isValid)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"{validationContext.DisplayName} contains invalid characters for {language} language.");
            }

            return ValidationResult.Success;
        }
    }
}