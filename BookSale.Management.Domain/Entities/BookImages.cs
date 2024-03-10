using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Management.Domain.Entities
{
    public class BookImages : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
    }
}
