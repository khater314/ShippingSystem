using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial class TbPaymentMethodDTO : BaseEntityDTO
{

    [NameLocalizedValidation("ar")]
    [Display(Name = "Field_MethodAname", ResourceType = typeof(ResShared))]
    public string? MethdAname { get; set; }


    [NameLocalizedValidation("en")]
    [Display(Name = "Field_MethodEname", ResourceType = typeof(ResShared))]
    public string? MethodEname { get; set; }

    [RequiredRange(0, 50)]
    [Display(Name = "Field_Commission", ResourceType = typeof(ResShared))]
    public double? Commission { get; set; }

}
