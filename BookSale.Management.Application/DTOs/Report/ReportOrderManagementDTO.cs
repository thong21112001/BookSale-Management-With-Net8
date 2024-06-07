using BookSale.Management.Domain.Enums;

namespace BookSale.Management.Application.DTOs.Report
{
    public class ReportOrderManagementDTO
    {
        public string FromDay { get; set; } = string.Empty;
        public string ToDay { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public StatusProcessing Status { get; set; }
    }
}
