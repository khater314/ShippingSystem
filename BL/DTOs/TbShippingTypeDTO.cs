using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial class TbShippingTypeDTO : BaseEntityDTO
{
    public string? ShippingTypeAname { get; set; }

    public string? ShippingTypeEname { get; set; }

    public double ShippingFactor { get; set; }

}
