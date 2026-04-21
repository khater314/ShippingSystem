using AppResources.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace BL.Validation
{
    public partial class PhoneNumberAttribute : ValidationAttribute
    {
        [GeneratedRegex(@"^(\+)?([0-9]{8,15})$")]
        private static partial Regex PhoneNumber();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var phoneNumber = value.ToString()!;


            var regex = PhoneNumber();

            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult(ResShared.Val_Phone);
            }

            return ValidationResult.Success;
        }


    }
}
