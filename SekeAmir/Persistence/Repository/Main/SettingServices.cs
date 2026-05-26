using Application.Contracts.main;
using Application.Contracts.Repository;
using Domain.Main;
using Microsoft.Extensions.Caching.Memory;

namespace Persistence.Repository.Main
{
    public class SettingServices : ISetting
    {
        private readonly IMemoryCache _cache;

        private readonly IMaster<Setting> _master;

        public SettingServices(IMaster<Setting> master, IMemoryCache cache)
        {
            _master = master;
            _cache = cache;
        }



        public async Task<Setting> GetSettingAsync()
        {
            return await _cache.GetOrCreateAsync<Setting>("SiteSetting", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                var Mysetting = await _master.GetAllEfAsync(null);
                var obj = Mysetting.FirstOrDefault();
                if (obj == null)
                {
                    var defaultSetting = new Setting()
                    {
                        CompanyName = "سکه صراف امیر",
                        CurrentTheme = "gold",
                        YearsOfExperience = 20,
                        HappyCustomers = 5000,
                        SuccessfulTransactions = 1000000,
                        Address = "تهران، خیابان فردوسی، پلاک ۱۲۳",
                        Phone1 = "۰۲۱-۱۲۳۴۵۶۷۸",
                        Phone2 = "۰۹۱۲-۱۲۳۴۵۶۷",
                        WorkingHours = "شنبه تا پنجشنبه: ۹ صبح تا ۶ عصر"


                    };
                    await _master.InsertAsync(defaultSetting);
                    return defaultSetting;
                }

                return obj;
            });
        }



        public async Task<bool> UpdateSettingAsync(Setting setting)
        {
            var res = await _master.UpdateAsync(setting);
            if (res == null)
                return false;
            _cache.Remove("SiteSetting");
            return true;
        }
    }
}
