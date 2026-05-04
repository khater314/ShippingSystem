using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppResources.Localization.Resources;
using BL.Validation;

namespace BL.DTOs
{
    public record UserRegisterDto : BaseEntityDTO
    {
        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_FirstName), ResourceType = typeof(ResShared))]
        public required string FirstName { get; set; }


        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_LastName), ResourceType = typeof(ResShared))]
        public required string LastName { get; set; }


        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [PhoneNumber(ErrorMessageResourceName = nameof(ResShared.Val_Phone), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_Phone), ResourceType = typeof(ResShared))]
        public required string PhoneNumber { get; set; }


        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 200, MinimumLength = 6, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_Email), ResourceType = typeof(ResShared))]
        public required string Email {get; set; }


        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 100, MinimumLength = 6, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_Password), ResourceType = typeof(ResShared))]
        public required string Password { get; set; }


        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_ConfirmPassword), ResourceType = typeof(ResShared))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ResShared.Val_Compare), ErrorMessageResourceType = typeof(ResShared))]
        public required string ConfirmedPassword { get; set; }


        public string? ReturnUrl { get; set; }

    }
}
