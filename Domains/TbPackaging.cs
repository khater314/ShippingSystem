using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public class TbPackaging : BaseEntity
    {
        public required string PackagingEname { get; set; }

        public required string PackagingAname { get; set; }

        public double Length { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Weight { get; set; }

        public virtual ICollection<TbShipment> TbShipments { get; set; } = [];
    }
}
