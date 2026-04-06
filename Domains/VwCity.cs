using System;
using System.Collections.Generic;

namespace Domains;

public partial class VwCity : BaseView
{
    public string? CityAname { get; set; }

    public string? CityEname { get; set; }

    public Guid CountryId { get; set; }

    public string? CountryAname { get; set; }

    public string? CountryEname { get; set; }
}
