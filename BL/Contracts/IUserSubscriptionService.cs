using System;
using System.Collections.Generic;
using System.Text;
using Domains;
using BL.DTOs;

namespace BL.Contracts
{
    public interface IUserSubscriptionService 
        : IBaseService<TbUserSubscription, TbUserSubscriptionDTO>
    {
        Task<IEnumerable<TbUserSubscriptionDTO>> GetAllViewDataAsync(CancellationToken ct = default);
    }
}
