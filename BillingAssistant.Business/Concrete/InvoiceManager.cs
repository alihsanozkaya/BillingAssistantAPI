using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.InvoiceDtos;
using BillingAssistant.Entities.DTOs.ProductDtos;
using System.Linq.Expressions;

namespace BillingAssistant.Business.Concrete;
public class InvoiceManager : IInvoiceService
{
    IInvoiceRepository _invoiceRepository;
    IMapper _mapper;
    public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }
    public async Task<IDataResult<Invoice>> AddAsync(InvoiceAddDto entity)
    {
        var newInvoice = _mapper.Map<Invoice>(entity);
        await _invoiceRepository.AddAsync(newInvoice);
        return new SuccessDataResult<Invoice>(newInvoice, Messages.Added);
    }
    public async Task<IDataResult<bool>> DeleteAsync(int id)
    {
        var isDelete = await _invoiceRepository.DeleteAsync(id);
        return new SuccessDataResult<bool>(isDelete, Messages.Deleted);
    }
    public async Task<IDataResult<InvoicesDto>> GetAsync(Expression<Func<Invoice, bool>> filter)
    {
        var invoice = await _invoiceRepository.GetAsync(filter);
        if (invoice != null)
        {
            var invoiceDto = _mapper.Map<InvoicesDto>(invoice);
            return new SuccessDataResult<InvoicesDto>(invoiceDto, Messages.Listed);
        }
        return new ErrorDataResult<InvoicesDto>(null, Messages.NotListed);
    }
    public async Task<IDataResult<InvoicesDto>> GetByIdAsync(int id)
    {
        var invoice = await _invoiceRepository.GetAsync(x => x.Id == id);
        if (invoice != null)
        {
            var invoiceDto = _mapper.Map<InvoicesDto>(invoice);
            return new SuccessDataResult<InvoicesDto>(invoiceDto, Messages.Listed);
        }
        return new ErrorDataResult<InvoicesDto>(null, Messages.NotListed);
    }
    public async Task<IDataResult<List<InvoicesDto>>> GetByUserIdAsync(int userId)
    {
        var invoices = await _invoiceRepository.GetListAsync(x => x.UserId == userId);
        if (invoices?.Any() == true)
        {
            var invoiceDtos = _mapper.Map<List<InvoicesDto>>(invoices);
            return new SuccessDataResult<List<InvoicesDto>>(invoiceDtos, Messages.Listed);
        }
        return new ErrorDataResult<List<InvoicesDto>>(null, Messages.NotListed);
    }
    public async Task<IDataResult<IEnumerable<InvoicesDto>>> GetListAsync(Expression<Func<Invoice, bool>> filter = null)
    {
        if (filter == null)
        {
            var response = await _invoiceRepository.GetListAsync();
            var responseInvoiceDetailDto = _mapper.Map<IEnumerable<InvoicesDto>>(response);
            return new SuccessDataResult<IEnumerable<InvoicesDto>>(responseInvoiceDetailDto, Messages.Listed);
        }
        else
        {
            var response = await _invoiceRepository.GetListAsync(filter);
            var responseInvoiceDetailDto = _mapper.Map<IEnumerable<InvoicesDto>>(response);
            return new SuccessDataResult<IEnumerable<InvoicesDto>>(responseInvoiceDetailDto, Messages.Listed);
        }
    }
    public async Task<IDataResult<InvoiceUpdateDto>> UpdateAsync(InvoiceUpdateDto invoiceUpdateDto)
    {
        var getInvoicey = await _invoiceRepository.GetAsync(x => x.Id == invoiceUpdateDto.Id);
        var invoice = _mapper.Map<Invoice>(invoiceUpdateDto);

        invoice.UpdatedDate = DateTime.UtcNow;
        invoice.UpdatedBy = 1;

        var invoiceUpdate = await _invoiceRepository.UpdateAsync(invoice);
        var resultUpdateDto = _mapper.Map<InvoiceUpdateDto>(invoiceUpdate);
        return new SuccessDataResult<InvoiceUpdateDto>(resultUpdateDto, Messages.Updated);
    }
}
