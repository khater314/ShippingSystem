using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
    }
}
