using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs
{
    public record UserResultDto
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; } = default!;
        public IEnumerable<string>? Errors { get; set; }
    }
}
