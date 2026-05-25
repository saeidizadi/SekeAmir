namespace SekeAmir.Web.Models
{
    /// <summary>
    /// مدل نرخ ارز
    /// </summary>
    public class CurrencyRate
    {
        public string Name { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string FlagCode { get; set; } = string.Empty;
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal ChangePercent { get; set; }
        public bool IsUp { get; set; }
    }

    /// <summary>
    /// مدل شمش طلا
    /// </summary>
    public class GoldBarRate
    {
        public string Weight { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal ChangePercent { get; set; }
        public bool IsUp { get; set; }
    }

    /// <summary>
    /// مدل سکه طلا
    /// </summary>
    public class CoinRate
    {
        public string Name { get; set; } = string.Empty;
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal ChangePercent { get; set; }
        public bool IsUp { get; set; }
    }

    /// <summary>
    /// مدل طلای آبشده
    /// </summary>
    public class MeltedGoldRate
    {
        public string Carat { get; set; } = string.Empty;
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal ChangePercent { get; set; }
        public bool IsUp { get; set; }
    }

    /// <summary>
    /// مدل سکه پارسیان
    /// </summary>
    public class ParsianCoinRate
    {
        public string Weight { get; set; } = string.Empty;
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal ChangePercent { get; set; }
        public bool IsUp { get; set; }
    }

    /// <summary>
    /// مدل ویوی صفحه اصلی
    /// </summary>
    public class HomeViewModel
    {
        public string CompanyName { get; set; } = "سکه امیر";
        public string CurrentTheme { get; set; } = "gold";
        public int YearsOfExperience { get; set; } = 20;
        public int HappyCustomers { get; set; } = 50000;
        public long SuccessfulTransactions { get; set; } = 1000000;

        // Hero Section Prices
        public string HeroDollarPrice { get; set; }
        public string HeroCoinPrice { get; set; } 
        public string HeroGoldPrice { get; set; }

        // Rate Lists
        public List<CurrencyRate> CurrencyRates { get; set; } = new();
        public List<GoldBarRate> GoldBarRates { get; set; } = new();
        public List<CoinRate> CoinRates { get; set; } = new();
        public List<MeltedGoldRate> MeltedGoldRates { get; set; } = new();
        public List<ParsianCoinRate> ParsianCoinRates { get; set; } = new();

        // About Section
        public List<FeatureItem> Features { get; set; } = new();

        // Contact Info
        public ContactInfo Contact { get; set; } = new();
    }

    /// <summary>
    /// مدل ویژگی‌ها
    /// </summary>
    public class FeatureItem
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// مدل اطلاعات تماس
    /// </summary>
    public class ContactInfo
    {
        public string Address { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string WorkingHours { get; set; } = string.Empty;
    }

    /// <summary>
    /// مدل داده‌های نمودار
    /// </summary>
    public class ChartDataPoint
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}
