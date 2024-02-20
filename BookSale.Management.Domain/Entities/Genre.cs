using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Domain.Entities
{
    //Lớp con (Genre) thừa kế toàn bộ thuộc tính lớp cha (BaseEntity)
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]        
        public bool IsActive { get; set; }
    }
}
