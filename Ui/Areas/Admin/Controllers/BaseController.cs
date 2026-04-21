using AppResources.Localization.Resources;
using BL.Contracts;
using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.Mvc;
using Ui.Filters;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(TransactionExceptionFilter))]
    public class BaseController<T, TDto>(IBaseService<T, TDto> baseService) : Controller where TDto : BaseEntityDTO
    {
        readonly IBaseService<T, TDto> _baseService = baseService;

        // GET:
        public virtual async Task<ActionResult> Index(CancellationToken ct)
        {
            var data = await _baseService.GetAllAsync(ct);
            return View(data);
        }



        public virtual async Task<ActionResult> Edit(Guid? id, CancellationToken ct)
        {
            if (id == null)
                return View(Activator.CreateInstance<TDto>());

            var data = await _baseService.GetByIdAsync((Guid)id, ct);
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TDto dTO, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(dTO);

            bool isNew = dTO.Id == Guid.Empty;

            if (isNew)
                await _baseService.AddAsync(dTO, ct);
            else
                await _baseService.UpdateAsync(dTO, ct);

            TempData["MessageType"] = isNew ? Helpers.MessageType.SaveSuccess.ToString() : Helpers.MessageType.UpdateSuccess.ToString();
            TempData["MessageBody"] = isNew ? ResShared.Msg_Saved : ResShared.Msg_Updated;

            return RedirectToAction(nameof(Index));
        }

        public virtual async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            TDto dto = await _baseService.GetByIdAsync(id, ct);
            await _baseService.ChangeStatusAsync(dto, 0, ct);

            TempData["MessageType"] = Helpers.MessageType.DeleteSuccess.ToString();
            TempData["MessageBody"] = ResShared.Msg_Deleted;

            return RedirectToAction(nameof(Index));
        }
    }
}
