using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;
        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IResult> AddAsync(CategoryAddDto entity)
        {
            var newCategory = _mapper.Map<Category>(entity);
            await _categoryRepository.AddAsync(newCategory);
            return new SuccessResult(Messages.Added);
        }
        public async Task<IDataResult<bool>> DeleteAsync(int id)
        {
            var isDelete = await _categoryRepository.DeleteAsync(id);
            return new SuccessDataResult<bool>(isDelete, Messages.Deleted);
        }
        public async Task<IDataResult<CategoriesDto>> GetAsync(Expression<Func<Category, bool>> filter)
        {
            var category = await _categoryRepository.GetAsync(filter);
            if (category != null)
            {
                var categoryDto = _mapper.Map<CategoriesDto>(category);
                return new SuccessDataResult<CategoriesDto>(categoryDto, Messages.Listed);
            }
            return new ErrorDataResult<CategoriesDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<CategoriesDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(x=>x.Id ==id);
            if (category != null)
            {
                var categoryDto = _mapper.Map<CategoriesDto>(category);
                return new SuccessDataResult<CategoriesDto>(categoryDto, Messages.Listed);
            }
            return new ErrorDataResult<CategoriesDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<IEnumerable<CategoriesDto>>> GetListAsync(Expression<Func<Category, bool>> filter = null)
        {
            if (filter == null)
            {
                var response = await _categoryRepository.GetListAsync();
                var responseCategoryDetailDto = _mapper.Map<IEnumerable<CategoriesDto>>(response);
                return new SuccessDataResult<IEnumerable<CategoriesDto>>(responseCategoryDetailDto, Messages.Listed);
            }
            else
            {
                var response = await _categoryRepository.GetListAsync(filter);
                var responseCategoryDetailDto = _mapper.Map<IEnumerable<CategoriesDto>>(response);
                return new SuccessDataResult<IEnumerable<CategoriesDto>>(responseCategoryDetailDto, Messages.Listed);
            }
        }
        public async Task<IDataResult<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var getCategory = await _categoryRepository.GetAsync(x => x.Id == categoryUpdateDto.Id);
            var category = _mapper.Map<Category>(categoryUpdateDto);

            category.UpdatedDate = DateTime.UtcNow;
            category.UpdatedBy = 1;

            var categoryUpdate = await _categoryRepository.UpdateAsync(category);
            var resultUpdateDto = _mapper.Map<CategoryUpdateDto>(categoryUpdate);
            return new SuccessDataResult<CategoryUpdateDto>(resultUpdateDto, Messages.Updated);
        }
    }    
}
