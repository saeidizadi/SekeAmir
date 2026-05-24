using Application.Contracts.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SekeAmir.Web.Base;
using System.Threading.Tasks;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _category.GetAll());
        }

        public async Task<IActionResult> Create()
        {
            if (await _category.UpgradeCategory())
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Error] = ErrorMessage;
                return RedirectToAction("Index");
            }
        }

    }
}
