using BookSale.Management.Infrastructure.Abstracts;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace BookSale.Management.Infrastructure.Services
{
	public class PDFService : IPDFService
	{
		private readonly IConverter _converter;

		public PDFService(IConverter converter)
		{
			_converter = converter;
		}

		public byte[] GeneratePDF(string contentHTML, 
									Orientation orientation = Orientation.Portrait, 
									PaperKind paperKind = PaperKind.A4)
		{
			var globalSetting = new GlobalSettings
			{
				ColorMode = ColorMode.Color,
				Orientation = orientation,
				PaperSize = paperKind,
				Margins = new MarginSettings() { Top = 10, Bottom = 10 }
			};

			var objectSetting = new ObjectSettings
			{
				PagesCount = true,
				HtmlContent = contentHTML,
				WebSettings = {DefaultEncoding = "utf-8"}
			};

			var pdf = new HtmlToPdfDocument()
			{
				GlobalSettings = globalSetting,
				Objects = { objectSetting }
			};

			return _converter.Convert(pdf);
        }
	}
}
