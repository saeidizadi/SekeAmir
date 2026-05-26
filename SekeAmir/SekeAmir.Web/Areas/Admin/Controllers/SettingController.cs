using Application.Contracts.main;
using Application.DTOs.SettingDto;
using AutoMapper;
using Domain.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Extention;
using SekeAmir.Web.Base;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class SettingController : BaseController
    {
        private readonly ISetting _setting;
        private readonly IOptions<Setting> _Headersetting;

        public SettingController(IOptions<Setting> headersetting, ISetting setting)
        {
            _Headersetting = headersetting;
            _setting = setting;
        }

        public async Task<IActionResult> Index()
        {
            var obj = await _setting.GetSettingAsync();
            return View(obj);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }
            
            var result = await _setting.UpdateSettingAsync(setting);
            if (result)
                TempData[Success] = SuccessMessage;
            else
                TempData[Error] = ErrorMessage;
            return RedirectToAction("Index");
        }
    }
}
