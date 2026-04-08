using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppResources.Localization.Resources;

namespace BL.DTOs;

public partial record TbCountryDTO : BaseEntityDTO
{
    [NameLocalizedValidation("ar")]
    [Display(Name = "Field_CountryAname", ResourceType = typeof(ResShared))]
    public string? CountryAname { get; set; }

    [NameLocalizedValidation("en")]
    [Display(Name = "Field_CountryEname", ResourceType = typeof(ResShared))]
    public string? CountryEname { get; set; }

}
