/* =====================================================================
   CSDL: Nhaccuatui - Website nghe nhac truc tuyen
   He quan tri: Microsoft SQL Server
   Phien ban: ASP.NET Core 8 MVC + ADO.NET
   Ghi chu: Ten bang & cot khop voi ma nguon trong src/WebMusic/.
            Mat khau luu dang BCrypt hash (cot matkhau nvarchar(100)).
   Cach dung:
     sqlcmd -S . -i setup/csdl.sql
     hoac mo SSMS -> New Query -> chay toan bo file nay.
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

/* ---------- Bang chinh ---------- */

-- The loai
CREATE TABLE theloai (
    matheloai  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tentheloai NVARCHAR(50)  NOT NULL,
    hinhanh    NVARCHAR(50)  NULL
);
GO

-- Chu de
CREATE TABLE chude (
    machude   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenchude  NVARCHAR(50)   NOT NULL,
    motathem  NVARCHAR(1000) NULL,
    hinhanh   NVARCHAR(50)   NULL
);
GO

-- Album
CREATE TABLE album (
    maalbum   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenalbum  NVARCHAR(50)  NOT NULL,
    hinhanh   NVARCHAR(50)  NULL
);
GO

-- Ca si
CREATE TABLE casi (
    macasi    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tencasi   NVARCHAR(50)   NOT NULL,
    namsinh   INT            NULL,
    hinhanh   NVARCHAR(50)   NULL,
    quequan   NVARCHAR(30)   NULL,
    motathem  NVARCHAR(1000) NULL
);
GO

-- Bai hat
CREATE TABLE baihat (
    mabaihat    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenbaihat   NVARCHAR(50)   NOT NULL,
    hinhanh     NVARCHAR(50)   NULL,
    loibaihat   NVARCHAR(1000) NULL,
    tacgia      NVARCHAR(50)   NULL,
    matheloai   INT NOT NULL,
    maalbum     INT NOT NULL,
    machude     INT NOT NULL,
    linkbaihat  NVARCHAR(100)  NULL,
    CONSTRAINT FK_BaiHat_TheLoai FOREIGN KEY (matheloai) REFERENCES theloai(matheloai),
    CONSTRAINT FK_BaiHat_Album   FOREIGN KEY (maalbum)   REFERENCES album(maalbum),
    CONSTRAINT FK_BaiHat_ChuDe   FOREIGN KEY (machude)   REFERENCES chude(machude)
);
GO

-- Playlist
CREATE TABLE playlist (
    maplaylist  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tenplaylist NVARCHAR(50) NOT NULL,
    hinhanh     NVARCHAR(50) NOT NULL,
    matheloai   INT NOT NULL,
    nguoitao    NVARCHAR(50) NULL,
    CONSTRAINT FK_Playlist_TheLoai FOREIGN KEY (matheloai) REFERENCES theloai(matheloai)
);
GO

/* ---------- Bang trung gian (nhieu - nhieu) ---------- */

-- Ca si - Album
CREATE TABLE casi_album (
    id       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    macasi   INT NOT NULL,
    maalbum  INT NOT NULL,
    CONSTRAINT FK_CaSiAlbum_CaSi  FOREIGN KEY (macasi)  REFERENCES casi(macasi)  ON DELETE CASCADE,
    CONSTRAINT FK_CaSiAlbum_Album FOREIGN KEY (maalbum) REFERENCES album(maalbum) ON DELETE CASCADE
);
GO

-- Ca si - Bai hat
CREATE TABLE casi_baihat (
    id        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    macasi    INT NOT NULL,
    mabaihat  INT NOT NULL,
    CONSTRAINT FK_CaSiBaiHat_CaSi   FOREIGN KEY (macasi)   REFERENCES casi(macasi)   ON DELETE CASCADE,
    CONSTRAINT FK_CaSiBaiHat_BaiHat FOREIGN KEY (mabaihat) REFERENCES baihat(mabaihat) ON DELETE CASCADE
);
GO

-- Playlist - Bai hat
CREATE TABLE playlist_baihat (
    id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    maplaylist  INT NOT NULL,
    mabaihat    INT NOT NULL,
    CONSTRAINT FK_PLBH_Playlist FOREIGN KEY (maplaylist) REFERENCES playlist(maplaylist) ON DELETE CASCADE,
    CONSTRAINT FK_PLBH_BaiHat   FOREIGN KEY (mabaihat)   REFERENCES baihat(mabaihat)     ON DELETE CASCADE
);
GO

-- Tai khoan (matkhau nvarchar(100) chua hash BCrypt)
CREATE TABLE taikhoan (
    id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    tendangnhap  NVARCHAR(30)  NOT NULL,
    matkhau      NVARCHAR(100) NULL
);
GO

