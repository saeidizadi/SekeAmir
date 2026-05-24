using System.Threading.Tasks;
using Application.Contracts.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SekeAmir.Web.Base;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _product.GetAll());
        }
        public async Task<IActionResult> Create()
        {
          if( await _product.UpgradeProduct())
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            TempData[Error] = ErrorMessage;
            return RedirectToAction("Index");
        }
    }
}
