using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.StoreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract
{
    public interface IStoreService
    {
        Task<IResult> AddAsync(StoreAddDto entity);
        Task<IDataResult<IEnumerable<StoresDto>>> GetListAsync(Expression<Func<Store, bool>> filter = null);
        Task<IDataResult<StoresDto>> GetAsync(Expression<Func<Store, bool>> filter);
        Task<IDataResult<StoresDto>> GetByIdAsync(int id);
        Task<IDataResult<StoreUpdateDto>> UpdateAsync(StoreUpdateDto storeUpdateDto);
        Task<IDataResult<bool>> DeleteAsync(int id);
    }
}