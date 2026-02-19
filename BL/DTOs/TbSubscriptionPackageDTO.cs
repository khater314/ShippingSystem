using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial class TbSubscriptionPackageDTO : BaseEntityDTO
{
    public string PackageName { get; set; } = null!;

    public int ShippimentCount { get; set; }

    public double NumberOfKiloMeters { get; set; }

    public double TotalWeight { get; set; }

}
