using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbUserSubscription : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;

    public virtual TbSubscriptionPackage Package { get; set; } = null!;
}
