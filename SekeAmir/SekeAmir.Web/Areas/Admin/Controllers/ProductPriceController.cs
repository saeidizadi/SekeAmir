using System.Threading.Tasks;
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
    public class ProductPriceController : BaseController
    {
        private readonly IProductPrice _productPrice;
        private readonly IProduct _product;

        public ProductPriceController(IProductPrice productPrice, IProduct product)
        {
            _productPrice = productPrice;
            _product = product;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Product = new SelectList(await _product.GetAll(), "id", "title");
            return View(await _productPrice.GetAllPrice());
        }
        public async Task<IActionResult> GetData()
        {
          if( await _productPrice.GetData())
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            TempData[Error] = ErrorMessage;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            ViewBag.Product = new SelectList(await _product.GetAll(), "id", "title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ProductPrice productPrice)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Product = new SelectList(await _product.GetAll(), "id", "title",productPrice.ProductId);
                return View(productPrice);
            }
            productPrice.Change = 0;
            productPrice.inputType = Domain.InputType.local;
            productPrice.CreateAt = DateTime.Now;
            var result=await _productPrice.InsertPrice(productPrice);
            if(result)
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            TempData[Error] = ErrorMessage;
            return RedirectToAction("Index");
        }
        public IActionResult GetPricesByProduct(int ProductId)
        {
          var model=  _productPrice.GetPriceByProdictId(ProductId);
            return PartialView("_GetProductPriceByProductId", model);
        }
    }
}
