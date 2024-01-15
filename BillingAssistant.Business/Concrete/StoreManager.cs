using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.StoreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Concrete
{
    public class StoreManager : IStoreService
    {
        IStoreRepository _storeRepository;
        IMapper _mapper;
        public StoreManager(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        public async Task<IResult> AddAsync(StoreAddDto entity)
        {
            var newStore = _mapper.Map<Store>(entity);
            await _storeRepository.AddAsync(newStore);
            return new SuccessResult(Messages.Added);
        }
        public async Task<IDataResult<bool>> DeleteAsync(int id)
        {
            var isDelete = await _storeRepository.DeleteAsync(id);
            return new SuccessDataResult<bool>(isDelete, Messages.Deleted);
        }
        public async Task<IDataResult<StoresDto>> GetAsync(Expression<Func<Store, bool>> filter)
        {
            var store = await _storeRepository.GetAsync(filter);
            if (store != null)
            {
                var storeDto = _mapper.Map<StoresDto>(store);
                return new SuccessDataResult<StoresDto>(storeDto, Messages.Listed);
            }
            return new ErrorDataResult<StoresDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<StoresDto>> GetByIdAsync(int id)
        {
            var store = await _storeRepository.GetAsync(x=>x.Id ==id);
            if (store != null)
            {
                var storeDto = _mapper.Map<StoresDto>(store);
                return new SuccessDataResult<StoresDto>(storeDto, Messages.Listed);
            }
            return new ErrorDataResult<StoresDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<IEnumerable<StoresDto>>> GetListAsync(Expression<Func<Store, bool>> filter = null)
        {
            if (filter == null)
            {
                var response = await _storeRepository.GetListAsync();
                var responseStoreDetailDto = _mapper.Map<IEnumerable<StoresDto>>(response);
                return new SuccessDataResult<IEnumerable<StoresDto>>(responseStoreDetailDto, Messages.Listed);
            }
            else
            {
                var response = await _storeRepository.GetListAsync(filter);
                var responseStoreDetailDto = _mapper.Map<IEnumerable<StoresDto>>(response);
                return new SuccessDataResult<IEnumerable<StoresDto>>(responseStoreDetailDto, Messages.Listed);
            }
        }
        public async Task<IDataResult<StoreUpdateDto>> UpdateAsync(StoreUpdateDto storeUpdateDto)
        {
            var getStore = await _storeRepository.GetAsync(x => x.Id == storeUpdateDto.Id);
            var store = _mapper.Map<Store>(storeUpdateDto);

            store.UpdatedDate = DateTime.UtcNow;
            store.UpdatedBy = 1;

            var storeUpdate = await _storeRepository.UpdateAsync(store);
            var resultUpdateDto = _mapper.Map<StoreUpdateDto>(storeUpdate);
            return new SuccessDataResult<StoreUpdateDto>(resultUpdateDto, Messages.Updated);
        }
    }    
}
