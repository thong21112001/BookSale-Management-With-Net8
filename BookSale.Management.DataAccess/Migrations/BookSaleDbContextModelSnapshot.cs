﻿// <auto-generated />
using System;
using BookSale.Management.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookSale.Management.DataAccess.Migrations
{
    [DbContext(typeof(BookSaleDbContext))]
    partial class BookSaleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookSale.Management.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Fullname")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Available")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("OldPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Publisher")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Yuki Yaku, Fly",
                            Available = 20,
                            Code = "xIl12Uia",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1903),
                            Description = "Nhân Vật Hạ Cấp Tomozaki",
                            GenreId = 1,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/n/h/nhan-vat-ha-cap-tomozaki_tap-6-5_ban-gioi-han.jpg",
                            IsActive = true,
                            OldPrice = 0.0,
                            Price = 110000.0,
                            Publisher = "Nhà Xuất Bản Kim Đồng",
                            Title = "Nhân Vật Hạ Cấp Tomozaki"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Jougi Shiraishi, Azure",
                            Available = 25,
                            Code = "eLa29ikM",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1920),
                            Description = "Hành Trình Của Elaina",
                            GenreId = 1,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/b/_/b_n-th_ng-e14.jpg",
                            IsActive = true,
                            OldPrice = 115000.0,
                            Price = 97000.0,
                            Publisher = "XYZ",
                            Title = "Hành Trình Của Elaina"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Kinugasa Syougo, Tomoseshunsaku",
                            Available = 20,
                            Code = "ka2901aM",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1924),
                            Description = "Chào Mừng Đến Lớp Học Đề Cao Thực Lực",
                            GenreId = 1,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/2/3/230424.jpg",
                            IsActive = true,
                            OldPrice = 0.0,
                            Price = 290000.0,
                            Publisher = "IPM",
                            Title = "Chào Mừng Đến Lớp Học Đề Cao Thực Lực"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Og Mandino",
                            Available = 20,
                            Code = "nBHvD89t",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1927),
                            Description = "Người Bán Hàng Vĩ Đại Nhất Thế Giới",
                            GenreId = 2,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_47973.jpg",
                            IsActive = true,
                            OldPrice = 148000.0,
                            Price = 103000.0,
                            Publisher = "FIRST NEWS",
                            Title = "Người Bán Hàng Vĩ Đại Nhất Thế Giới"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Napoleon Hill",
                            Available = 20,
                            Code = "nDjjk922",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1929),
                            Description = "Để Thế Giới Biết Bạn Là Ai",
                            GenreId = 2,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/8/9/8935086855324.jpg",
                            IsActive = true,
                            OldPrice = 138000.0,
                            Price = 96000.0,
                            Publisher = "FIRST NEWS",
                            Title = "Để Thế Giới Biết Bạn Là Ai"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Philip Kotler, Gary Armstrong",
                            Available = 20,
                            Code = "thU23bny",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1932),
                            Description = "Nguyên Lý Marketing",
                            GenreId = 2,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/z/3/z3191421803753_d34be7758308b8ee74572ebf885cbf9a.jpg",
                            IsActive = true,
                            OldPrice = 999000.0,
                            Price = 779000.0,
                            Publisher = "Alpha Books",
                            Title = "Nguyên Lý Marketing"
                        },
                        new
                        {
                            Id = 7,
                            Author = "Dubu (Redice Studio), Chugong",
                            Available = 20,
                            Code = "SLvng292",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1935),
                            Description = "Solo Leveling",
                            GenreId = 3,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/s/o/solo-leveling_bia_obi_card_tap-10.jpg",
                            IsActive = true,
                            OldPrice = 88000.0,
                            Price = 84000.0,
                            Publisher = "Nhà Xuất Bản Kim Đồng",
                            Title = "Solo Leveling"
                        },
                        new
                        {
                            Id = 8,
                            Author = "Gosho Aoyama",
                            Available = 20,
                            Code = "ttLDC72J",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1938),
                            Description = "Thám Tử Lừng Danh Conan",
                            GenreId = 3,
                            Image = "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/FrameAds_03_1080X1080.png",
                            IsActive = true,
                            OldPrice = 25000.0,
                            Price = 21000.0,
                            Publisher = "Nhà Xuất Bản Kim Đồng",
                            Title = "Thám Tử Lừng Danh Conan"
                        },
                        new
                        {
                            Id = 9,
                            Author = "Soubee Amako",
                            Available = 20,
                            Code = "JakOk29L",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1940),
                            Description = "Ninja Rantaro",
                            GenreId = 3,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/n/i/ninja-rantaro_bia_tap-23.jpg",
                            IsActive = true,
                            OldPrice = 40000.0,
                            Price = 38000.0,
                            Publisher = "Nhà Xuất Bản Kim Đồng",
                            Title = "Ninja Rantaro"
                        },
                        new
                        {
                            Id = 10,
                            Author = "Trang Anh, Minh Anh",
                            Available = 20,
                            Code = "taCNBD00",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1944),
                            Description = "Tiếng Anh Cho NgườI Bắt Đầu",
                            GenreId = 4,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/9/7/9786043987102.jpg",
                            IsActive = true,
                            OldPrice = 200000.0,
                            Price = 149000.0,
                            Publisher = "Công Ty Cổ Phần Công Nghệ",
                            Title = "Tiếng Anh Cho NgườI Bắt Đầu"
                        },
                        new
                        {
                            Id = 11,
                            Author = "Kenvin Kang, Hanna Byun",
                            Available = 20,
                            Code = "nuanCE50",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1947),
                            Description = "Nuance - 50 Sắc Thái Của Từ",
                            GenreId = 4,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/8/9/8935309503834.jpg",
                            IsActive = true,
                            OldPrice = 159000.0,
                            Price = 116000.0,
                            Publisher = "Alpha Books",
                            Title = "Nuance - 50 Sắc Thái Của Từ"
                        },
                        new
                        {
                            Id = 12,
                            Author = "ThS Phan Hoàng Văn",
                            Available = 20,
                            Code = "btVLthcs",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1949),
                            Description = "500 Bài Tập Vật Lí Trung Học Cơ Sở",
                            GenreId = 5,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/8/9/8935083581509.jpg",
                            IsActive = true,
                            OldPrice = 145000.0,
                            Price = 104000.0,
                            Publisher = "NXB Đại Học Quốc Gia TP.HCM",
                            Title = "500 Bài Tập Vật Lí Trung Học Cơ Sở"
                        },
                        new
                        {
                            Id = 13,
                            Author = "Nhiều tác giả",
                            Available = 20,
                            Code = "sgkL11mm",
                            CreatedOn = new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1952),
                            Description = "Sách Giáo Khoa Bộ Lớp 1",
                            GenreId = 5,
                            Image = "https://cdn0.fahasa.com/media/catalog/product/3/3/3300000026817.jpg",
                            IsActive = true,
                            OldPrice = 186000.0,
                            Price = 176000.0,
                            Publisher = "Nhà xuất bản Giáo Dục",
                            Title = "Sách Giáo Khoa Bộ Lớp 1"
                        });
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.BookCatalogue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("CatalogueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CatalogueId");

                    b.ToTable("BookCatalogue");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.BookImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookImages");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.CartDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CartId");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.Catalogue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Catalogue");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Văn học",
                            IsActive = true,
                            Title = "Văn học"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Kinh tế",
                            IsActive = true,
                            Title = "Kinh tế"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Sách thiếu nhi",
                            IsActive = true,
                            Title = "Sách thiếu nhi"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Sách ngoại ngữ",
                            IsActive = true,
                            Title = "Sách ngoại ngữ"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Giáo khoa - Tham khảo",
                            IsActive = true,
                            Title = "Giáo khoa - Tham khảo"
                        });
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Light Novel",
                            IsActive = true,
                            Name = "Light Novel"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Marketing",
                            IsActive = true,
                            Name = "Marketing"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Comic",
                            IsActive = true,
                            Name = "Comic"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Tiếng Anh",
                            IsActive = true,
                            Name = "Tiếng Anh"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Sách giáo khoa",
                            IsActive = true,
                            Name = "Sách giáo khoa"
                        });
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAddresse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "62150f4f-db4e-49f3-8f1c-0f2e2188ca1b",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "796919ae-8ffd-48da-9a30-0433929684cc",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.Book", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.BookCatalogue", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookSale.Management.Domain.Entities.Catalogue", "Catalogue")
                        .WithMany()
                        .HasForeignKey("CatalogueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Catalogue");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.BookImages", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.Cart", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.CartDetail", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookSale.Management.Domain.Entities.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.UserAddress", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookSale.Management.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BookSale.Management.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookSale.Management.Domain.Entities.Genre", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
