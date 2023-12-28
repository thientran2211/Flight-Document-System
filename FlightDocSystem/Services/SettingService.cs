using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Services
{
    public class SettingService : ISettingService
    {
        private readonly FlightDocsContext _context;

        public SettingService(FlightDocsContext context) 
        {
            _context = context;
        }

        public async Task<Setting> AddSettingAsync(SettingDto settingDto)
        {
            var setting = new Setting { UserID = settingDto.UserID };
            _context.Settings.Add(setting);
            await _context.SaveChangesAsync();

            return setting;
        }

        public async Task<Setting> UploadLogoAsync(int id, IFormFile file)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null)
            {
                throw new KeyNotFoundException(nameof(setting));
            }

            if (file != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", "webp" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new InvalidOperationException("Invalid file format. Only JPG or PNG or WEBP is allowed.");
                }

                var uploadLogo = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                var filePath = Path.Combine(uploadLogo, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                setting.Logo = filePath;
                setting.LogoName = Path.GetFileName(file.FileName);

                await _context.SaveChangesAsync();
            }

            return setting;
        }        
    }
}
