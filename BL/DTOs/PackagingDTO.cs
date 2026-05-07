using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs
{
    public record PackagingDTO : BaseEntityDTO
    {
        public required string PackagingEname { get; set; }

        public required string PackagingAname { get; set; }

        public double Length { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Weight { get; set; }
    }
}
