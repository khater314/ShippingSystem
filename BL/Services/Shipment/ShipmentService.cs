using System;
using System.Collections.Generic;
using System.Text;
using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using BL.Mapping;
using DAL.Contracts;
using Domains.Entities;
using Domains.Models;

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

        private readonly IMapper _mapper = mapper;
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

        public async Task<PagedResult<TbShipmentDTO>> GetShipmentsByUserIdAsync(
            Guid userId = default,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken ct = default
        )   
        {
            if (userId == default)
                userId = await _userService.GetLoggedInUserId();

            PaginationParameters parameters = new() { PageNumber = pageNumber, PageSize = pageSize };

            var shipments = await _repo.GetPagedListAsync<TbShipmentDTO>(
                parameters: parameters,
                filter: s => s.UserId == userId,
                ct: ct,
                selector: s => new TbShipmentDTO
                {
                    Id = s.Id,
                    TrackingNumber = s.TrackingNumber,
                    ShippingRate = s.ShippingRate,
                    ShippingTypeId = s.ShippingTypeId,
                    SenderId = s.SenderId,
                    ReceiverId = s.ReceiverId,
                    UserId = s.UserId,
                    Weight = s.Weight,
                    Width = s.Width,
                    Length = s.Length,
                    Height = s.Height,
                    DelivryDate = s.DelivryDate, 
                    ShippingDate = s.ShippingDate,
                    PackageValue = s.PackageValue,
                    UserSubscriptionId = s.UserSubscriptionId,
                    PackagingId = s.PackagingId,
                    PaymentMethodId = s.PaymentMethodId,
                    ReferenceId = s.ReferenceId,

                    Sender = new TbUserContactDTO
                    {
                        Id = s.Sender.Id,
                        UserId = s.Sender.UserId,
                        FullName = s.Sender.FullName,
                        Email = s.Sender.Email,
                        Phone = s.Sender.Phone,
                        CityId = s.Sender.CityId,
                        Address = s.Sender.Address,               
                        PostalCode = s.Sender.PostalCode,         
                        ContactType = s.Sender.ContactType,       
                        IsDefaultAddress = s.Sender.IsDefaultAddress, 
                        OtherAddressInfo = s.Sender.OtherAddressInfo,
                        Contacts = s.Sender.Contacts,
                        CityEname = s.Sender.City.CityEname,
                        CityAname = s.Sender.City.CityAname,
                        CountryAname = s.Sender.City.Country.CountryAname,
                        CountryEname = s.Sender.City.Country.CountryEname

                    },
                    Receiver = new TbUserContactDTO
                    {
                        Id = s.Receiver.Id,
                        UserId = s.Receiver.UserId,
                        FullName = s.Receiver.FullName,
                        Email = s.Receiver.Email,
                        Phone = s.Receiver.Phone,
                        CityId = s.Receiver.CityId,
                        Address = s.Receiver.Address,             
                        PostalCode = s.Receiver.PostalCode,       
                        ContactType = s.Receiver.ContactType,     
                        IsDefaultAddress = s.Receiver.IsDefaultAddress, 
                        OtherAddressInfo = s.Receiver.OtherAddressInfo,
                        Contacts = s.Receiver.Contacts,
                        CityEname = s.Receiver.City.CityEname,
                        CityAname = s.Receiver.City.CityAname,
                        CountryAname = s.Receiver.City.Country.CountryAname,
                        CountryEname = s.Receiver.City.Country.CountryEname
                    }
                }
            );

            return shipments;
        }
        
        public override async Task<TbShipmentDTO> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var shipment = await _repo.GetListAsync<TbShipment>(
                filter: s => s.Id == id,
                ct: ct,
                includers: [ 
                    x => x.Sender, 
                    x => x.Receiver, 
                    x => x.Sender.City, 
                    x => x.Receiver.City,
                    x => x.Sender.City.Country,
                    x => x.Receiver.City.Country
                    ]
                );

            return _mapper.Map<TbShipment, TbShipmentDTO>(shipment.First());
        }  
        
    }
}
