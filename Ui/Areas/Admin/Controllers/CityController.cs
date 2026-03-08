using BL.Contracts;
using BL.DTOs;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class CityController(ICityService cityService) : BaseController<TbCity, TbCityDTO>(cityService)
    {
        private readonly ICityService _cityService = cityService;
    }
}
