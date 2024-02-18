# BOOK SALE IMPLEMENT CLEAN ARCHITECHTURE

Triển khai kiến trúc của dự án sử dụng Onion Architechture:

- Thứ tự của các tầng: Presentation -> Infrastructure -> Application -> Domain.
- Các folder dự án bao gồm: Presentation/testing(UI), Application(Application), Infrastructure(DataAcess and Infrastructure, Domain(Core). 

## LIÊN KẾT GIỮA CÁC TẦNG TRONG DỰ ÁN

- Presentation(UI) liên kết với Infrastructure(Có 2 project là DataAcess và Infrastructure).
- Infrastructure(project Infrastructure) liên kết với Application và Infrastructure(project DataAcess).
- Application liên kết đến Domain(Core).
