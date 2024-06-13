using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using BillingAssistant.Entities.DTOs.InvoiceDtos;
using BillingAssistant.Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract;

public interface IInvoiceService
{
    Task<IDataResult<Invoice>> AddAsync(InvoiceAddDto entity);
    Task<IDataResult<IEnumerable<InvoicesDto>>> GetListAsync(Expression<Func<Invoice, bool>> filter = null);
    Task<IDataResult<List<InvoicesDto>>> GetByUserIdAsync(int userId);
    Task<IDataResult<InvoicesDto>> GetAsync(Expression<Func<Invoice, bool>> filter);
    Task<IDataResult<InvoicesDto>> GetByIdAsync(int id);
    Task<IDataResult<InvoiceUpdateDto>> UpdateAsync(InvoiceUpdateDto invoiceUpdateDto);
    Task<IDataResult<bool>> DeleteAsync(int id);
}
