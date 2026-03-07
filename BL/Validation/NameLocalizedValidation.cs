using AppResources.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.Validation
{
    public class NameLocalizedValidation(string lang = "en", int max = 100, int min = 3) : ValidationAttribute
    {
        private readonly string _lang = lang;
        private readonly int _max = max;
        private readonly int _min = min;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // 1. Required Validation
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(string.Format(ResShared.Val_Required, validationContext.DisplayName));
            }

            // 2. StringLength Validation
            var strValue = value.ToString() ?? string.Empty;
            if (strValue.Length < _min || strValue.Length > _max)
            {
                return new ValidationResult(string.Format(ResShared.Val_StringLength, validationContext.DisplayName, _min, _max));
            }

            // 3. LanguageValidation
            var langAttr = new LanguageValidation(_lang)
            {
                ErrorMessageResourceName = _lang switch
                {
                    "ar" => "Val_Language_Ar",
                    "en" => "Val_Language_En",
                    _ => "Val_Language_En"
                },
                ErrorMessageResourceType = typeof(ResShared)
            };

            return langAttr.GetValidationResult(value, validationContext) ?? ValidationResult.Success;
        }
    }
}
