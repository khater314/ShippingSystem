using AppResources.Localization.Resources;
using BL.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial record TbCityDTO : BaseEntityDTO
{
    [NameLocalizedValidation("ar")]
    [Display(Name = nameof(ResShared.Field_CityAname), ResourceType = typeof(ResShared))]
    public string? CityAname { get; set; }

    [NameLocalizedValidation("en")]
    [Display(Name = nameof(ResShared.Field_CityEname), ResourceType = typeof(ResShared))]
    public string? CityEname { get; set; }


    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_CountryEname), ResourceType = typeof(ResShared))]
    public Guid? CountryId { get; set; }


    // Read-only properties for country names
    public string? CountryAname { get; set; }
    public string? CountryEname { get; set; }

}
