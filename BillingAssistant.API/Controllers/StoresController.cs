using BillingAssistant.Business.Abstract;
using BillingAssistant.Entities.DTOs.StoreDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private IStoreService _storeService;
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetStores()
        {
            var result = await _storeService.GetListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]/{storeId:int}")]
        public async Task<IActionResult> GetStoreById(int storeId)
        {
            var result = await _storeService.GetByIdAsync(storeId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateStore(StoreAddDto storeAddDto)
        {
            var result = await _storeService.AddAsync(storeAddDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{storeId:int}")]
        public async Task<IActionResult> DeleteStore(int storeId)
        {
            var result = await _storeService.DeleteAsync(storeId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateStore(StoreUpdateDto storeUpdateDto)
        {
            var result = await _storeService.UpdateAsync(storeUpdateDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}