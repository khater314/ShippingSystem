using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Microsoft.AspNetCore.Http;

namespace BL.Services
{
    public class CarrierService(
        ITableRepository<TbCarrier> repo, 
        IMapper mapper, 
        IUserService userService
        )
        : 
        BaseService<TbCarrier, TbCarrierDTO>(repo, mapper, userService), ICarrierService
    {
    }
}