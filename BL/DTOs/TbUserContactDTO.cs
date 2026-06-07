using AppResources.Localization.Resources;
using BL.Validation;
using Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs;

public partial record TbUserContactDTO : BaseEntityDTO
{

    public Guid? UserId { get; set; }

    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [StringLength(200, MinimumLength = 3, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_FullName), ResourceType = typeof(ResShared))]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [StringLength(200, MinimumLength = 3, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
    [EmailAddress(ErrorMessageResourceName = nameof(ResShared.Val_Email), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_Email), ResourceType = typeof(ResShared))]
    public string Email { get; set; } = null!;

    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [StringLength(200, MinimumLength = 3, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
    [PhoneNumber]
    [Display(Name = nameof(ResShared.Field_Phone), ResourceType = typeof(ResShared))]
    public string Phone { get; set; } = null!;

    
    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [Display(Name = nameof(ResShared.Field_CityId), ResourceType = typeof(ResShared))]
    public Guid CityId { get; set; }


    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [StringLength(500, MinimumLength = 3, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_Address), ResourceType = typeof(ResShared))]
    public string Address { get; set; } = null!;


    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [Display(Name = nameof(ResShared.Field_ContactType), ResourceType = typeof(ResShared))]
    public ContactType ContactType { get; set; }



    [Display(Name = nameof(ResShared.Field_OtherAddressInfo), ResourceType = typeof(ResShared))]
    public string? OtherAddressInfo { get; set; }


    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [StringLength(20, MinimumLength = 3, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_PostalCode), ResourceType = typeof(ResShared))]
    public string PostalCode { get; set; } = null!;

    [StringLength(500, MinimumLength = 3, ErrorMessageResourceName = nameof(ResShared.Val_StringLength), ErrorMessageResourceType = typeof(ResShared))]
    [Display(Name = nameof(ResShared.Field_Contacts), ResourceType = typeof(ResShared))]
    public string? Contacts { get; set; } //contact info for the address, like phone, email, etc.

    [Required(ErrorMessageResourceName = nameof(ResShared.Val_Required), ErrorMessageResourceType = typeof(ResShared), AllowEmptyStrings = false)]
    [Display(Name = nameof(ResShared.Field_IsDefaultAddress), ResourceType = typeof(ResShared))]
    public bool IsDefaultAddress { get; set; }


    // Read-Only From 
    [Display(Name = nameof(ResShared.Field_CityAname), ResourceType = typeof(ResShared))]
    public string? CityAname { get; set; }

    [Display(Name = nameof(ResShared.Field_CityEname), ResourceType = typeof(ResShared))]
    public string? CityEname { get; set; }
}