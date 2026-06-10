using BL.Contracts;
using Microsoft.AspNetCore.Mvc;
using BL.DTOs;
using Domains.Entities;

namespace Ui.Areas.Admin.Controllers
{
    public class CountryController(ICountryService countryService) : BaseController<TbCountry, TbCountryDTO>(countryService)
    {
        private readonly ICountryService _countryService = countryService;
    }
}
