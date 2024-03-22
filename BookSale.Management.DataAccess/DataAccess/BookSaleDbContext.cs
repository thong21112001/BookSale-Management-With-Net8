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

			builder.Entity<Catalogue>().HasData(
				new Catalogue { Id = 1, Title = "Văn học", Description = "Văn học", IsActive = true },
				new Catalogue { Id = 2, Title = "Kinh tế", Description = "Kinh tế", IsActive = true },
				new Catalogue { Id = 3, Title = "Sách thiếu nhi", Description = "Sách thiếu nhi", IsActive = true },
				new Catalogue { Id = 4, Title = "Sách ngoại ngữ", Description = "Sách ngoại ngữ", IsActive = true },
				new Catalogue { Id = 5, Title = "Giáo khoa - Tham khảo", Description = "Giáo khoa - Tham khảo", IsActive = true }
			);

			builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Light Novel", Description = "Light Novel", IsActive = true },
                new Genre { Id = 2, Name = "Marketing", Description = "Marketing", IsActive = true },
                new Genre { Id = 3, Name = "Comic", Description = "Comic", IsActive = true },
                new Genre { Id = 4, Name = "Tiếng Anh", Description = "Tiếng Anh", IsActive = true },
                new Genre { Id = 5, Name = "Sách giáo khoa", Description = "Sách giáo khoa", IsActive = true }
            );

			builder.Entity<Book>().HasData(
				new Book { 
                    Id = 1,
                    Title = "Nhân Vật Hạ Cấp Tomozaki",
                    Code = "xIl12Uia",
                    Author = "Yuki Yaku, Fly",
                    Publisher = "Nhà Xuất Bản Kim Đồng",
                    Description = "Nhân Vật Hạ Cấp Tomozaki",
                    Available = 20,
                    Price = 110000,
                    OldPrice = 0,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/n/h/nhan-vat-ha-cap-tomozaki_tap-6-5_ban-gioi-han.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 1,
                },
				new Book
				{
                    Id = 2,
					Title = "Hành Trình Của Elaina",
					Code = "eLa29ikM",
					Author = "Jougi Shiraishi, Azure",
					Publisher = "XYZ",
					Description = "Hành Trình Của Elaina",
					Available = 25,
					Price = 97000,
                    OldPrice = 115000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/b/_/b_n-th_ng-e14.jpg",
                    CreatedOn = DateTime.Now,
					IsActive = true,
					GenreId = 1,
				}, 
                new Book
				{
                    Id = 3,
                    Title = "Chào Mừng Đến Lớp Học Đề Cao Thực Lực",
                    Code = "ka2901aM",
                    Author = "Kinugasa Syougo, Tomoseshunsaku",
                    Publisher = "IPM",
                    Description = "Chào Mừng Đến Lớp Học Đề Cao Thực Lực",
                    Available = 20,
                    Price = 290000,
                    OldPrice = 0,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/2/3/230424.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 1,
                },
                new Book
                {
                    Id = 4,
                    Title = "Người Bán Hàng Vĩ Đại Nhất Thế Giới",
                    Code = "nBHvD89t",
                    Author = "Og Mandino",
                    Publisher = "FIRST NEWS",
                    Description = "Người Bán Hàng Vĩ Đại Nhất Thế Giới",
                    Available = 20,
                    Price = 103000,
                    OldPrice = 148000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_47973.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 2,
                },
                new Book
                {
                    Id = 5,
                    Title = "Để Thế Giới Biết Bạn Là Ai",
                    Code = "nDjjk922",
                    Author = "Napoleon Hill",
                    Publisher = "FIRST NEWS",
                    Description = "Để Thế Giới Biết Bạn Là Ai",
                    Available = 20,
                    Price = 96000,
                    OldPrice = 138000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/8/9/8935086855324.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 2,
                },
                new Book
                {
                    Id = 6,
                    Title = "Nguyên Lý Marketing",
                    Code = "thU23bny",
                    Author = "Philip Kotler, Gary Armstrong",
                    Publisher = "Alpha Books",
                    Description = "Nguyên Lý Marketing",
                    Available = 20,
                    Price = 779000,
                    OldPrice = 999000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/z/3/z3191421803753_d34be7758308b8ee74572ebf885cbf9a.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 2,
                },
                new Book
                {
                    Id = 7,
                    Title = "Solo Leveling",
                    Code = "SLvng292",
                    Author = "Dubu (Redice Studio), Chugong",
                    Publisher = "Nhà Xuất Bản Kim Đồng",
                    Description = "Solo Leveling",
                    Available = 20,
                    Price = 84000,
                    OldPrice = 88000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/s/o/solo-leveling_bia_obi_card_tap-10.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 3,
                },
                new Book
                {
                    Id = 8,
                    Title = "Thám Tử Lừng Danh Conan",
                    Code = "ttLDC72J",
                    Author = "Gosho Aoyama",
                    Publisher = "Nhà Xuất Bản Kim Đồng",
                    Description = "Thám Tử Lừng Danh Conan",
                    Available = 20,
                    Price = 21000,
                    OldPrice = 25000,
                    Image = "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/FrameAds_03_1080X1080.png",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 3,
                },
                new Book
                {
                    Id = 9,
                    Title = "Ninja Rantaro",
                    Code = "JakOk29L",
                    Author = "Soubee Amako",
                    Publisher = "Nhà Xuất Bản Kim Đồng",
                    Description = "Ninja Rantaro",
                    Available = 20,
                    Price = 38000,
                    OldPrice = 40000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/n/i/ninja-rantaro_bia_tap-23.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 3,
                },
                new Book
                {
                    Id = 10,
                    Title = "Tiếng Anh Cho NgườI Bắt Đầu",
                    Code = "taCNBD00",
                    Author = "Trang Anh, Minh Anh",
                    Publisher = "Công Ty Cổ Phần Công Nghệ",
                    Description = "Tiếng Anh Cho NgườI Bắt Đầu",
                    Available = 20,
                    Price = 149000,
                    OldPrice = 200000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/9/7/9786043987102.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 4,
                },
                new Book
                {
                    Id = 11,
                    Title = "Nuance - 50 Sắc Thái Của Từ",
                    Code = "nuanCE50",
                    Author = "Kenvin Kang, Hanna Byun",
                    Publisher = "Alpha Books",
                    Description = "Nuance - 50 Sắc Thái Của Từ",
                    Available = 20,
                    Price = 116000,
                    OldPrice = 159000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/8/9/8935309503834.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 4,
                },
                new Book
                {
                    Id = 12,
                    Title = "500 Bài Tập Vật Lí Trung Học Cơ Sở",
                    Code = "btVLthcs",
                    Author = "ThS Phan Hoàng Văn",
                    Publisher = "NXB Đại Học Quốc Gia TP.HCM",
                    Description = "500 Bài Tập Vật Lí Trung Học Cơ Sở",
                    Available = 20,
                    Price = 104000,
                    OldPrice = 145000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/8/9/8935083581509.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 5,
                },
                new Book
                {
                    Id = 13,
                    Title = "Sách Giáo Khoa Bộ Lớp 1",
                    Code = "sgkL11mm",
                    Author = "Nhiều tác giả",
                    Publisher = "Nhà xuất bản Giáo Dục",
                    Description = "Sách Giáo Khoa Bộ Lớp 1",
                    Available = 20,
                    Price = 176000,
                    OldPrice = 186000,
                    Image = "https://cdn0.fahasa.com/media/catalog/product/3/3/3300000026817.jpg",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    GenreId = 5,
                }
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "62150f4f-db4e-49f3-8f1c-0f2e2188ca1b",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
					Id = "796919ae-8ffd-48da-9a30-0433929684cc",
					Name = "Manager",
					NormalizedName = "MANAGER"
				}
            );
		}
    }
}
