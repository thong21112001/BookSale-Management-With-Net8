using BookSale.Management.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Domain.Entities
{
    public class Order
    {
        [Required]
        [StringLength(50)]
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Code { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public double TotalAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public StatusProcessing Status { get; set; }

        [Required]
        [StringLength(50)]
        public string UserId { get; set; } = string.Empty;
    }
}
