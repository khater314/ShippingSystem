using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppResources.Localization.Resources;
using BL.Validation;

namespace BL.DTOs;
/// <summary>
/// Using Built In DataAnnotations for validation and localization, and custom validation attribute for language specific validation
/// </summary>
public partial class TbShippingTypeDTO : BaseEntityDTO
{
    [Required(ErrorMessageResourceName = "Val_Required", ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [StringLength(100, MinimumLength = 3 , ErrorMessageResourceName = "Val_StringLength", ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = "Field_ShippingTypeAname", ResourceType = typeof(ResShared))]
    [LanguageValidation("ar", ErrorMessageResourceName = "Val_Language_Ar", ErrorMessageResourceType = typeof(ResShared))]
    public string? ShippingTypeAname { get; set; }



    [Required(ErrorMessageResourceName = "Val_Required", ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "Val_StringLength", ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = "Field_ShippingTypeEname", ResourceType = typeof(ResShared))]
    [LanguageValidation("en", ErrorMessageResourceName = "Val_Language_En", ErrorMessageResourceType = typeof(ResShared))]
    public string? ShippingTypeEname { get; set; }



    [Required(ErrorMessageResourceName = "Val_Required", ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [Range(0, 100, ErrorMessageResourceName = "Val_Range", ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = "Field_ShippingFactor", ResourceType = typeof(ResShared))]
    public double ShippingFactor { get; set; }

}
