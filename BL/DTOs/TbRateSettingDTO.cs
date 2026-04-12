using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial record TbRateSettingDTO : BaseEntityDTO
{
    [RequiredRange(max: 100, min: 0.01)]
    [Display(Name = nameof(ResShared.Field_KiloMeterRate), ResourceType = typeof(ResShared))]
    public double? KiloMeterRate { get; set; }

    [RequiredRange(max: 100, min: 0.01)]
    [Display(Name = nameof(ResShared.Field_KiloGramRate), ResourceType = typeof(ResShared))]
    public double? KiloGramRate { get; set; }
}
