using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BL.DTOs;
using Domains;

namespace BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<TbCountry, TbCountryDTO>().ReverseMap();
            CreateMap<TbCity, TbCityDTO>().ReverseMap();
            CreateMap<VwCity, TbCityDTO>().ReverseMap();
            CreateMap<TbCarrier, TbCarrierDTO>().ReverseMap();
            CreateMap<TbPaymentMethod, TbPaymentMethodDTO>().ReverseMap();
            CreateMap<TbSubscriptionPackage, TbSubscriptionPackageDTO>().ReverseMap();
            CreateMap<TbShipmentStatus, TbShipmentStatusDTO>().ReverseMap();
            CreateMap<TbShipment, TbShipmentDTO>().ReverseMap();
            CreateMap<TbRateSetting, TbRateSettingDTO>().ReverseMap();
            CreateMap<TbLog, TbLogDTO>().ReverseMap();
            CreateMap<TbShippingType, TbShippingTypeDTO>().ReverseMap();
            CreateMap<TbUserContact, TbUserContactDTO>().ReverseMap();
            CreateMap<TbUserSubscription, TbUserSubscriptionDTO>().ReverseMap();
        }

    }
}
