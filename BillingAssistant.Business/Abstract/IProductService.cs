using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract
{
    public interface IProductService
    {
        Task<IResult> AddAsync(ProductAddDto entity);
        Task<IDataResult<IEnumerable<ProductsDto>>> GetListAsync(Expression<Func<Product, bool>> filter = null);
        Task<IDataResult<ProductsDto>> GetAsync(Expression<Func<Product, bool>> filter);
        Task<IDataResult<ProductsDto>> GetByIdAsync(int id);
        Task<IDataResult<ProductUpdateDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<IDataResult<bool>> DeleteAsync(int id);
    }
}