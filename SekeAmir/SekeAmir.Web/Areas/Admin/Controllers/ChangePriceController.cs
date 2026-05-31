using Application.Contracts.Shop;
using Domain.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SekeAmir.Web.Base;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize]
    public class ChangePriceController(IChangePrice price, IProduct product) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var obj = await price.GetAllChangePrice();
            return View(obj);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Product = new SelectList(await product.GetAll(), "id", "title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChangePrice changePrice)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Product = new SelectList(await product.GetAll(), "id", "title", changePrice.ProductId);
                return View(changePrice);
            }
            
            var result = await price.InsertChange(changePrice);
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
