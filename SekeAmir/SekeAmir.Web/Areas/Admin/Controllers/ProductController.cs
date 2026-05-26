using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Domain.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Extention;
using SekeAmir.Web.Base;
using System.Threading.Tasks;

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
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var obj = await _product.GetProductById(Id);
            if (obj == null)
                return NotFound();

            return View(new EditProductVm()
            {
                Id = Id,
                InputType=obj.InputType,
                IsExchange=obj.IsExchange
                
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductVm model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var old = await _product.GetProductById(model.Id);
            if (model.Image != null)
            {
                var result = FileTools.UploadFile(model.Image, FileTools.GetFileName(model.Image), "Product");
                if (result.Success)
                {
                    model.ImageAddress = result.FilePath;
                    if (!string.IsNullOrEmpty(old.iconImage))
                        FileTools.DeleteFile(old.iconImage);
                }
                else
                {
                    TempData[Error] = "آپلود تصویر با خطا مواجه شد";
                    return View(model);
                }
                old.iconImage = model.ImageAddress;
            }
        
            old.IsExchange = model.IsExchange;
            old.InputType = model.InputType;
            var res = await _product.updateProduct(old);
            if (res)
            {
                TempData[Success] = SuccessMessage;
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Error] = ErrorMessage;
                return View(model);
            }

        }
    }
}