/* =====================================================================
   DU LIEU MAU (seed data)
   File nhac + anh nam trong src/WebMusic/wwwroot/audio va wwwroot/images
   ===================================================================== */

-- The loai
INSERT INTO theloai (tentheloai, hinhanh) VALUES
(N'Nhạc trẻ',         'pic4.png'),
(N'Nhạc trẻ tình',    'pic1.jpg'),
(N'Pop Việt',         'pic2.jpg'),
(N'Rap Việt',         'pic3.jpg');
GO

-- Chu de
INSERT INTO chude (tenchude, motathem, hinhanh) VALUES
(N'Hot V-Pop',         N'Những bài hát V-Pop hot nhất hiện tại', 'pic1.jpg'),
(N'Bài hát yêu thích', N'Tuyển tập bài hát được yêu thích',     'pic2.jpg'),
(N'Tình ca bolero',    N'Bolero trữ tình',                      'pic3.jpg');
GO

-- Album
INSERT INTO album (tenalbum, hinhanh) VALUES
(N'Sai người sai thời điểm', 'pic7.jpg'),
(N'Cảm em chờ',              'pic2.jpg'),
(N'Tìm',                     'pic3.jpg');
GO

-- Ca si
INSERT INTO casi (tencasi, namsinh, hinhanh, quequan, motathem) VALUES
(N'Thanh Hùng', 1992, '1.jpg', N'Hải Dương', N'Ca sĩ trẻ thể loại pop ballad.'),
(N'MIN',        1988, '2.jpg', N'Hà Nội',   N'Ca sĩ tự sáng tác, indie pop.');
GO

-- Bai hat (4 bai, 3 bai co file mp3 trong wwwroot/audio)
INSERT INTO baihat (tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat) VALUES
(N'Sai người sai thời điểm', 'pic1.jpg',
 N'Chuyện tình yêu lúc nào cũng thế, đi mãi bao năm lê thê mong tìm được ai. Sai người sai thời điểm, để rồi mất nhau...',
 N'Thanh Hùng', 1, 1, 1, 'Sai-Nguoi-Sai-Thoi-Diem-Thanh-Hung.mp3'),
(N'Cảm em chờ', 'pic2.jpg',
 N'Cảm em chờ, cảm em đã chờ, chờ một người chưa từng quay về. Cảm em nhớ, cảm em đã nhớ...',
 N'MIN', 2, 2, 1, 'Co-Em-Cho-MIN-Mr-A.mp3'),
(N'Tìm', 'pic3.jpg',
 N'Tìm một bờ vai, tìm một vòng tay, tìm nơi bình yên để gác những âu lo.',
 N'MIN', 4, 3, 2, 'Tim-MIN-Mr-A.mp3'),
(N'Chờ', 'pic4.jpg',
 N'Anh vẫn chờ, dẫu biết em không về.',
 N'Thanh Hùng', 1, 1, 1, NULL);
GO

-- Playlist
INSERT INTO playlist (tenplaylist, hinhanh, matheloai, nguoitao) VALUES
(N'Nhạc Việt hot',      'pic1.jpg', 1, N'V.A'),
(N'Bài hát yêu thích',  'pic2.jpg', 2, N'V.A'),
(N'Indie Việt',         'pic3.jpg', 3, N'MIN');
GO

-- Junction CaSi <-> BaiHat
--   macasi 1 = Thanh Hung -> bai 1, 4 ; macasi 2 = MIN -> bai 2, 3
INSERT INTO casi_baihat (macasi, mabaihat) VALUES
(1, 1), (1, 4), (2, 2), (2, 3);
GO

-- Junction CaSi <-> Album
--   macasi 1 -> album 1 ; macasi 2 -> album 2, 3
INSERT INTO casi_album (macasi, maalbum) VALUES
(1, 1), (2, 2), (2, 3);
GO

-- Junction Playlist <-> BaiHat
INSERT INTO playlist_baihat (maplaylist, mabaihat) VALUES
(1, 1), (1, 2), (1, 3),
(2, 1), (2, 4),
(3, 2), (3, 3);
GO

-- Tai khoan (BCrypt hash, workFactor 11)
--   admin / 123
--   huyen / 123456
INSERT INTO taikhoan (tendangnhap, matkhau) VALUES
('admin',
 '$2a$11$PaMQhBC0Ulw4Je7ak8XLZuGF8U9CNlxXCQFhuOoEU2m3e/dIvn31S'),
('huyen',
 '$2a$11$KW0i11JHl1oDSp7HWjuTXuiRAQ92URwTtKulBZE4r2EHoeH6yi6JO');
GO

PRINT N'>> Tao CSDL Nhaccuatui va du lieu mau thanh cong!';
GO