using System;
using System.Collections.Generic;
using System.Text;
using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using BL.Mapping;
using DAL.Contracts;
using Domains;

namespace BL.Services.Shipment
{
    public class ShipmentService(
        ITableRepository<TbShipment> repo, 
        IMapper mapper, 
        IUserService userService, 
        IUserContactService contactService,
        IShipmentRateCalculator rateCalculator,
        ITrackingNumberCreator trackingNumberCreator,
        ISelectShippingType selectShippingType,
        IUnitOfWork unitOfWork
        ) 
        : BaseService<TbShipment, TbShipmentDTO>(unitOfWork, mapper, userService), IShipmentService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITableRepository<TbShipment> _repo = repo;
        private readonly IUserService _userService = userService;
        private readonly IUserContactService _contactService = contactService;
        private readonly IShipmentRateCalculator _rateCalculator = rateCalculator;
        private readonly ITrackingNumberCreator _trackingNumberCreator = trackingNumberCreator;
        private readonly ISelectShippingType _selectShippingType = selectShippingType;
        
        public async Task<bool> CreateAsync(TbShipmentDTO dto, CancellationToken ct = default)
        {
            await _unitOfWork.BeginTransactionAsync(ct);
            // Traking number and shipping rate.
            dto.TrackingNumber = _trackingNumberCreator.Create();
            dto.ShippingRate = _rateCalculator.Calculate();
            dto.UserId = await _userService.GetLoggedInUserId();
            dto.ShippingTypeId = await _selectShippingType.GetShippingTypeId(dto.Sender.CityId, dto.Receiver.CityId);


            // Save sender and receiver contact info if not exist.
            if (dto.SenderId == Guid.Empty)
                dto.SenderId = await _contactService.AddAndGetIdAsync(dto.Sender, ct);

            if (dto.ReceiverId == Guid.Empty)
                dto.ReceiverId = await _contactService.AddAndGetIdAsync(dto.Receiver, ct);



            // Save shipment.
            await this.AddAsync(dto, ct);

            return await _unitOfWork.CommitAsync(ct);

        }
    }
}
