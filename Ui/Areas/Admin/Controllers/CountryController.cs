using BL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Domains;
using BL.DTOs;

namespace Ui.Areas.Admin.Controllers
{
    public class CountryController(ICountryService countryService) : BaseController<TbCountry, TbCountryDTO>(countryService)
    {
        private readonly ICountryService _countryService = countryService;
    }
}
