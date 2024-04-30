using BillingAssistant.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAssistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinariesController : ControllerBase
    {
        ICloudinaryService _cloudinaryService;
        IOcrService _ocrService;
        public CloudinariesController(ICloudinaryService cloudinaryService,IOcrService ocrService)
        {
            _cloudinaryService = cloudinaryService;
            _ocrService = ocrService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddImage(IFormFile file)
        {
            var result = await _cloudinaryService.UploadOrderImageAsync(file);
            //await _ocrService.ReadOcr(file);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}