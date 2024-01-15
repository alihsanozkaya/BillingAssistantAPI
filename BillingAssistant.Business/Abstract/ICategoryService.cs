using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResult> AddAsync(CategoryAddDto entity);
        Task<IDataResult<IEnumerable<CategoriesDto>>> GetListAsync(Expression<Func<Category, bool>> filter = null);
        Task<IDataResult<CategoriesDto>> GetAsync(Expression<Func<Category, bool>> filter);
        Task<IDataResult<CategoriesDto>> GetByIdAsync(int id);
        Task<IDataResult<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IDataResult<bool>> DeleteAsync(int id);
    }
}