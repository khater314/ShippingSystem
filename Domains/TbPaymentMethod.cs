using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbPaymentMethod : BaseEntity
{
    public string? MethodAname { get; set; }

    public string? MethodEname { get; set; }

    public double? Commission { get; set; }

    public virtual ICollection<TbShipment> TbShipments { get; set; } = new List<TbShipment>();
}
