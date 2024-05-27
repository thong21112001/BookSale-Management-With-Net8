using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs.Report;
using BookSale.Management.Infrastructure.Abstracts;
using BookSale.Management.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class ReportController : BaseController
	{
        private readonly IPDFService _pDFService;
        private readonly IOrderService _orderService;

		public ReportController(IPDFService pDFService, IOrderService orderService)
        {
            _pDFService = pDFService;
            _orderService = orderService;
		}

        public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> ExportPdfOrder(string id)
		{
			var getReport = await _orderService.GetReportByIdAsync(id);

            string html = await this.RenderViewAsync<ReportOrderDTO>(RouteData,"_LayoutReportOrder", getReport, true);

			var result = _pDFService.GeneratePDF(html);

			return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
		}
	}
}
