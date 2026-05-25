using Application.Contracts.Shop;
using Domain.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SekeAmir.Web.Base;
using System.Threading.Tasks;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize]
    public class ChangePriceController : BaseController
    {
        private readonly IChangePrice _changePrice;
        private readonly IProduct _product;

        public ChangePriceController(IChangePrice changePrice, IProduct product)
        {
            _changePrice = changePrice;
            _product = product;
        }

        public async Task<IActionResult> Index()
        {
            var obj = await _changePrice.GetAllChangePrice();
            return View(obj);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Product = new SelectList(await _product.GetAll(), "id", "title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChangePrice changePrice)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Product = new SelectList(await _product.GetAll(), "id", "title", changePrice.ProductId);
                return View(changePrice);
            }
            
            var result = await _changePrice.InsertChange(changePrice);
            if (result)
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            TempData[Error] = ErrorMessage;
            return RedirectToAction("Index");


        }
    }
}
