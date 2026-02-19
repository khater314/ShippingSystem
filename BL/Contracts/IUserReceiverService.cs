using System;
using System.Collections.Generic;
using System.Text;
using Domains;
using BL.DTOs;

namespace BL.Contracts
{
    public interface IUserReceiverService : IBaseService<TbUserReceiver, TbUserReceiverDTO>
    {
    }
}
