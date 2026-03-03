using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShippingTypesController(IShippingTypeService shippingTypeService) : Controller
    {
        readonly IShippingTypeService _shippingTypeService = shippingTypeService;

        // GET: ShippingTypesController
        public async Task<ActionResult> Index()
        {
            var data = await _shippingTypeService.GetAllAsync();
            return View(data);
        }



        public async Task<ActionResult> Edit(Guid? id, CancellationToken ct)
        {
            var data = new TbShippingTypeDTO();
            if (id is not null)
               data = await _shippingTypeService.GetByIdAsync((Guid)id, ct);
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TbShippingTypeDTO dTO, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(dTO);
            if (dTO.Id == Guid.Empty)
                await _shippingTypeService.AddAsync(dTO, dTO.Id, ct);
            else
                await _shippingTypeService.UpdateAsync(dTO, dTO.Id, ct);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _shippingTypeService.ChangeStatusAsync(id, Guid.NewGuid(), 0, ct);
            return RedirectToAction(nameof(Index));
        }
    }
}
