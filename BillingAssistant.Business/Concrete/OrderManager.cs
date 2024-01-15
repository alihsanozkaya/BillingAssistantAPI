using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using BillingAssistant.Entities.DTOs.OrderDtos;
using BillingAssistant.Entities.DTOs.ProductDtos;
using BillingAssistant.Entities.DTOs.StoreDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderRepository _orderRepository;
        ICloudinaryService _cloudinaryService;
        IMapper _mapper;
        public OrderManager(IOrderRepository orderRepository, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<IResult> AddAsync(OrderAddDto entity)
        {
            var newOrder = _mapper.Map<Order>(entity);
            var imageUrl = await UploadImageAsync(entity.Image);
            newOrder.ImageUrl = imageUrl;
            await _orderRepository.AddAsync(newOrder);
            return new SuccessResult(Messages.Added);
        }
        public async Task<IDataResult<bool>> DeleteAsync(int id)
        {
            var isDelete = await _orderRepository.DeleteAsync(id);
            return new SuccessDataResult<bool>(isDelete, Messages.Deleted);
        }
        public async Task<IDataResult<OrdersDto>> GetAsync(Expression<Func<Order, bool>> filter)
        {
            var order = await _orderRepository.GetAsync(filter);
            if (order != null)
            {
                var orderDto = _mapper.Map<OrdersDto>(order);
                return new SuccessDataResult<OrdersDto>(orderDto, Messages.Listed);
            }
            return new ErrorDataResult<OrdersDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<OrdersDto>> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == id);
            if (order != null)
            {
                var orderDto = _mapper.Map<OrdersDto>(order);
                return new SuccessDataResult<OrdersDto>(orderDto, Messages.Listed);
            }
            return new ErrorDataResult<OrdersDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<IEnumerable<OrdersDto>>> GetListAsync(Expression<Func<Order, bool>> filter = null)
        {
            if (filter == null)
            {
                var response = await _orderRepository.GetListAsync();
                var responseOrderDetailDto = _mapper.Map<IEnumerable<OrdersDto>>(response);
                return new SuccessDataResult<IEnumerable<OrdersDto>>(responseOrderDetailDto, Messages.Listed);
            }
            else
            {
                var response = await _orderRepository.GetListAsync(filter);
                var responseOrderDetailDto = _mapper.Map<IEnumerable<OrdersDto>>(response);
                return new SuccessDataResult<IEnumerable<OrdersDto>>(responseOrderDetailDto, Messages.Listed);
            }
        }
        public async Task<IDataResult<OrderUpdateDto>> UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            var getOrder = await _orderRepository.GetAsync(x => x.Id == orderUpdateDto.Id);
            var order = _mapper.Map<Order>(orderUpdateDto);

            order.UpdatedDate = DateTime.UtcNow;
            order.UpdatedBy = 1;

            var orderUpdate = await _orderRepository.UpdateAsync(order);
            var resultUpdateDto = _mapper.Map<OrderUpdateDto>(orderUpdate);
            return new SuccessDataResult<OrderUpdateDto>(resultUpdateDto, Messages.Updated);
        }
        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            var imagePath = Path.GetTempFileName(); // Get a temporary file path to store the uploaded image
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var imageUrl = await _cloudinaryService.UploadImageAsync(imagePath);

            // Delete the temporary image file
            System.IO.File.Delete(imagePath);

            return imageUrl;
        }
    }
}