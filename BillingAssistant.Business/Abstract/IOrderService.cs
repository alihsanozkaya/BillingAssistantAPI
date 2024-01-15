using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract
{
    public interface IOrderService
    {
        Task<IResult> AddAsync(OrderAddDto entity);
        Task<IDataResult<IEnumerable<OrdersDto>>> GetListAsync(Expression<Func<Order, bool>> filter = null);
        Task<IDataResult<OrdersDto>> GetAsync(Expression<Func<Order, bool>> filter);
        Task<IDataResult<OrdersDto>> GetByIdAsync(int id);
        Task<IDataResult<OrderUpdateDto>> UpdateAsync(OrderUpdateDto orderUpdateDto);
        Task<IDataResult<bool>> DeleteAsync(int id);
    }
}