using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Extention;
using SekeAmir.Web.Base;

namespace SekeAmir.Web.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize]
    public class CategoryController(ICategory category) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await category.GetAll());
        }

        public async Task<IActionResult> Create()
        {
            if (await category.UpgradeCategory())
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
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var obj = await category.GetById(Id);
            if (obj == null)
                return NotFound();
       
            return View(new EditCategoryVm()
            {
                description = obj.description,
                iconImage = obj.iconImage,
                id = Id
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryVm model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var old = await category.GetById(model.id);
            if (model.Image != null)
            {
                var result = FileTools.UploadFile(model.Image, FileTools.GetFileName(model.Image), "Category");
                if (result.Success)
                {
                    model.iconImage = result.FilePath;
                    if (!string.IsNullOrEmpty(old?.iconImage))
                        FileTools.DeleteFile(old.iconImage);
                }
                else
                {
                    TempData[Error] = "آپلود تصویر با خطا مواجه شد";
                    return View(model);
                }
            }
            old.iconImage = model.iconImage;
            old.description = model.description;
            var res = await category.UpdateCastegory(old);
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
