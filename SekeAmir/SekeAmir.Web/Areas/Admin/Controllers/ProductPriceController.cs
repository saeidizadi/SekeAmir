using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Application.Features.Category.Request.Queries;
using Domain.Shop;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using SekeAmir.Web.Base;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize]
    public class ProductPriceController : BaseController
    {
        private readonly IProductPrice _productPrice;
        private readonly IProduct _product;
        private readonly IMediator _mediator;

        public ProductPriceController(IProductPrice productPrice, IProduct product,IMediator mediator)
        {
            _productPrice = productPrice;
            _product = product;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int? productId, int pageId=1)
        {
            Expression<Func<ProductPrice, bool>> Filter = x => !productId.HasValue || x.ProductId == productId;
            var model = await _productPrice.GetProductPricesPagingAsync(pageId, 50, productId);
            ViewBag.Product = new SelectList(await _product.GetAll(), "id", "title");
            return View(model);
        }
        public async Task<IActionResult> GetData()
        {
            if (await _productPrice.GetData())
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
            if (!ModelState.IsValid)
            {
                ViewBag.Product = new SelectList(await _product.GetAll(), "id", "title", productPrice.ProductId);
                return View(productPrice);
            }
            productPrice.Change = 0;
            productPrice.inputType = Domain.InputType.local;
            productPrice.CreateAt = DateTime.Now;
            var result = await _productPrice.InsertPrice(productPrice);
            if (result)
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            TempData[Error] = ErrorMessage;
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetPricesByProduct(int? ProductId,int pageId)
        {
            Expression<Func<ProductPrice, bool>> Filter = x => !ProductId.HasValue || x.ProductId == ProductId;
            var model = await _productPrice.GetProductPricesPagingAsync(pageId, 50, ProductId);
            return PartialView("_GetProductPriceByProductId", model);
        }
        public async Task<IActionResult> ShowDemo()
        {
            var obj =await _mediator.Send(new GetCategoryWithProductsRequest { });
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
