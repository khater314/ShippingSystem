using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial record TbRateSettingDTO : BaseEntityDTO
{
    public double? KiloMeterRate { get; set; }

    public double? KiloGramRate { get; set; }
}
