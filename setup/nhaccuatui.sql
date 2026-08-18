/* =====================================================================
   CSDL: Nhaccuatui - Website nghe nhac truc tuyen (SQL Server)
   ASP.NET Core 8 MVC + ADO.NET. Ten bang & cot khup voi ma nguon src/WebMusic/.
   Mat khau luu trong cot matkhau nvarchar(100).
   Cach dung: sqlcmd -S . -i setup/nhaccuatui.sql  (hoac mo SSMS -> chay toan bo).
   ===================================================================== */

USE master;
GO
IF DB_ID('Nhaccuatui') IS NOT NULL
    ALTER DATABASE Nhaccuatui SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
IF DB_ID('Nhaccuatui') IS NOT NULL
    DROP DATABASE Nhaccuatui;
CREATE DATABASE Nhaccuatui;
GO
USE Nhaccuatui;
GO

CREATE TABLE theloai (
    matheloai  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tentheloai NVARCHAR(50)  NOT NULL,
    hinhanh    NVARCHAR(255)  NULL
);
GO

CREATE TABLE chude (
    machude   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenchude  NVARCHAR(50)   NOT NULL,
    motathem  NVARCHAR(1000) NULL,
    hinhanh   NVARCHAR(255)   NULL
);
GO

CREATE TABLE album (
    maalbum   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenalbum  NVARCHAR(50)  NOT NULL,
    hinhanh   NVARCHAR(255)  NULL
);
GO

CREATE TABLE casi (
    macasi    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tencasi   NVARCHAR(50)   NOT NULL,
    namsinh   INT            NULL,
    hinhanh   NVARCHAR(255)   NULL,
    quequan   NVARCHAR(30)   NULL,
    motathem  NVARCHAR(1000) NULL
);
GO

CREATE TABLE baihat (
    mabaihat    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenbaihat   NVARCHAR(50)   NOT NULL,
    hinhanh     NVARCHAR(255)   NULL,
    loibaihat   NVARCHAR(1000) NULL,
    tacgia      NVARCHAR(50)   NULL,
    matheloai   INT NOT NULL,
    maalbum     INT NOT NULL,
    machude     INT NOT NULL,
    linkbaihat  NVARCHAR(100)  NULL,
    luotnghe    INT NOT NULL DEFAULT 0,
    duration    INT NOT NULL DEFAULT 0,
    CONSTRAINT FK_BaiHat_TheLoai FOREIGN KEY (matheloai) REFERENCES theloai(matheloai),
    CONSTRAINT FK_BaiHat_Album   FOREIGN KEY (maalbum)   REFERENCES album(maalbum),
    CONSTRAINT FK_BaiHat_ChuDe   FOREIGN KEY (machude)   REFERENCES chude(machude)
);
GO

CREATE TABLE playlist (
    maplaylist  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenplaylist NVARCHAR(50) NOT NULL,
    hinhanh     NVARCHAR(255) NOT NULL,
    matheloai   INT NOT NULL,
    nguoitao    NVARCHAR(50) NULL,
    CONSTRAINT FK_Playlist_TheLoai FOREIGN KEY (matheloai) REFERENCES theloai(matheloai)
);
GO

CREATE TABLE casi_album (
    id       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    macasi   INT NOT NULL,
    maalbum  INT NOT NULL,
    CONSTRAINT FK_CaSiAlbum_CaSi  FOREIGN KEY (macasi)  REFERENCES casi(macasi)  ON DELETE CASCADE,
    CONSTRAINT FK_CaSiAlbum_Album FOREIGN KEY (maalbum) REFERENCES album(maalbum) ON DELETE CASCADE
);
GO

CREATE TABLE casi_baihat (
    id        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    macasi    INT NOT NULL,
    mabaihat  INT NOT NULL,
    CONSTRAINT FK_CaSiBaiHat_CaSi   FOREIGN KEY (macasi)   REFERENCES casi(macasi)   ON DELETE CASCADE,
    CONSTRAINT FK_CaSiBaiHat_BaiHat FOREIGN KEY (mabaihat) REFERENCES baihat(mabaihat) ON DELETE CASCADE
);
GO

CREATE TABLE playlist_baihat (
    id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    maplaylist  INT NOT NULL,
    mabaihat    INT NOT NULL,
    CONSTRAINT FK_PLBH_Playlist FOREIGN KEY (maplaylist) REFERENCES playlist(maplaylist) ON DELETE CASCADE,
    CONSTRAINT FK_PLBH_BaiHat   FOREIGN KEY (mabaihat)   REFERENCES baihat(mabaihat)     ON DELETE CASCADE
);
GO

-- tao truoc yeuthich
CREATE TABLE taikhoan (
    id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tendangnhap  NVARCHAR(30)  NOT NULL,
    matkhau      NVARCHAR(100) NULL,
    vaitro       NVARCHAR(20)  NOT NULL DEFAULT 'User'
);
GO

CREATE TABLE yeuthich (
    id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    mataikhoan  INT NOT NULL,
    mabaihat    INT NOT NULL,
    CONSTRAINT UQ_YT UNIQUE (mataikhoan, mabaihat),
    CONSTRAINT FK_YT_TaiKhoan FOREIGN KEY (mataikhoan) REFERENCES taikhoan(id) ON DELETE CASCADE,
    CONSTRAINT FK_YT_BaiHat   FOREIGN KEY (mabaihat)   REFERENCES baihat(mabaihat) ON DELETE CASCADE
);
GO

INSERT INTO theloai (tentheloai, hinhanh) VALUES
(N'Nhạc trẻ',         'pic4.png'),
(N'Nhạc trẻ tình',    'pic1.jpg'),
(N'Pop Việt',         'pic2.jpg'),
(N'Rap Việt',         'pic3.jpg');
GO

