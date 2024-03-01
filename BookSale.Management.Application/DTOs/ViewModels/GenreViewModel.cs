using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs.ViewModels
{
    public class GenreViewModel
    {
        public int? Id { get; set; } = 0;
        [Required(ErrorMessage = "Name is not null")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
