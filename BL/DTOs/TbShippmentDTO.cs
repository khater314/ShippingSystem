using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;
/// <summary>
/// TrackingNumber should be string, but it's double in the database, so we keep it as double here to avoid mapping issues, but we should consider changing it to string in the future.
/// </summary>
public partial class TbShippmentDTO : BaseEntityDTO
{
    public DateTime ShippingDate { get; set; }

    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_SenderId), ResourceType = typeof(ResShared))]
    public Guid SenderId { get; set; }

    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_ReceiverId), ResourceType = typeof(ResShared))]
    public Guid ReceiverId { get; set; }

    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_ShippingTypeId), ResourceType = typeof(ResShared))]
    public Guid ShippingTypeId { get; set; }



    [RequiredRange(min: 0.01, max: 1000)]
    [Display(Name = nameof(ResShared.Field_Width), ResourceType = typeof(ResShared))]
    public double Width { get; set; }

    [RequiredRange(min: 0.01, max: 1000)]
    [Display(Name = nameof(ResShared.Field_Height), ResourceType = typeof(ResShared))]
    public double Height { get; set; }

    [RequiredRange(min: 0.01, max: 1000)]
    [Display(Name = nameof(ResShared.Field_Weight), ResourceType = typeof(ResShared))]
    public double Weight { get; set; }

    [RequiredRange(min: 0.01, max: 1000)]
    [Display(Name = nameof(ResShared.Field_Length), ResourceType = typeof(ResShared))]
    public double Length { get; set; }

        [RequiredRange(min: 0.01, max: 1000)]
        [Display(Name = nameof(ResShared.Field_PackageValue), ResourceType = typeof(ResShared))]
    public decimal PackageValue { get; set; }
    [RequiredRange(min: 0, max: 10)]
    [Display(Name = nameof(ResShared.Field_ShippingRate), ResourceType = typeof(ResShared))]
    public decimal ShippingRate { get; set; }

    public Guid? PaymentMethodId { get; set; }
    public Guid? UserSubscriptionId { get; set; }

    public double? TrackingNumber { get; set; } // Should be string
    public Guid? ReferenceId { get; set; }

}
