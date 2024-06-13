using BillingAssistant.Business.Abstract;
using BillingAssistant.Entities.DTOs.OrderDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderAddDto orderAddDto)
        {
            var result = await _orderService.AddAsync(orderAddDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _orderService.GetListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]/{orderId:int}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var result = await _orderService.GetByIdAsync(orderId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder(OrderUpdateDto orderUpdateDto)
        {
            var result = await _orderService.UpdateAsync(orderUpdateDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{orderId:int}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _orderService.DeleteAsync(orderId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest();
        }
    }
}