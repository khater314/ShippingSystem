using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public class VwUserSubscription : BaseView
    {
        public Guid UserId { get; set; }

        public Guid PackageId { get; set; }

        public DateTime SubscriptionDate { get; set; }

        // Read-Only
        public string? PackageName { get; set; }
        public string? Email { get; set; }
    }
}
