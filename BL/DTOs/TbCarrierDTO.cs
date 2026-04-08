using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial record TbCarrierDTO : BaseEntityDTO
{
    [NameLocalizedValidation("en")]
    [Display(Name = nameof(ResShared.Field_CarrierName), ResourceType = typeof(ResShared))]
    public string CarrierName { get; set; } = null!;
}
