using BL.DTOs;
using Domains.Entities;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Contracts.Shipment
{
    public interface IShipmentService : IBaseService<TbShipment, TbShipmentDTO>
    {
        Task<bool> CreateAsync(TbShipmentDTO dto, CancellationToken ct = default);
        Task<PagedResult<TbShipmentDTO>> GetShipmentsByUserIdAsync(
            Guid userId = default,
            int pageNumber = 1, 
            int pageSize = 10, 
            CancellationToken ct = default);
    }
}
