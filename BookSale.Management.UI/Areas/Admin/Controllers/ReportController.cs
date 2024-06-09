using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.Report;
using BookSale.Management.Infrastructure.Abstracts;
using BookSale.Management.UI.Helpers;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class ReportController : BaseController
	{
        private readonly IPDFService _pDFService;
        private readonly IOrderService _orderService;
        private readonly IGenreService _genreService;
        private readonly IExcelHandlerService _excelHandlerService;

        public ReportController(IPDFService pDFService, IOrderService orderService, IGenreService genreService, 
                                IExcelHandlerService excelHandlerService)
        {
            _pDFService = pDFService;
            _orderService = orderService;
            _genreService = genreService;
            _excelHandlerService = excelHandlerService;
        }

        [Breadscrum("Order", "Report")]
        public async Task<IActionResult> Index(ReportOrderManagementDTO requestOrder)
		{
            IEnumerable<ResponseOrderManagementDTO> responseOrderDTO = new List<ResponseOrderManagementDTO>();

            var genres = await _genreService.GetGenreForDropDownList();
            ViewBag.Genres = genres;

            if (!string.IsNullOrEmpty(requestOrder.FromDay) && !string.IsNullOrEmpty(requestOrder.ToDay))
            {
                responseOrderDTO = await _orderService.GetReportOrderAsync(requestOrder);
            }

            ViewBag.FilterData = requestOrder;

            return View(responseOrderDTO);
		}

		[HttpGet]
		public async Task<IActionResult> ExportPdfOrder(string id)
		{
			var getReport = await _orderService.GetReportByIdAsync(id);

            string html = await this.RenderViewAsync<ReportOrderDTO>(RouteData,"_LayoutReportOrder", getReport, true);

			var result = _pDFService.GeneratePDF(html);

			return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
		}

        [HttpGet]
        public async Task<IActionResult> ExportExcelOrder(ReportOrderManagementDTO requestOrder)
        {
            var responseOrderDTO = await _orderService.GetReportOrderAsync(requestOrder);

            var stream = await _excelHandlerService.Export<ResponseOrderManagementDTO>(responseOrderDTO.ToList());

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"OrdersReport{DateTime.Now.Ticks}.xlsx");
        }
    }
}
