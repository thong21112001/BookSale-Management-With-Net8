using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.Checkout;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Save(CartRequestDTO requestDTO)
        {
            try
            {
                var cart = _mapper.Map<Cart>(requestDTO);

                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.CartRepository.Save(cart);
                await _unitOfWork.SaveChangeAsync();

                if (requestDTO.BookForCarts.Any())
                {
                    foreach (var item in requestDTO.BookForCarts)
                    {

                        var cartDetail = new CartDetail
                        {
                            Price = item.Price,
                            Quantity = item.Quantity,
                            Note = "Book",
                            IsActive = true,
                            CartId = cart.Id,
                            BookId = item.Id,
                        };

                        await _unitOfWork.Table<CartDetail>().AddAsync(cartDetail);
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
