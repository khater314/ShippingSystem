using BL.Contracts;
using BL.DTOs;
using BL.Services;
using Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Ui.Areas.Admin.Controllers
{
    // Note: We Use VwCity To Get Cities with Country Names, But We Use TbCity For Edit And Add To Avoid Complexity In Mapping
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
