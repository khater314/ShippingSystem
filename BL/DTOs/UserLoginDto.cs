using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppResources.Localization.Resources;
using BL.Validation;

namespace BL.DTOs
{
    public record UserLoginDto : BaseEntityDTO
    {
        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 200, MinimumLength = 6, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_Email), ResourceType = typeof(ResShared))]
        public required string Email {get; set; }

        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 100, MinimumLength = 6, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_Password), ResourceType = typeof(ResShared))]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }



    }
}
