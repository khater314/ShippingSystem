using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial class TbUserSubscriptionDTO : BaseEntityDTO
{
    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; }

    public virtual TbSubscriptionPackageDTO Package { get; set; } = null!;
}
