# Website Nghe Nhạc Trực Tuyến

## Đề tài: Xây dựng Website nghe nhạc trực tuyến (ASP.NET)

Đồ án chuyên đề ASP.NET xây dựng website nghe nhạc trực tuyến với đầy đủ chức năng dành cho người dùng và quản trị viên, được phát triển bằng **C# / ASP.NET Web Forms** kết nối với **Microsoft SQL Server**.

---

# Thông tin đồ án

| Thông tin                | Chi tiết                                                                   |
| ------------------------ | -------------------------------------------------------------------------- |
| **Trường**               | Đại học Trà Vinh – Trường Kỹ thuật và Công nghệ – Khoa Công nghệ Thông tin |
| **Lớp**                  | DK24TT80161                                                                |
| **Giảng viên hướng dẫn** | TS. Đoàn Phước Miền                                                        |
| **Năm học**              | 2026                                                                       |

### Nhóm sinh viên thực hiện

| STT | Họ và tên    | MSSV      |
| --: | ------------ | --------- |
|   1 | Lê Việt Hoài | 170124610 |
|   2 | Lê Thị Nhàn  | 170124608 |
|   3 | Đỗ Xuân Thủy | 170124411 |

---

# 1. Giới thiệu

Website nghe nhạc trực tuyến cho phép người dùng nghe nhạc, tìm kiếm bài hát, xem thông tin ca sĩ, album, chủ đề, thể loại và thưởng thức âm nhạc theo playlist. Bên cạnh đó, hệ thống còn cung cấp trang quản trị (Admin) giúp quản lý toàn bộ dữ liệu của website.

### Công nghệ sử dụng

* **Ngôn ngữ lập trình:** C#
* **Nền tảng:** ASP.NET Web Forms (.NET Framework)
* **Cơ sở dữ liệu:** Microsoft SQL Server
* **Truy xuất dữ liệu:** ADO.NET
* **Giao diện:** HTML5, CSS3, Bootstrap, JavaScript, jQuery
* **Kiến trúc:** Model – View – Controller (MVC)
* **Quy trình phát triển:** Waterfall
* **Công cụ phát triển:** Visual Studio 2019, SQL Server Management Studio (SSMS), Git, GitHub

---

# 2. Chức năng chính

## Người dùng

* Xem trang chủ với danh sách bài hát, ca sĩ và album nổi bật.
* Nghe nhạc trực tuyến.
* Xem lời bài hát.
* Xem danh sách và thông tin chi tiết của:

  * Bài hát
  * Ca sĩ
  * Album
  * Chủ đề
  * Thể loại
* Nghe nhạc theo Playlist.
* Tìm kiếm bài hát theo từ khóa.

## Quản trị viên

* Đăng nhập và đăng ký tài khoản quản trị.
* Quản lý (Thêm, Sửa, Xóa):

  * Bài hát
  * Ca sĩ
  * Album
  * Thể loại
  * Chủ đề
  * Playlist
  * Tài khoản
* Quản lý quan hệ:

  * Ca sĩ – Bài hát
  * Ca sĩ – Album
  * Playlist – Bài hát
* Tải lên hình ảnh và tệp nhạc (.mp3).

---

# 3. Cấu trúc thư mục

```text
webmusicASP/
├── progress-report/      # Báo cáo tiến độ
│   ├── BaoCaoTuan1.txt
│   ├── BaoCaoTuan2.txt
│   ├── BaoCaoTuan3.txt
│   ├── BaoCaoTuan4.txt
│   ├── BaoCaoTuan5.txt
│   └── BaoCaoTuan6.txt
│
├── setup/
│   └── csdl.sql          # Script tạo cơ sở dữ liệu
│
├── src/
│   ├── Nhom.sln
│   ├── Nhom/
│   │   ├── Models/
│   │   ├── Controllers/
│   │   ├── Views/
│   │   ├── audio/
│   │   ├── images/
│   │   ├── css/
│   │   └── js/
│   └── packages/
│
└── thesis/
    └── BaoCao-WebsiteNgheNhacTrucTuyen-DK24TT80161.pdf
```

---

