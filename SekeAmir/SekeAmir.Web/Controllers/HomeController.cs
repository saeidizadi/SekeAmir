using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SekeAmir.Web.Models;
using System.Diagnostics;
using Application.Contracts.main;
using Application.Features.Category.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using PersianAssistant.Extensions;
using Application.DTOs.Shop;

namespace SekeAmir.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController(IMediator mediator, ISetting settingRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var res = await mediator.Send(new GetCategoryWithProductsRequest { });
            if (res.ErrorId < 0)
            {
                return NotFound();
            }
            var products = res.Result as List<ShowAllPricesVM>;

            var viewModel = new HomeViewModel
            {
                CompanyName = "سکه صراف امیر",
                CurrentTheme = "gold",
                YearsOfExperience = 20,
                HappyCustomers = 5000,
                SuccessfulTransactions = 1000000,

                HeroDollarPrice = ((double)products.FirstOrDefault(x => x.ProductId == 2).FinalSellPrice).Toman(),
                HeroCoinPrice = ((double)products.FirstOrDefault(x => x.ProductId == 29).FinalSellPrice).Toman(),
                HeroGoldPrice = ((double)products.FirstOrDefault(x => x.ProductId == 37).FinalSellPrice).Toman(),
                // ویژگی‌ها
                Features = GetFeatures(),
                // اطلاعات تماس
                Contact = await GetContactInfo(),
                
            };


            if (products != null && products.Any())
            {
                // پر کردن نرخ ارزها (مثلاً CategoryId = 1)
                viewModel.CurrencyRates = products
                    .Where(p => p.CategoryId == 1)
                    .Select(p => new CurrencyRate
                    {
                        Name = p.ProductTitle,
                        NameEn = GetEnglishName(p.ProductTitle),
                        FlagCode = GetFlagCode(p.ProductTitle),
                        BuyPrice = p.FinalBuyPrice,
                        SellPrice = p.FinalSellPrice,
                        ChangePercent = p.buyChange,
                        IsUp = p.buyChange >= 0
                    }).ToList();

                // پر کردن شمش طلا (CategoryId = 2)
                viewModel.GoldBarRates = products
                    .Where(p => p.CategoryId == 3)
                    .Select(p => new GoldBarRate
                    {
                        Weight = p.ProductTitle,
                        Price = p.FinalSellPrice,
                        ChangePercent = p.SellChange,
                        IsUp = p.SellChange >= 0
                    }).ToList();

                // پر کردن سکه طلا (CategoryId = 3)
                viewModel.CoinRates = products
                    .Where(p => p.CategoryId == 4)
                    .Select(p => new CoinRate
                    {
                        Name = p.ProductTitle,
                        BuyPrice = p.FinalBuyPrice,
                        SellPrice = p.FinalSellPrice,
                        ChangePercent = p.buyChange,
                        IsUp = p.buyChange >= 0
                    }).ToList();

                // پر کردن طلای آبشده (CategoryId = 4)
                viewModel.MeltedGoldRates = products
                    .Where(p => p.CategoryId == 5)
                    .Select(p => new MeltedGoldRate
                    {
                        Carat = p.ProductTitle,
                        BuyPrice = p.FinalBuyPrice,
                        SellPrice = p.FinalSellPrice,
                        ChangePercent = p.buyChange,
                        IsUp = p.buyChange >= 0
                    }).ToList();

                // پر کردن سکه پارسیان (CategoryId = 6)
                viewModel.ParsianCoinRates = products
                    .Where(p => p.CategoryId == 5)
                    .Select(p => new ParsianCoinRate
                    {
                        Weight = p.ProductTitle,
                        BuyPrice = p.FinalBuyPrice,
                        SellPrice = p.FinalSellPrice,
                        ChangePercent = p.buyChange,
                        IsUp = p.buyChange >= 0
                    }).ToList();
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Utility
        private string GetEnglishName(string persianName)
        {
            return persianName switch
            {
                "دلار آمریکا" => "USD",
                "یورو" => "EUR",
                "پوند انگلیس" => "GBP",
                "درهم امارات" => "AED",
                _ => ""
            };
        }
        private string GetFlagCode(string persianName)
        {
            return persianName switch
            {
                "دلار آمریکا" => "us",
                "یورو" => "eu",
                "پوند انگلیس" => "gb",
                "درهم امارات" => "ae",
                _ => ""
            };
        }
        private List<FeatureItem> GetFeatures()
        {
            return new List<FeatureItem>
            {
                new() { Icon = "bi-shield-check", Title = "امنیت و اعتبار", Description = "دارای مجوز رسمی از بانک مرکزی جمهوری اسلامی ایران" },
                new() { Icon = "bi-lightning-charge", Title = "سرعت تراکنش", Description = "انجام معاملات در کمترین زمان ممکن و بدون تأخیر" },
                new() { Icon = "bi-percent", Title = "بهترین نرخ‌ها", Description = "ارائه رقابتی‌ترین نرخ‌های خرید و فروش در بازار" },
                new() { Icon = "bi-headset", Title = "پشتیبانی ۲۴/۷", Description = "تیم پشتیبانی حرفه‌ای در تمام ساعات شبانه‌روز" }
            };
        }
        private async Task<ContactInfo> GetContactInfo()
        {
            var contactInfo = await settingRepository.GetSettingAsync();
            return new ContactInfo
            {
                Address = contactInfo.Address,
                Phone1 = contactInfo.Phone1,
                Phone2 = contactInfo.Phone2,
                WorkingHours = contactInfo.WorkingHours
            };
        }
        #endregion
    }
}
