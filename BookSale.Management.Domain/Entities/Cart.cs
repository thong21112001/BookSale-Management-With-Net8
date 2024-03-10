using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Management.Domain.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Code { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(1000)]
        public string Note { get; set; } = string.Empty;

        [Required]
        public bool Status { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
