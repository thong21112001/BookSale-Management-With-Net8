using BookSale.Management.Infrastructure.Abstracts;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace BookSale.Management.Infrastructure.Services
{
    public class ExcelHandlerService : IExcelHandlerService
    {
        public ExcelHandlerService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;   
        }

        public async Task<byte[]> Export<T>(List<T> data) where T : class, new()
        {
            if (!data.Any())
            {
                return default;
            }

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workBook = package.Workbook.Worksheets.Add("ReportOrder");

                T obj = new T();

                var properties = obj.GetType().GetProperties();

                //create data
                for (int row = 0; row < data.Count(); row++)
                {
                    var rowData = data[row];

                    for (int col = 0; col < properties.Count(); col++)
                    {
                        workBook.Cells[row+2,col+1].Value = rowData.GetType().GetProperty(properties[col].Name).GetValue(rowData);
                    }
                }

                //create header
                for (int i = 0; i < properties.Count(); i++)
                {
                    workBook.Cells[1, i + 1].Value = properties[i].Name;

                    workBook.Column(i+1).AutoFit();

                    workBook.Cells[1, i+1].Style.Font.Bold = true;
                    
                    workBook.Cells[1, i+1].Style.Fill.PatternType = ExcelFillStyle.Solid;

                    workBook.Cells[1, i+1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DDD"));
                }

                await package.SaveAsync();
            }

            return stream.ToArray();
        }
    }
}
