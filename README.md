# BOOK SALE IMPLEMENT CONFIG AND CONNECT DATABASE

Triển khai cấu hình đăng ký và kết nối cơ sở dữ liệu:

- B1: Thiết lập chuỗi kết nối ở UI trong Presentation file appsettings.josn.
- B2: Ở trong Infrastructure folder DataAccess tạo mới class BookSaleDbContext.cs để migration.
- B3: Ở trong Infrastructure folder Infrastructure tạo mới class ConfigurationDbAccess.cs để quản lý đăng ký chuỗi kn vào nhìu cái khác, tách từ Program.cs trong tầng Presentation.
