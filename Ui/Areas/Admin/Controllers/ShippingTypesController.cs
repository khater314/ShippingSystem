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


        // POST: ShippingTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(TbShippingTypeDTO dTO)
        {
            if (!ModelState.IsValid)
                return View("Edit", dTO);
            if (dTO.Id == Guid.Empty)
                await _shippingTypeService.AddAsync(dTO, dTO.Id);
            else
                await _shippingTypeService.UpdateAsync(dTO, dTO.Id);

            return RedirectToAction(nameof(Index));
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _shippingTypeService.ChangeStatusAsync(id, Guid.NewGuid(), 0, ct);
            return RedirectToAction(nameof(Index));
        }
    }
}
