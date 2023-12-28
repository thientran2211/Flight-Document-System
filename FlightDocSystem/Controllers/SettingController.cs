using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService) 
        {
            _settingService = settingService;
        }

        [HttpPost]
        [Route("AddNewSetting")]
        public async Task<IActionResult> AddNewSetting(SettingDto model)
        {
            var setting = await _settingService.AddSettingAsync(model);
            return Ok(setting);
        }

        [HttpPost]
        [Route("UploadLogo")]
        public async Task<IActionResult> UploadLogo(int id, IFormFile file)
        {
            var logo = await _settingService.UploadLogoAsync(id, file);
            return Ok(logo);
        }

        [HttpPut]
        [Route("UpdateLogo")]
        public async Task<IActionResult> UpdateLogo(int id, IFormFile file)
        {
            var logoUpdate = await _settingService.UploadLogoAsync(id,file);
            return Ok(logoUpdate);
        }
    }
}
