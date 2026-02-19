using System;
using System.Collections.Generic;

namespace BL.DTOs;

public partial class TbUserSenderDTO : BaseEntityDTO
{
    public Guid UserId { get; set; }

    public string SenderName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public Guid CityId { get; set; }

    public string Address { get; set; } = null!;

    public virtual TbCityDTO City { get; set; } = null!;

}
