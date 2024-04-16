using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.Checkout;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Save(OrderRequestDTO requestDTO)
        {
            try
            {
                var order = _mapper.Map<Order>(requestDTO);

                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.OrderRepository.Save(order);
                await _unitOfWork.SaveChangeAsync();

                if (requestDTO.BookForCarts.Any())
                {
                    foreach (var item in requestDTO.BookForCarts)
                    {

                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = item.Id,
                            Quantity = item.Quantity,
                            UnitPrice = item.Price
                        };

                        await _unitOfWork.Table<OrderDetail>().AddAsync(orderDetail);
                    }
                    await _unitOfWork.SaveChangeAsync();
                }
                await _unitOfWork.SaveTransactionAsync();
                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

    }
}
