using Domain.Main;
using System.Threading.Tasks;

namespace Application.Contracts.main
{
    public interface ISetting
    {

        public Task<Setting> GetSettingAsync();

        public Task<bool> UpdateSettingAsync(Setting setting);
    }
}