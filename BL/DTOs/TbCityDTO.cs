using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial class TbCityDTO : BaseEntityDTO
{
    [NameLocalizedValidation("ar")]
    [Display(Name = "Field_CityAname", ResourceType = typeof(ResShared))]
    public string? CityAname { get; set; }

    [NameLocalizedValidation("en")]
    [Display(Name = "Field_CityEname", ResourceType = typeof(ResShared))]
    public string? CityEname { get; set; }

}