# 4. Hướng dẫn cài đặt

## Yêu cầu

* Visual Studio 2019 trở lên
* ASP.NET & Web Development Workload
* Microsoft SQL Server (Express hoặc Standard)
* SQL Server Management Studio (SSMS)
* .NET Framework

## Các bước cài đặt

### Bước 1. Tải mã nguồn

```bash
git clone <repository-url>
cd webmusicASP
```

### Bước 2. Tạo cơ sở dữ liệu

* Mở **SQL Server Management Studio (SSMS)**.
* Mở file:

```text
setup/csdl.sql
```

* Chọn **Execute** để tạo cơ sở dữ liệu **Nhaccuatui** cùng dữ liệu mẫu.

### Bước 3. Cấu hình chuỗi kết nối

Mở project bằng Visual Studio.

Trong file:

```text
Web.config
```

chỉnh lại chuỗi kết nối:

```xml
NhaccuatuiConnectionString
```

cho phù hợp với tên SQL Server trên máy.

> **Lưu ý:** Một số Controller sử dụng chuỗi kết nối viết trực tiếp trong mã nguồn, vì vậy cần chỉnh lại `Data Source` tương ứng.

### Bước 4. Chạy chương trình

* Build Solution.
* Nhấn **F5** hoặc **Ctrl + F5** để chạy trên IIS Express.

---

# 5. Tài khoản thử nghiệm

| Vai trò       | Tài khoản | Mật khẩu   |
| ------------- | --------- | ---------- |
| Quản trị viên | **admin** | **123456** |

Tài khoản trên đã được tạo sẵn trong file:

```text
setup/csdl.sql
```

Có thể tạo thêm tài khoản mới thông qua chức năng **Đăng ký** của trang quản trị.

---

# 6. Báo cáo tiến độ

Các báo cáo tiến độ được lưu trong thư mục:

```text
progress-report/
```

| Tuần   | Thời gian               | Nội dung                                                |
| ------ | ----------------------- | ------------------------------------------------------- |
| Tuần 1 | 01/06/2026 – 07/06/2026 | Khảo sát và phân tích yêu cầu                           |
| Tuần 2 | 08/06/2026 – 14/06/2026 | Thiết kế cơ sở dữ liệu và ERD                           |
| Tuần 3 | 15/06/2026 – 21/06/2026 | Xây dựng Models và Controllers                          |
| Tuần 4 | 22/06/2026 – 28/06/2026 | Xây dựng chức năng quản trị (Back-end)                  |
| Tuần 5 | 29/06/2026 – 05/07/2026 | Xây dựng giao diện người dùng và chức năng phát nhạc    |
| Tuần 6 | 06/07/2026 – 09/07/2026 | Tìm kiếm, kiểm thử, hoàn thiện hệ thống và viết báo cáo |

---

# 7. Kết quả đạt được

Sau quá trình thực hiện, nhóm đã hoàn thành các nội dung sau:

* Thiết kế và xây dựng cơ sở dữ liệu trên Microsoft SQL Server.
* Xây dựng đầy đủ chức năng quản trị (CRUD).
* Hoàn thiện giao diện người dùng.
* Tích hợp chức năng phát nhạc trực tuyến bằng HTML5 Audio.
* Hỗ trợ tìm kiếm bài hát.
* Hoàn thiện báo cáo đồ án và tài liệu hướng dẫn cài đặt.

---

# 8. Hướng phát triển

Trong tương lai, hệ thống có thể được mở rộng với các chức năng:

* Đăng nhập bằng Google hoặc Facebook.
* Yêu thích bài hát.
* Bình luận và đánh giá.
* Bảng xếp hạng bài hát.
* Gợi ý nhạc theo sở thích.
* Chia sẻ Playlist.
* Phát nhạc nền liên tục.
* Giao diện Responsive hoàn chỉnh cho thiết bị di động.

---

**Đồ án Chuyên đề ASP.NET – Website Nghe Nhạc Trực Tuyến**
**Lớp:** DK24TT80161
**Giảng viên hướng dẫn:** TS. Đoàn Phước Miên
**Năm học:** 2026
