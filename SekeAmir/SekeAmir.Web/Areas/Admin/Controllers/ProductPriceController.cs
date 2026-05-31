using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Application.Features.Category.Request.Queries;
using Domain.Shop;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SekeAmir.Web.Base;
using System.Linq.Expressions;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize]
    public class ProductPriceController(IProductPrice price, IProduct product, IMediator mediator)
        : BaseController
    {
        public async Task<IActionResult> Index(int? productId, int pageId=1)
        {
            Expression<Func<ProductPrice, bool>> filter = x => !productId.HasValue || x.ProductId == productId;
            var model = await price.GetProductPricesPagingAsync(pageId, 50, productId);
            ViewBag.Product = new SelectList(await product.GetAll(), "id", "title");
            return View(model);
        }
        public async Task<IActionResult> GetData()
        {
            if (await price.GetData())
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
            ViewBag.Product = new SelectList(await product.GetAll(), "id", "title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ProductPrice productPrice)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Product = new SelectList(await product.GetAll(), "id", "title", productPrice.ProductId);
                return View(productPrice);
            }
            productPrice.Change = 0;
            productPrice.inputType = Domain.InputType.local;
            productPrice.CreateAt = DateTime.Now;
            var result = await price.InsertPrice(productPrice);
            if (result)
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            TempData[Error] = ErrorMessage;
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetPricesByProduct(int? productId,int pageId)
        {
            var model = await price.GetProductPricesPagingAsync(pageId, 50, productId);
            return PartialView("_GetProductPriceByProductId", model);
        }
        public async Task<IActionResult> ShowDemo()
        {
            var obj =await mediator.Send(new GetCategoryWithProductsRequest { });
            if(obj.ErrorId>0)
            {
               var data = obj.Result;
                return View(data);

            }
            TempData[Error] = obj.ErrorTitle;
            return View(new List<ShowAllPricesVM>());
        }
    }
}
