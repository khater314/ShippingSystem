using AppResources.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.Validation
{
    public class RequiredRange(double max, double min) : ValidationAttribute
    {
        private readonly double _min = min;
        private readonly double _max = max;
        override protected ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(string.Format(ResShared.Val_Required, validationContext.DisplayName));
            }
            if (double.TryParse(value.ToString(), out double numericValue))
            {
                if (numericValue < _min || numericValue > _max)
                {
                    return new ValidationResult(string.Format(ResShared.Val_Range, validationContext.DisplayName, _min, _max));
                }
            }
            else
            {
                return new ValidationResult(ResShared.Val_InvalidNumber);
            }
            return ValidationResult.Success;
        }
    }
}
