/* =====================================================================
   CSDL: Nhaccuatui - Website nghe nhac truc tuyen
   He quan tri: Microsoft SQL Server
   Ghi chu: Ten bang & cot khop voi ma nguon trong thu muc src/.
   Cach dung: Mo SSMS -> New Query -> chay toan bo file nay.
   ===================================================================== */

IF DB_ID('Nhaccuatui') IS NULL
    CREATE DATABASE [Nhaccuatui];
GO

USE [Nhaccuatui];
GO

/* ---------- Xoa bang cu (neu chay lai) ---------- */
IF OBJECT_ID('dbo.playlist_baihat','U') IS NOT NULL DROP TABLE dbo.playlist_baihat;
IF OBJECT_ID('dbo.casi_baihat','U')     IS NOT NULL DROP TABLE dbo.casi_baihat;
IF OBJECT_ID('dbo.casi_album','U')      IS NOT NULL DROP TABLE dbo.casi_album;
IF OBJECT_ID('dbo.BaiHat','U')          IS NOT NULL DROP TABLE dbo.BaiHat;
IF OBJECT_ID('dbo.Playlist','U')        IS NOT NULL DROP TABLE dbo.Playlist;
IF OBJECT_ID('dbo.Album','U')           IS NOT NULL DROP TABLE dbo.Album;
IF OBJECT_ID('dbo.ChuDe','U')           IS NOT NULL DROP TABLE dbo.ChuDe;
IF OBJECT_ID('dbo.CaSi','U')            IS NOT NULL DROP TABLE dbo.CaSi;
IF OBJECT_ID('dbo.TheLoai','U')         IS NOT NULL DROP TABLE dbo.TheLoai;
IF OBJECT_ID('dbo.TaiKhoan','U')        IS NOT NULL DROP TABLE dbo.TaiKhoan;
GO

/* ---------- Bang chinh ---------- */

-- Tai khoan quan tri
CREATE TABLE dbo.TaiKhoan (
    id            INT IDENTITY(1,1) PRIMARY KEY,
    tendangnhap   NVARCHAR(30)  NOT NULL,
    matkhau       NVARCHAR(10)  NULL
);
GO

-- The loai
CREATE TABLE dbo.TheLoai (
    matheloai     INT IDENTITY(1,1) PRIMARY KEY,
    tentheloai    NVARCHAR(50)  NOT NULL,
    hinhanh       NVARCHAR(50)  NULL
);
GO

-- Chu de
CREATE TABLE dbo.ChuDe (
    machude       INT IDENTITY(1,1) PRIMARY KEY,
    tenchude      NVARCHAR(50)   NOT NULL,
    motathem      NVARCHAR(1000) NULL,
    hinhanh       NVARCHAR(50)   NULL
);
GO

-- Album
CREATE TABLE dbo.Album (
    maalbum       INT IDENTITY(1,1) PRIMARY KEY,
    tenalbum      NVARCHAR(50)  NOT NULL,
    hinhanh       NVARCHAR(50)  NULL
);
GO

-- Ca si
CREATE TABLE dbo.CaSi (
    macasi        INT IDENTITY(1,1) PRIMARY KEY,
    tencasi       NVARCHAR(50)   NOT NULL,
    namsinh       INT            NULL,
    hinhanh       NVARCHAR(50)   NULL,
    quequan       NVARCHAR(30)   NULL,
    motathem      NVARCHAR(1000) NULL
);
GO

-- Bai hat (thu tu cot khop voi: insert into baihat values(...))
CREATE TABLE dbo.BaiHat (
    mabaihat      INT IDENTITY(1,1) PRIMARY KEY,
    tenbaihat     NVARCHAR(50)   NOT NULL,
    hinhanh       NVARCHAR(50)   NULL,
    loibaihat     NVARCHAR(1000) NULL,
    tacgia        NVARCHAR(50)   NULL,
    matheloai     INT            NOT NULL,
    maalbum       INT            NOT NULL,
    machude       INT            NOT NULL,
    linkbaihat    NVARCHAR(50)   NULL,
    CONSTRAINT FK_BaiHat_TheLoai FOREIGN KEY (matheloai) REFERENCES dbo.TheLoai(matheloai),
    CONSTRAINT FK_BaiHat_Album   FOREIGN KEY (maalbum)   REFERENCES dbo.Album(maalbum),
    CONSTRAINT FK_BaiHat_ChuDe   FOREIGN KEY (machude)   REFERENCES dbo.ChuDe(machude)
);
GO

-- Playlist (thu tu cot khop voi: insert into playlist values(...))
CREATE TABLE dbo.Playlist (
    maplaylist    INT IDENTITY(1,1) PRIMARY KEY,
    tenplaylist   NVARCHAR(50)  NOT NULL,
    hinhanh       NVARCHAR(50)  NOT NULL,
    matheloai     INT           NOT NULL,
    nguoitao      NVARCHAR(50)  NULL,
    CONSTRAINT FK_Playlist_TheLoai FOREIGN KEY (matheloai) REFERENCES dbo.TheLoai(matheloai)
);
GO

