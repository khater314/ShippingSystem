using Domains.Enums;
using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial record TbShipmentStatusDTO : BaseEntityDTO
{
    public Guid? ShipmentId { get; set; }

    public string? Notes { get; set; }

    public Guid CarrierId { get; set; }

    public ShipmentStatus Status { get; set; }
}
