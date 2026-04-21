using Domains.Enums;
using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbUserContact : BaseEntity
{
    public Guid UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public Guid CityId { get; set; }

    public string Address { get; set; } = null!;

    public ContactType ContactType { get; set; }

    public virtual TbCity City { get; set; } = null!;

    public virtual ICollection<TbShipment> TbShipments { get; set; } = new List<TbShipment>();
}
