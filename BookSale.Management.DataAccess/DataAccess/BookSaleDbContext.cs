//Ctrl + R + G xoá các using nhanh
//Ctrl + E + D sắp xếp các using khi chèn vào

using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Management.DataAccess.DataAccess
{
    public class BookSaleDbContext : IdentityDbContext<ApplicationUser,IdentityRole,string>
    {
        public BookSaleDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookCatalogue> BookCatalogue { get; set; }
        public DbSet<BookImages> BookImages { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Catalogue> Catalogue { get; set; }
        public DbSet<UserAddress> UserAddresse { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetail> CartDetails  { get; set; }

        //Phương thức gọi sau khi đăng ký DbContext
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Mặc định khi tạo Migration là AspNetUser+..
            //Thay đổi bằng cách tự định nghĩa tên
            //AspNetUserToken->UserToken
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");   
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");   
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");   
        }
    }
}
