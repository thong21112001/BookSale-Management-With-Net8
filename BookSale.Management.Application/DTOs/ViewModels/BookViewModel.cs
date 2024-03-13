using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Xin chọn thể loại")]
        [DisplayName("THỂ LOẠI")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Không để trống")]
        [DisplayName("TIÊU ĐỀ")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Không để trống")]
        [DisplayName("SỐ LƯỢNG")]
        public int Available { get; set; }

        [Required(ErrorMessage = "Không để trống")]
        [DisplayName("ĐƠN GIÁ")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Không để trống")]
        [DisplayName("NGƯỜI GIAO")]
        public string? Publisher { get; set; }

        [Required(ErrorMessage = "Không để trống")]
        [DisplayName("TÁC GIẢ")]
        public string? Author { get; set; }
    }
}
