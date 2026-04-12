using BL.Contracts;
using BL.DTOs;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class RateSettingController(IRateSettingService rateSettingService) : BaseController<TbRateSetting, TbRateSettingDTO>(rateSettingService)
    {
        private readonly IRateSettingService _rateSettingService = rateSettingService;
    }
}
