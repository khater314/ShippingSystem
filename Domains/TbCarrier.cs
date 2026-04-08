using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCarrier : BaseEntity
{
    public string CarrierName { get; set; } = null!;

    public virtual ICollection<TbShipmentStatus> TbShipmentStatuses { get; set; } = new List<TbShipmentStatus>();
}
