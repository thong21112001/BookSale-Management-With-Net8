using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Domain.Entities
{
    public class ApplicationUser : IdentityUser // Trỏ chuột bấm vào IdentityUser xong bấm F12 để nhảy đến
    {
        [StringLength(250)]
        public string? Fullname { get; set; }

        [StringLength(1000)]
        public string? Address { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public string? MobilePhone { get; set; }
    }
}
