using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BL.DTOs;
using Domains;
using Domains.Entities;

namespace BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<TbCity, TbCityDTO>().ReverseMap();
            CreateMap<TbCountry, TbCountryDTO>().ReverseMap();
            CreateMap<TbCarrier, TbCarrierDTO>().ReverseMap();
            CreateMap<TbPaymentMethod, TbPaymentMethodDTO>().ReverseMap();
            CreateMap<TbPackaging, TbPackagingDTO>().ReverseMap();
            CreateMap<TbSubscriptionPackage, TbSubscriptionPackageDTO>().ReverseMap();
            CreateMap<TbShipmentStatus, TbShipmentStatusDTO>().ReverseMap();
            CreateMap<TbShipment, TbShipmentDTO>().ReverseMap();
            CreateMap<TbRateSetting, TbRateSettingDTO>().ReverseMap();
            CreateMap<TbLog, TbLogDTO>().ReverseMap();
            CreateMap<TbShippingType, TbShippingTypeDTO>().ReverseMap();
            CreateMap<TbUserContact, TbUserContactDTO>().ReverseMap();
            CreateMap<TbUserSubscription, TbUserSubscriptionDTO>().ReverseMap();

            CreateMap<VwCity, TbCityDTO>().ReverseMap();
            CreateMap<VwUserContact, TbUserContactDTO>().ReverseMap();
            CreateMap<VwUserSubscription, TbUserSubscriptionDTO>().ReverseMap();

            CreateMap<TbRefreshToken, TbRefreshTokenDto>().ReverseMap();
        }

    }
}
