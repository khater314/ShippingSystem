using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTOs
{
    public record UserReadDto : BaseEntityDTO
    {
        [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
        [StringLength(maximumLength: 200, MinimumLength = 6, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
        [Display(Name = nameof(ResShared.Field_Email), ResourceType = typeof(ResShared))]
        public required string Email { get; set; }
    }
}
