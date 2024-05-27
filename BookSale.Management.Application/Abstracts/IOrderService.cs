using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Checkout;
using BookSale.Management.Application.DTOs.Report;

namespace BookSale.Management.Application.Abstracts
{
    public interface IOrderService
    {
        Task<bool> Save(OrderRequestDTO requestDTO);
		Task<ResponseDataTable<object>> GetAllOrderPaginationAsync(RequestDataTable request);
		Task<ReportOrderDTO> GetReportByIdAsync(string id);

	}
}
