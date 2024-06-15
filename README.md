# Mục lục
- [Giới thiệu về BOOK-STORE-MANAGEMENT](#BOOKSTOREMANAGEMENT)
- [Tổng quan về dự án](#Tổng-quan-về-dự-án)
- [Cấu trúc của dự án](#Cấu-trúc-các-thành-phần-trong-solution)
- [Lưu ý khi tải và chạy dự án](#Một-vài-lưu-ý-khi-đọc-code)
- [File restore SQL](#Link-SQL)
- [Liên hệ](#Liên-hệ)
- [Công nghệ sử dụng](#Công-nghệ)

# BOOKSTOREMANAGEMENT
Chào mừng đến với project book-store-management, dự án được tạo với mục đích giúp nâng cao kĩ năng và tìm hiểu một vài thư viện!

Book-store-management, với các tính năng:
- Thanh toán qua paypal, momo, vnpay.
- Thống kê, lọc, xuất báo cáo dưới dạng excel hoặc pdf.
- Sử dụng mô hình Clean Architechture để thiết kế dự án.
- Tích hợp đăng nhập với google, facebook.
- Và còn rất nhiều tính năng khác đang được phát triển và thêm vào dự án từng ngày.

# Tổng quan về dự án
- Dự án được phát triển trên .NET 8, có thể chạy trên tất cả các nền tảng mà .NET 8 hỗ trợ.
- Sử dụng tối thiểu các thư viện bên ngoài, kể cả các thư viện hỗ trợ HTTP từ .NET SDK.
- Sử dụng SQL Server 2022 để kết nối database, lưu trữ dữ liệu cho dự án.

# Cấu trúc các thành phần trong solution
Tìm hiểu về thứ tự cũng như folder dự án bao gồm những gì:
- Thứ tự của các tầng: Presentation -> Infrastructure -> Application -> Domain.
- Các folder dự án bao gồm: Presentation/testing(UI), Application(Application), Infrastructure(DataAcess and Infrastructure, Domain(Core)). 

Các dự án trong solution được chia thành các nhóm sau:
- Presentation(UI) liên kết với Infrastructure(Có 2 project là DataAccess và Infrastructure).
- Infrastructure(project Infrastructure) liên kết với Application và Infrastructure(project DataAccess).
- Infrastructure(project DataAccess) liên kết đến Domain(Core).
- Application liên kết đến Domain(Core).

Giải thích:
- Presentation : Tầng UI dùng để giao tiếp với người dùng có chứa các Controller và View, các setting, các layout...
- Infrastructure(project Infrastructure) : Sử dụng để cấu hình, đăng ký dịch vụ, sử dụng các thư viện bên ngoài.
- Infrastructure(project DataAccess) : Sử dụng để cấu hình database, triển khai các repository, các migrations, Dapper.
- Domain(Core) : Sử dụng để tạo các entities(các bảng cho database), các enum cần dùng, Setting mặc định(admin, smtp, page), các Abstracts của Repository.
- Application : Chứa các Service và Abstracts của Service, các DTO, cấu hình automap

# Một vài lưu ý khi đọc code:
- Cần có kiến thức về Clean Architechture, .Net, C#, SQL.
- Cần tìm hiểu các thư viện sử dụng trong dự án.

# Link SQL
* 🖥️  Google Drive tải file .bak để restore sql này về : [SQL](https://drive.google.com/file/d/1r2ZVxVdcfP_X4lW_0sSXx-OnbJUgmJOl/view?usp=drive_link)

# Liên hệ
* 🖥️  Facebook : [Trần Quang Thông](https://www.facebook.com/quangthong211101)
* ✉️  Email : [quangthong211101@gmail.com](mailto:quangthong211101@gmail.com)

# Công nghệ
<p align="left">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/html5/html5-original.svg" alt="HTML" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/css3/css3-original.svg" alt="CSS" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/bootstrap/bootstrap-original.svg" alt="Bootstrap" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/javascript/javascript-original.svg" alt="JavaScript" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/jquery/jquery-original.svg" alt="jQuery" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" alt="C#" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" alt=".NET" width="40" height="40"/>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" alt="SQL Server" width="40" height="40"/>
</p>
