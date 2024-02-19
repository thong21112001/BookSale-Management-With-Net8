//Ctrl + R + G xoá các using nhanh
//Ctrl + E + D sắp xếp các using khi chèn vào

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Management.DataAccess.DataAccess
{
    public class BookSaleDbContext : IdentityDbContext
    {
        public BookSaleDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
