using System;
using System.Collections.Generic;
using System.Text;
using Domains;
using BL.DTOs;
using Domains.Entities;

namespace BL.Contracts
{
    public interface ILogService : IBaseService<TbLog, TbLogDTO>
    {
    }
}
