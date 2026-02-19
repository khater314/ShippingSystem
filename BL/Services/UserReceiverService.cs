using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserReceiverService : BaseService<TbUserReceiver, TbUserReceiverDTO>, IUserReceiverService
    {
        public UserReceiverService(ITableRepository<TbUserReceiver> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}