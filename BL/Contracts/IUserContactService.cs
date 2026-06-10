using System;
using System.Collections.Generic;
using System.Text;
using BL.DTOs;
using Domains.Entities;

namespace BL.Contracts
{
    public interface IUserContactService : IBaseService<TbUserContact, TbUserContactDTO>
    {
        Task<IEnumerable<TbUserContactDTO>> GetAllViewDataAsync(CancellationToken ct = default);
    }
}
