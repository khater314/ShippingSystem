using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial class TbSettingDTO : BaseEntityDTO
{
    public double? KiloMeterRate { get; set; }

    public double? KilooGramRate { get; set; }
}
