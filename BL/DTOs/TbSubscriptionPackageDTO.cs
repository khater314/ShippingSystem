using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial record TbSubscriptionPackageDTO : BaseEntityDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ResShared), ErrorMessageResourceName = nameof(ResShared.Field_PackageName) )]
    [Display(Name = nameof(ResShared.Field_PackageName), ResourceType = typeof(ResShared))]
    public string PackageName { get; set; } = null!;

    [RequiredRange(max: 100000, min: 1)]
    [Display(Name = nameof(ResShared.Field_ShipmentCount), ResourceType = typeof(ResShared))]
    public int ShipmentCount { get; set; }

    [RequiredRange(max: 100000, min: 1)]
    [Display(Name = nameof(ResShared.Field_NumberOfKiloMeters), ResourceType = typeof(ResShared))]
    public double NumberOfKiloMeters { get; set; }

    [RequiredRange(max: 100000, min: 1)]
    [Display(Name = nameof(ResShared.Field_TotalWeight), ResourceType = typeof(ResShared))]
    public double TotalWeight { get; set; }

}
