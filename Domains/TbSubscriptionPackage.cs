using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbSubscriptionPackage : BaseEntity
{
    public string PackageName { get; set; } = null!;

    public int ShipmentCount { get; set; }

    public double NumberOfKiloMeters { get; set; }

    public double TotalWeight { get; set; }

    public virtual ICollection<TbUserSubscription> TbUserSubscriptions { get; set; } = new List<TbUserSubscription>();
}
