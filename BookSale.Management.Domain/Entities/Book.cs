using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Management.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string? Title { get; set; }

        [Required]
        [StringLength(250)]
        public string? Author { get; set; }

        [StringLength(250)]
        public string Publisher { get; set; }

        [Required]
        public int Available { get; set; }

        public double Price { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }
    }
}
