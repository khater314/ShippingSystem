using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs
{
    public record UserResultDto
    {
        public bool IsSuccess { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
