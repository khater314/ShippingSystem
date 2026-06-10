using System;
using System.Collections.Generic;
using System.Text;
using BL.DTOs;
using Domains.Entities;

namespace BL.Contracts
{
    public interface IUserSubscriptionService 
        : IBaseService<TbUserSubscription, TbUserSubscriptionDTO>
    {
        Task<IEnumerable<TbUserSubscriptionDTO>> GetAllViewDataAsync(CancellationToken ct = default);
    }
}
