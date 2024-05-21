using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
        {
			_orderService = orderService;
		}

        [Breadscrum("Order List", "Management")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetOrderPagination(RequestDataTable requestDataTable)
        {
            var data = await _orderService.GetAllOrderPaginationAsync(requestDataTable);
            return Json(data);
        }
    }
}
