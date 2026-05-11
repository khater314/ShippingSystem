using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Contracts.Shipment
{
    public interface IShipmentService : IBaseService<TbShipment, TbShipmentDTO>
    {
        Task<bool> CreateAsync(TbShipmentDTO dto, CancellationToken ct = default);
    }
}
