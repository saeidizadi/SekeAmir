using Application.Contracts.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SekeAmir.Web.Base;
using System.Threading.Tasks;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [AllowAnonymous]
    public class CategoryController : BaseController
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }

        public IActionResult Index()
        {
            return View();
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
