using BillingAssistant.Business.Abstract;
using BillingAssistant.EmailService;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using BillingAssistant.Entities.DTOs.InvoiceDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private IInvoiceService _invoiceService;
        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateInvoice(InvoiceAddDto invoiceAddDto)
        {
            var result = await _invoiceService.AddAsync(invoiceAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetInvoices()
        {
            var result = await _invoiceService.GetListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]/{invoiceId:int}")]
        public async Task<IActionResult> GetInvoiceById(int invoiceId)
        {
            var result = await _invoiceService.GetByIdAsync(invoiceId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{invoiceId:int}")]
        public async Task<IActionResult> DeleteInvoice(int invoiceId)
        {
            var result = await _invoiceService.DeleteAsync(invoiceId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateInvoice(InvoiceUpdateDto invoiceUpdateDto)
        {
            var result = await _invoiceService.UpdateAsync(invoiceUpdateDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}