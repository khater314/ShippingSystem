using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial class TbPaymentMethodDTO : BaseEntityDTO
{
    public string? MethdAname { get; set; }

    public string? MethodEname { get; set; }

    public double? Commission { get; set; }

}
