using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs
{
    public record TbRefreshTokenDto : BaseEntityDTO
    {
        public required string Token { get; init; }
        public required Guid UserId { get; init; }
        public required DateTime Expires { get; init; }
    }
}
