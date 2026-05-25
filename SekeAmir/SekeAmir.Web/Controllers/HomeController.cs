using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SekeAmir.Web.Models;
using System.Diagnostics;
using Application.Features.Category.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Domain.Dto.Shop;
using PersianAssistant.Extensions;

namespace SekeAmir.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController(IMediator mediator) : Controller
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

                // نرخ‌های Hero (فعلاً استاتیک - میتونی بعداً از دیتای جدید هم بدست بیاری)
                HeroDollarPrice = ((double)products.FirstOrDefault(x => x.ProductId == 2).FinalSellPrice).Toman(),
                HeroCoinPrice = ((double)products.FirstOrDefault(x => x.ProductId == 29).FinalSellPrice).Toman(),
                HeroGoldPrice = ((double)products.FirstOrDefault(x => x.ProductId == 37).FinalSellPrice).Toman(),

                // ویژگی‌ها
                Features = GetFeatures(),

                // اطلاعات تماس
                Contact = GetContactInfo()
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
            else
            {
                // اگه دیتایی نبود، از دیتای استاتیک استفاده کن
                SetDefaultRates(viewModel);
            }

            return View(viewModel);
        }
        private void SetDefaultRates(HomeViewModel viewModel)
        {
            viewModel.CurrencyRates = GetCurrencyRates();
            viewModel.GoldBarRates = GetGoldBarRates();
            viewModel.CoinRates = GetCoinRates();
            viewModel.MeltedGoldRates = GetMeltedGoldRates();
            viewModel.ParsianCoinRates = GetParsianCoinRates();
        }

        // متدهای کمکی
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
        private List<CurrencyRate> GetCurrencyRates()
        {
            return new List<CurrencyRate>
            {
                new() { Name = "دلار آمریکا", NameEn = "USD", FlagCode = "us", BuyPrice = 58300, SellPrice = 58500, ChangePercent = 1.2m, IsUp = true },
                new() { Name = "یورو", NameEn = "EUR", FlagCode = "eu", BuyPrice = 63100, SellPrice = 63300, ChangePercent = 0.8m, IsUp = true },
                new() { Name = "پوند انگلیس", NameEn = "GBP", FlagCode = "gb", BuyPrice = 73800, SellPrice = 74000, ChangePercent = 0.3m, IsUp = false },
                new() { Name = "درهم امارات", NameEn = "AED", FlagCode = "ae", BuyPrice = 15900, SellPrice = 16000, ChangePercent = 0.5m, IsUp = true }
            };
        }

        private List<GoldBarRate> GetGoldBarRates()
        {
            return new List<GoldBarRate>
            {
                new() { Weight = "۱ گرم", Price = 2950000, ChangePercent = 1.5m, IsUp = true },
                new() { Weight = "۵ گرم", Price = 14700000, ChangePercent = 1.4m, IsUp = true },
                new() { Weight = "۱۰ گرم", Price = 29350000, ChangePercent = 1.3m, IsUp = true },
                new() { Weight = "۵۰ گرم", Price = 146500000, ChangePercent = 0.2m, IsUp = false }
            };
        }

        private List<CoinRate> GetCoinRates()
        {
            return new List<CoinRate>
            {
                new() { Name = "سکه امامی", BuyPrice = 31800000, SellPrice = 32000000, ChangePercent = 2.1m, IsUp = true },
                new() { Name = "سکه بهار آزادی", BuyPrice = 29500000, SellPrice = 29700000, ChangePercent = 1.8m, IsUp = true },
                new() { Name = "نیم سکه", BuyPrice = 17200000, SellPrice = 17400000, ChangePercent = 0.5m, IsUp = false },
                new() { Name = "ربع سکه", BuyPrice = 10800000, SellPrice = 11000000, ChangePercent = 0.9m, IsUp = true }
            };
        }

        private List<MeltedGoldRate> GetMeltedGoldRates()
        {
            return new List<MeltedGoldRate>
            {
                new() { Carat = "طلای ۲۴ عیار", BuyPrice = 3150000, SellPrice = 3200000, ChangePercent = 1.1m, IsUp = true },
                new() { Carat = "طلای ۱۸ عیار", BuyPrice = 2360000, SellPrice = 2400000, ChangePercent = 0.8m, IsUp = true },
                new() { Carat = "طلای ۷۵۰", BuyPrice = 2360000, SellPrice = 2400000, ChangePercent = 0.3m, IsUp = false }
            };
        }

        private List<ParsianCoinRate> GetParsianCoinRates()
        {
            return new List<ParsianCoinRate>
            {
                new() { Weight = "۰.۰۵۰ گرم", BuyPrice = 190000, SellPrice = 200000, ChangePercent = 1.0m, IsUp = true },
                new() { Weight = "۰.۱۰۰ گرم", BuyPrice = 380000, SellPrice = 400000, ChangePercent = 0.7m, IsUp = true },
                new() { Weight = "۰.۱۵۰ گرم", BuyPrice = 570000, SellPrice = 600000, ChangePercent = 0.4m, IsUp = false },
                new() { Weight = "۰.۲۰۰ گرم", BuyPrice = 760000, SellPrice = 800000, ChangePercent = 1.2m, IsUp = true },
                new() { Weight = "۰.۳۰۰ گرم", BuyPrice = 1140000, SellPrice = 1200000, ChangePercent = 0.9m, IsUp = true }
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

        private ContactInfo GetContactInfo()
        {
            return new ContactInfo
            {
                Address = "تهران، خیابان فردوسی، پلاک ۱۲۳",
                Phone1 = "۰۲۱-۱۲۳۴۵۶۷۸",
                Phone2 = "۰۹۱۲-۱۲۳۴۵۶۷",
                WorkingHours = "شنبه تا پنجشنبه: ۹ صبح تا ۶ عصر"
            };
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
    }
}
