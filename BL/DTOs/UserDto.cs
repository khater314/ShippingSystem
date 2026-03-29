using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs
{
    public class UserDto : BaseEntityDTO
    {
        public required string Email {get; set; }
        public required string Password { get; set; }
        public required string ConfirmedPassword { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
