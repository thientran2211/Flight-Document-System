using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface ISettingService
    {
        public Task<Setting> AddSettingAsync(SettingDto settingDto);
        public Task<Setting> UploadLogoAsync(int id, IFormFile file);
    }
}
