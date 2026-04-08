using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial record TbPaymentMethodDTO : BaseEntityDTO
{

    [NameLocalizedValidation("ar")]
    [Display(Name = nameof(ResShared.Field_MethodAname) , ResourceType = typeof(ResShared))]
    public string? MethodAname { get; set; }


    [NameLocalizedValidation("en")]
    [Display(Name = nameof(ResShared.Field_MethodEname), ResourceType = typeof(ResShared))]
    public string? MethodEname { get; set; }

    [RequiredRange(min: 0, max: 50)]
    [Display(Name = nameof(ResShared.Field_Commission) , ResourceType = typeof(ResShared))]
    public double? Commission { get; set; }

}
