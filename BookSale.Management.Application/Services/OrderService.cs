﻿using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Checkout;
using BookSale.Management.Application.DTOs.Order;
using BookSale.Management.Application.DTOs.Report;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using BookSale.Management.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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



		#region Implement function into Admin
		//Hàm phân trang, hiển thị danh sách book lên Book/Index
		public async Task<ResponseDataTable<object>> GetAllOrderPaginationAsync(RequestDataTable request)
		{
			try
			{
				var (orders, totalRecords) = await _unitOfWork.OrderRepository.GetAllOrderByPagination<OrderDTO>((request.SkipItems / request.PageSize) + 1, request.PageSize, request.keyword);

				return new ResponseDataTable<object>
				{
					Draw = request.Draw,
					RecordsTotal = totalRecords,
					RecordsFiltered = totalRecords,
					Data = orders.Select(x => new
					{
						Id = x.Id,
						Code = x.Code,
						CreatedOn = x.CreatedOn,
						PaymentMethod = Enum.GetName(typeof(PaymentMethod), x.PaymentMethod),
						Status = Enum.GetName(typeof(StatusProcessing), x.Status),
						Fullname = x.Fullname,
						TotalPrice = x.TotalPrice
                    }).ToList()
				};
			}
			catch (Exception ex)
			{
				// Log or handle the exception here
				Console.WriteLine($"Error in GetAllOrderPaginationAsync: {ex.Message}");
				throw; // Rethrow the exception
			}
		}

		public async Task<ReportOrderDTO> GetReportByIdAsync(string id)
		{
			try
			{
                //Get 1 order tu id truyen vao
                var order = await _unitOfWork.Table<Order>()
                             .Where(x => x.Id == id)
                             .Include(x => x.Details)
                             .SingleAsync();

                var user = await _unitOfWork.Table<ApplicationUser>()
                                            .Where(u => u.Id == order.UserId)
                                            .SingleAsync();

                var details = order.Details.Join(_unitOfWork.Table<Book>(), x => x.ProductId,
                                                                                        y => y.Id,
                                                                                        (detail, book) => new OrderDetailDTO
                                                                                        {
                                                                                            Price = detail.UnitPrice,
                                                                                            Quantity = detail.Quantity,
                                                                                            ProductName = book.Title
                                                                                        }).ToList();

                var address = _mapper.Map<OrderAddressDTO>(user);

                return new ReportOrderDTO
                {
                    Code = order.Code,
                    CreateOn = order.CreatedOn,
					Address = address,
                    Details = details
                };
            }
			catch (Exception ex)
			{
                File.AppendAllText("error_log.txt", $"Bug: {ex}\n");
                throw;
			}
		}
		#endregion



		#region Implement function into Admin
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
		#endregion
	}
}
