using BookSale.Management.Domain.Enums;

namespace BookSale.Management.Application.DTOs.Report
{
    //Tên các trường đặt đúng thứ tự select và tên trong storeprocedure
    public class ResponseOrderManagementDTO
    {
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public StatusProcessing Status { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
