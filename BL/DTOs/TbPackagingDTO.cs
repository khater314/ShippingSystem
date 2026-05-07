using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BL.Validation;
using AppResources.Localization.Resources;

namespace BL.DTOs
{
    public record TbPackagingDTO : BaseEntityDTO
    {
        [NameLocalizedValidation("en")]
        [Display(Name = nameof(ResShared.Field_PackagingEname), ResourceType = typeof(ResShared))]
        public required string PackagingEname { get; set; }


        [NameLocalizedValidation("ar")]
        [Display(Name = nameof(ResShared.Field_PackagingAname), ResourceType = typeof(ResShared))]
        public required string PackagingAname { get; set; }


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
    }
}
