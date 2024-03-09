using BookSale.Management.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Management.Application.DTOs
{
	public class BookDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Author { get; set; }
		public string? Publisher { get; set; }
		public int Available { get; set; }
		public double Price { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsActive { get; set; }
		public int GenreId { get; set; }

		[ForeignKey(nameof(GenreId))]
		public Genre Genre { get; set; }
	}
}
