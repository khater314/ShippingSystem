using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCity : BaseEntity
{
    public string? CityAname { get; set; }

    public string? CityEname { get; set; }

    public Guid CountryId { get; set; }

    public virtual TbCountry Country { get; set; } = null!;

    public virtual ICollection<TbUserContact> TbUserContacts { get; set; } = new List<TbUserContact>();

}
