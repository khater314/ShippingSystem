using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using DAL.Exceptions;
using Ui.Helpers;
using Microsoft.Extensions.DependencyInjection;
using AppResources.Localization.Resources;

namespace Ui.Filters
{
    public class TransactionExceptionFilter(ILogger<TransactionExceptionFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<TransactionExceptionFilter> _logger = logger;
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DataAccessException dae)
            {
                var factory = context.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>();
                var tempData = factory.GetTempData(context.HttpContext);

                string? actionName = context.RouteData.Values["action"]?.ToString();
                string? controllerName = context.RouteData.Values["controller"]?.ToString();

                MessageType enumKey = default;
                string resourceMsg = "";
                
                if (actionName == "Edit" && context.HttpContext.Request.HasFormContentType)
                {
                    var idValue = context.HttpContext.Request.Form["Id"].ToString();
                    bool isUpdate = !string.IsNullOrEmpty(idValue) && idValue != Guid.Empty.ToString();
                    
                    enumKey = isUpdate ? MessageType.UpdateFailed : MessageType.SaveFailed;
                    resourceMsg = isUpdate ? ResShared.Msg_Error_Update : ResShared.Msg_Error_Save;
                }
                else if (actionName == "Delete")
                {
                    enumKey = MessageType.DeleteFailed;
                    resourceMsg = ResShared.Msg_Error_Delete;
                }


                tempData["MessageType"] = enumKey;
                tempData["MessageBody"] = !string.IsNullOrEmpty(dae.Message) ? dae.Message : resourceMsg;

                _logger.LogError(dae, "Error occurred in {Action}", context.RouteData.Values["action"]);

                context.Result = new RedirectToActionResult("Index", controllerName, null);
                context.ExceptionHandled = true;
            }
        }
    }
}