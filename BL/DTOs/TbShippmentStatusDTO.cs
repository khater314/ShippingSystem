using System;
using System.Collections.Generic;

namespace BL.DTOs;
/// <summary>
/// Should Edit TbShippmentStatusDTO
/// </summary>
public partial class TbShippmentStatusDTO : BaseEntityDTO
{
    public Guid? ShippmentId { get; set; }

    public string? Notes { get; set; }

    public Guid CarrierId { get; set; }

    public virtual TbCarrierDTO Carrier { get; set; } = null!;

    public virtual TbShippmentDTO? Shippment { get; set; }
}
