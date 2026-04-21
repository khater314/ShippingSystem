using Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public class VwUserContact : BaseView
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public Guid CityId { get; set; }

        public string Address { get; set; } = null!;

        public ContactType ContactType { get; set; }

        // Read-Only From 
        public string? CityAname { get; set; }
        public string? CityEname { get; set; }
    }
}
