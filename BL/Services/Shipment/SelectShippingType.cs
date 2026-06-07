using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Shipment
{
    public class SelectShippingType(
        ICityService cityService,
        IShippingTypeService shippingTypeService
        )
        : ISelectShippingType
    {
        private readonly ICityService _cityService = cityService;
        private readonly IShippingTypeService _shippingTypeService = shippingTypeService;

        public async Task<Guid> GetShippingTypeId(Guid senderCityId, Guid receiverCityId) 
        {

            var senderCity = await _cityService.GetByIdAsync(senderCityId);
            var receiverCity = await _cityService.GetByIdAsync(receiverCityId);
            var shippingTypes = (await _shippingTypeService.GetAllAsync()).ToList();

            if (senderCity.CountryId == receiverCity.CountryId) 
            //shipping within the same country
            {  
                return shippingTypes[0].Id; 
            }
            else 
            // shipping between different countries
            {
                return shippingTypes[0].Id; 
            }
        }
    }
}
