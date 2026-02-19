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
            CreateMap<TbCarrier, TbCarrierDTO>().ReverseMap();
            CreateMap<TbPaymentMethod, TbPaymentMethodDTO>().ReverseMap();
            CreateMap<TbSubscriptionPackage, TbSubscriptionPackageDTO>().ReverseMap();
            CreateMap<TbShippmentStatus, TbShippmentStatusDTO>().ReverseMap();
            CreateMap<TbShippment, TbShippmentDTO>().ReverseMap();
            CreateMap<TbSetting, TbSettingDTO>().ReverseMap();
            CreateMap<TbLog, TbLogDTO>().ReverseMap();
            CreateMap<TbShippingType, TbShippingTypeDTO>().ReverseMap();
            CreateMap<TbUserReceiver, TbUserReceiverDTO>().ReverseMap();
            CreateMap<TbUserSender, TbUserSenderDTO>().ReverseMap();
            CreateMap<TbUserSubscription, TbUserSubscriptionDTO>().ReverseMap();
        }

    }
}
