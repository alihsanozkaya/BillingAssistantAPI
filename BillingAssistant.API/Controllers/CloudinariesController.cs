using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAssistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinariesController : ControllerBase
    {
        ICloudinaryService _cloudinaryService;
        public CloudinariesController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddImage(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest(Messages.FileIsEmpty);
            }
            var result = await _cloudinaryService.UploadOrderImageAsync(file);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}