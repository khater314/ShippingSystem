using BL.DTOs;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Microsoft.AspNetCore.Http;
using Domains.Entities;

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