INSERT INTO chude (tenchude, motathem, hinhanh) VALUES
(N'Hot V-Pop',         N'Những bài hát V-Pop hot nhất hiện tại', 'pic1.jpg'),
(N'Bài hát yêu thích', N'Tuyển tập bài hát được yêu thích',     'pic2.jpg'),
(N'Tình ca bolero',    N'Bolero trữ tình',                      'pic3.jpg');
GO

INSERT INTO album (tenalbum, hinhanh) VALUES
(N'Sai người sai thời điểm', 'pic7.jpg'),
(N'Cảm em chờ',              'pic2.jpg'),
(N'Tìm',                     'pic3.jpg'),
(N'Không lối về',            'pic4.jpg'),
(N'Chúng ta của hiện tại',   'pic5.jpg');
GO

INSERT INTO casi (tencasi, namsinh, hinhanh, quequan, motathem) VALUES
(N'Thanh Hùng',    1992, '1.jpg', N'Hải Dương', N'Ca sĩ trẻ thể loại pop ballad.'),
(N'MIN',           1988, '2.jpg', N'Hà Nội',    N'Ca sĩ tự sáng tác, indie pop.'),
(N'Erik',          1997, '3.webp', N'Hà Nội',   N'Ca sĩ V-Pop, ballad và dance.'),
(N'Sơn Tùng M-TP', 1994, '4.jpg', N'Hải Phòng', N'Ca sĩ R&B, V-Pop hàng đầu.');
GO

-- linkbaihat phai trung khop voi file mp3 thuc te trong wwwroot/audio
INSERT INTO baihat (tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration) VALUES
(N'Sai người sai thời điểm', 'pic1.jpg',
 N'Chuyện tình yêu lúc nào cũng thế, đi mãi bao năm lê thê mong tìm được ai. Sai người sai thời điểm, để rồi mất nhau...',
 N'Thanh Hùng', 1, 1, 1, 'Sai-Nguoi-Sai-Thoi-Diem-Thanh-Hung.mp3', 1280, 245),
(N'Cảm em chờ', 'pic2.jpg',
 N'Cảm em chờ, cảm em đã chờ, chờ một người chưa từng quay về. Cảm em nhớ, cảm em đã nhớ...',
 N'MIN', 2, 2, 1, 'Co-Em-Cho-MIN-Mr-A.mp3', 940, 198),
(N'Tìm', 'pic3.jpg',
 N'Tìm một bờ vai, tìm một vòng tay, tìm nơi bình yên để gác những âu lo.',
 N'MIN', 4, 3, 2, 'Tim-MIN-Mr-A.mp3', 765, 212),
(N'Chờ', 'pic4.jpg',
 N'Anh vẫn chờ, dẫu biết em không về.',
 N'Thanh Hùng', 1, 1, 1, 'Cho-Thanh-Hung.mp3', 320, 180),
(N'Không lối về', 'pic4.jpg',
 N'Không lối về, anh và em đã hết, chỉ còn lại những kỷ niệm.',
 N'Erik', 1, 4, 1, 'Khong-Loi-Ve-Erik.mp3', 650, 235),
(N'Chúng ta của hiện tại', 'pic5.jpg',
 N'Chúng ta của hiện tại, em bên ai rồi, anh vẫn đây.',
 N'Sơn Tùng M-TP', 3, 5, 1, 'Chung-Ta-Cua-Hien-Tai-Son-Tung-M-TP.mp3', 2100, 268),
(N'Muộn rồi mà sao còn', 'pic5.jpg',
 N'Muộn rồi mà sao còn, anh vẫn chờ em.',
 N'Sơn Tùng M-TP', 4, 5, 2, 'Muon-Roi-Ma-Sao-Con-Son-Tung-M-TP.mp3', 1800, 255),
(N'Hãy trao cho anh', 'pic5.jpg',
 N'Hãy trao cho anh, một lần cuối...',
 N'Sơn Tùng M-TP', 3, 5, 3, 'Hay-Trao-Cho-Anh-Son-Tung-M-TP.mp3', 540, 220);
GO

INSERT INTO playlist (tenplaylist, hinhanh, matheloai, nguoitao) VALUES
(N'Nhạc Việt hot',      'pic1.jpg', 1, N'V.A'),
(N'Bài hát yêu thích',  'pic2.jpg', 2, N'V.A'),
(N'Indie Việt',         'pic3.jpg', 3, N'MIN');
GO

-- 1=Thanh Hung -> 1,4 ; 2=MIN -> 2,3 ; 3=Erik -> 5 ; 4=Son Tung -> 6,7,8
INSERT INTO casi_baihat (macasi, mabaihat) VALUES
(1, 1), (1, 4), (2, 2), (2, 3), (3, 5), (4, 6), (4, 7), (4, 8);
GO

-- 1->album1 ; 2->album2,3 ; 3->album4 ; 4->album5
INSERT INTO casi_album (macasi, maalbum) VALUES
(1, 1), (2, 2), (2, 3), (3, 4), (4, 5);
GO

INSERT INTO playlist_baihat (maplaylist, mabaihat) VALUES
(1, 1), (1, 2), (1, 3), (1, 5),
(2, 1), (2, 4), (2, 6),
(3, 2), (3, 3), (3, 7);
GO

-- admin/123, huyen/123456
INSERT INTO taikhoan (tendangnhap, matkhau, vaitro) VALUES
('admin', '123', 'Admin'),
('huyen', '123456', 'User');
GO

PRINT N'>> Tao CSDL Nhaccuatui va du lieu mau thanh cong!';
GO