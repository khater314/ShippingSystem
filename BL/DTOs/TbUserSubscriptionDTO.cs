using AppResources.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial record TbUserSubscriptionDTO : BaseEntityDTO
{
    [Required(ErrorMessageResourceType = typeof(ResShared), ErrorMessageResourceName = nameof(ResShared.Val_Required))]
    [Display(Name = nameof(ResShared.Field_UserId), ResourceType = typeof(ResShared))]
    public Guid UserId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ResShared), ErrorMessageResourceName = nameof(ResShared.Val_Required))]
    [Display(Name = nameof(ResShared.Field_PackageId), ResourceType = typeof(ResShared))]
    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;

    // Read-Only 
    public string? Email { get; set; }
    public string? PackageName { get; set; }
}
