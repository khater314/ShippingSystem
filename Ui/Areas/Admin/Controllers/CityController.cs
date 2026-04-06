using BL.Contracts;
using BL.DTOs;
using BL.Services;
using Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Ui.Areas.Admin.Controllers
{
    public class CityController(ICityService cityService) : BaseController<TbCity, TbCityDTO>(cityService)
    {
        private readonly ICityService _cityService = cityService;
        public override async Task<ActionResult> Index(CancellationToken ct)
        {
            var data = await _cityService.GetAllCountryCitiesAsync(ct);
            return View(data);
        }
        public override async Task<ActionResult> Edit(Guid? id, CancellationToken ct)
        {
            ViewBag.Countries = await _cityService.GetAllCountryCitiesAsync(ct);
            return await base.Edit(id, ct);
        }
    }
}
