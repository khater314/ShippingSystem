using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbRateSetting : BaseEntity
{
    public double? KiloMeterRate { get; set; }

    public double? KiloGramRate { get; set; }
}
