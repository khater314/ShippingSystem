using AppResources.Localization.Resources;
using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Ui.Filters;
using Ui.Helpers;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(TransactionExceptionFilter))]
    public class ShippingTypeController(IShippingTypeService shippingTypeService) : Controller
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
            if (!ModelState.IsValid) return View(dTO);

            bool isNew = dTO.Id == Guid.Empty;

            if (isNew)
                await _shippingTypeService.AddAsync(dTO, ct);
            else
                await _shippingTypeService.UpdateAsync(dTO, ct);

            TempData["MessageType"] = isNew ? Helpers.MessageType.SaveSuccess.ToString() : Helpers.MessageType.UpdateSuccess.ToString();
            TempData["MessageBody"] = isNew ? ResShared.Msg_Saved : ResShared.Msg_Updated;

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            TbShippingTypeDTO tbShippingTypeDTO = await _shippingTypeService.GetByIdAsync(id, ct);
            await _shippingTypeService.ChangeStatusAsync(tbShippingTypeDTO, 0, ct);

            TempData["MessageType"] = Helpers.MessageType.DeleteSuccess.ToString();
            TempData["MessageBody"] = ResShared.Msg_Deleted;

            return RedirectToAction(nameof(Index));
        }
    }
}
