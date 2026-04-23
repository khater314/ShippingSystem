using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public class TbRefreshToken : BaseEntity
    {
        public required string Token { get; set; }
        public required Guid UserId { get; set; }
        public required DateTime Expires { get; set; }

    }
}