/* ---------- Bang trung gian (nhieu - nhieu) ---------- */

-- Ca si - Album
CREATE TABLE dbo.casi_album (
    id       INT IDENTITY(1,1) PRIMARY KEY,
    macasi   INT NOT NULL,
    maalbum  INT NOT NULL,
    CONSTRAINT FK_CaSiAlbum_CaSi  FOREIGN KEY (macasi)  REFERENCES dbo.CaSi(macasi),
    CONSTRAINT FK_CaSiAlbum_Album FOREIGN KEY (maalbum) REFERENCES dbo.Album(maalbum)
);
GO

-- Ca si - Bai hat
CREATE TABLE dbo.casi_baihat (
    id        INT IDENTITY(1,1) PRIMARY KEY,
    macasi    INT NOT NULL,
    mabaihat  INT NOT NULL,
    CONSTRAINT FK_CaSiBaiHat_CaSi   FOREIGN KEY (macasi)   REFERENCES dbo.CaSi(macasi),
    CONSTRAINT FK_CaSiBaiHat_BaiHat FOREIGN KEY (mabaihat) REFERENCES dbo.BaiHat(mabaihat)
);
GO

-- Playlist - Bai hat
CREATE TABLE dbo.playlist_baihat (
    id          INT IDENTITY(1,1) PRIMARY KEY,
    maplaylist  INT NOT NULL,
    mabaihat    INT NOT NULL,
    CONSTRAINT FK_PLBH_Playlist FOREIGN KEY (maplaylist) REFERENCES dbo.Playlist(maplaylist),
    CONSTRAINT FK_PLBH_BaiHat   FOREIGN KEY (mabaihat)   REFERENCES dbo.BaiHat(mabaihat)
);
GO

/* =====================================================================
   DU LIEU MAU (seed data)
   ===================================================================== */

-- Tai khoan quan tri mac dinh
INSERT INTO dbo.TaiKhoan (tendangnhap, matkhau) VALUES
(N'admin', N'123456');
GO

-- The loai
INSERT INTO dbo.TheLoai (tentheloai, hinhanh) VALUES
(N'Nhac tre',   N'theloai/nhactre.jpg'),
(N'Ballad',     N'theloai/ballad.jpg'),
(N'Rap',        N'theloai/rap.jpg');
GO

-- Chu de
INSERT INTO dbo.ChuDe (tenchude, motathem, hinhanh) VALUES
(N'Tam trang', N'Nhung bai hat nhe nhang, sau lang', N'chude/tamtrang.jpg'),
(N'Soi dong',  N'Nhac soi dong, tiec tung',          N'chude/soidong.jpg');
GO

-- Album
INSERT INTO dbo.Album (tenalbum, hinhanh) VALUES
(N'Album Vol.1', N'album/vol1.jpg'),
(N'Single Hits', N'album/single.jpg');
GO

-- Ca si
INSERT INTO dbo.CaSi (tencasi, namsinh, hinhanh, quequan, motathem) VALUES
(N'MIN',        1997, N'casi/min.jpg',       N'Ha Noi',   N'Ca si nhac tre'),
(N'Mr. A',      1990, N'casi/mra.jpg',       N'TP.HCM',   N'Rapper'),
(N'Thanh Hung', 1995, N'casi/thanhhung.jpg', N'Nghe An',  N'Ca si ballad');
GO

-- Bai hat (matheloai, maalbum, machude tham chieu du lieu o tren)
INSERT INTO dbo.BaiHat (tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat) VALUES
(N'Co Em Cho',            N'baihat/coemcho.jpg', N'...', N'MIN, Mr. A',   1, 1, 2, N'audio/Co-Em-Cho-MIN-Mr-A.mp3'),
(N'Tim',                  N'baihat/tim.jpg',     N'...', N'MIN, Mr. A',   1, 1, 1, N'audio/Tim-MIN-Mr-A.mp3'),
(N'Sai Nguoi Sai Thoi Diem', N'baihat/snstd.jpg', N'...', N'Thanh Hung', 2, 2, 1, N'audio/Sai-Nguoi-Sai-Thoi-Diem-Thanh-Hung.mp3');
GO

-- Playlist
INSERT INTO dbo.Playlist (tenplaylist, hinhanh, matheloai, nguoitao) VALUES
(N'Chill mỗi ngày', N'playlist/chill.jpg', 2, N'admin');
GO

-- Quan he mau
INSERT INTO dbo.casi_album  (macasi, maalbum)   VALUES (1,1),(2,1),(3,2);
INSERT INTO dbo.casi_baihat (macasi, mabaihat)  VALUES (1,1),(2,1),(1,2),(2,2),(3,3);
INSERT INTO dbo.playlist_baihat (maplaylist, mabaihat) VALUES (1,1),(1,2),(1,3);
GO

PRINT N'>> Tao CSDL Nhaccuatui va du lieu mau thanh cong!';
GO
