using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookSale.Management.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Available = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OldPrice = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresse_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCatalogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CatalogueId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCatalogue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCatalogue_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCatalogue_Catalogue_CatalogueId",
                        column: x => x.CatalogueId,
                        principalTable: "Catalogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookImages_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartDetails_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetails_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Catalogue",
                columns: new[] { "Id", "Description", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, "Văn học", true, "Văn học" },
                    { 2, "Kinh tế", true, "Kinh tế" },
                    { 3, "Sách thiếu nhi", true, "Sách thiếu nhi" },
                    { 4, "Sách ngoại ngữ", true, "Sách ngoại ngữ" },
                    { 5, "Giáo khoa - Tham khảo", true, "Giáo khoa - Tham khảo" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Light Novel", true, "Light Novel" },
                    { 2, "Marketing", true, "Marketing" },
                    { 3, "Comic", true, "Comic" },
                    { 4, "Tiếng Anh", true, "Tiếng Anh" },
                    { 5, "Sách giáo khoa", true, "Sách giáo khoa" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62150f4f-db4e-49f3-8f1c-0f2e2188ca1b", null, "User", "USER" },
                    { "796919ae-8ffd-48da-9a30-0433929684cc", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "Available", "Code", "CreatedOn", "Description", "GenreId", "Image", "IsActive", "OldPrice", "Price", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "Yuki Yaku, Fly", 20, "xIl12Uia", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1903), "Nhân Vật Hạ Cấp Tomozaki", 1, "https://cdn0.fahasa.com/media/catalog/product/n/h/nhan-vat-ha-cap-tomozaki_tap-6-5_ban-gioi-han.jpg", true, 0.0, 110000.0, "Nhà Xuất Bản Kim Đồng", "Nhân Vật Hạ Cấp Tomozaki" },
                    { 2, "Jougi Shiraishi, Azure", 25, "eLa29ikM", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1920), "Hành Trình Của Elaina", 1, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_n-th_ng-e14.jpg", true, 115000.0, 97000.0, "XYZ", "Hành Trình Của Elaina" },
                    { 3, "Kinugasa Syougo, Tomoseshunsaku", 20, "ka2901aM", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1924), "Chào Mừng Đến Lớp Học Đề Cao Thực Lực", 1, "https://cdn0.fahasa.com/media/catalog/product/2/3/230424.jpg", true, 0.0, 290000.0, "IPM", "Chào Mừng Đến Lớp Học Đề Cao Thực Lực" },
                    { 4, "Og Mandino", 20, "nBHvD89t", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1927), "Người Bán Hàng Vĩ Đại Nhất Thế Giới", 2, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_47973.jpg", true, 148000.0, 103000.0, "FIRST NEWS", "Người Bán Hàng Vĩ Đại Nhất Thế Giới" },
                    { 5, "Napoleon Hill", 20, "nDjjk922", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1929), "Để Thế Giới Biết Bạn Là Ai", 2, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935086855324.jpg", true, 138000.0, 96000.0, "FIRST NEWS", "Để Thế Giới Biết Bạn Là Ai" },
                    { 6, "Philip Kotler, Gary Armstrong", 20, "thU23bny", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1932), "Nguyên Lý Marketing", 2, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3191421803753_d34be7758308b8ee74572ebf885cbf9a.jpg", true, 999000.0, 779000.0, "Alpha Books", "Nguyên Lý Marketing" },
                    { 7, "Dubu (Redice Studio), Chugong", 20, "SLvng292", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1935), "Solo Leveling", 3, "https://cdn0.fahasa.com/media/catalog/product/s/o/solo-leveling_bia_obi_card_tap-10.jpg", true, 88000.0, 84000.0, "Nhà Xuất Bản Kim Đồng", "Solo Leveling" },
                    { 8, "Gosho Aoyama", 20, "ttLDC72J", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1938), "Thám Tử Lừng Danh Conan", 3, "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/FrameAds_03_1080X1080.png", true, 25000.0, 21000.0, "Nhà Xuất Bản Kim Đồng", "Thám Tử Lừng Danh Conan" },
                    { 9, "Soubee Amako", 20, "JakOk29L", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1940), "Ninja Rantaro", 3, "https://cdn0.fahasa.com/media/catalog/product/n/i/ninja-rantaro_bia_tap-23.jpg", true, 40000.0, 38000.0, "Nhà Xuất Bản Kim Đồng", "Ninja Rantaro" },
                    { 10, "Trang Anh, Minh Anh", 20, "taCNBD00", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1944), "Tiếng Anh Cho NgườI Bắt Đầu", 4, "https://cdn0.fahasa.com/media/catalog/product/9/7/9786043987102.jpg", true, 200000.0, 149000.0, "Công Ty Cổ Phần Công Nghệ", "Tiếng Anh Cho NgườI Bắt Đầu" },
                    { 11, "Kenvin Kang, Hanna Byun", 20, "nuanCE50", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1947), "Nuance - 50 Sắc Thái Của Từ", 4, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935309503834.jpg", true, 159000.0, 116000.0, "Alpha Books", "Nuance - 50 Sắc Thái Của Từ" },
                    { 12, "ThS Phan Hoàng Văn", 20, "btVLthcs", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1949), "500 Bài Tập Vật Lí Trung Học Cơ Sở", 5, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935083581509.jpg", true, 145000.0, 104000.0, "NXB Đại Học Quốc Gia TP.HCM", "500 Bài Tập Vật Lí Trung Học Cơ Sở" },
                    { 13, "Nhiều tác giả", 20, "sgkL11mm", new DateTime(2024, 3, 19, 9, 34, 15, 485, DateTimeKind.Local).AddTicks(1952), "Sách Giáo Khoa Bộ Lớp 1", 5, "https://cdn0.fahasa.com/media/catalog/product/3/3/3300000026817.jpg", true, 186000.0, 176000.0, "Nhà xuất bản Giáo Dục", "Sách Giáo Khoa Bộ Lớp 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCatalogue_BookId",
                table: "BookCatalogue",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCatalogue_CatalogueId",
                table: "BookCatalogue",
                column: "CatalogueId");

            migrationBuilder.CreateIndex(
                name: "IX_BookImages_BookId",
                table: "BookImages",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_BookId",
                table: "CartDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresse_UserId",
                table: "UserAddresse",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCatalogue");

            migrationBuilder.DropTable(
                name: "BookImages");

            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserAddresse");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Catalogue");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
