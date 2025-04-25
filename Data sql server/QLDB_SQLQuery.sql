USE QuanLyDanhBa
GO

-- Xóa khóa ngoại giữa LIENHE và NGUOIDUNG
ALTER TABLE LIENHE
DROP CONSTRAINT FK_LIENHE_NGUOIDUNG;

-- Xóa khóa ngoại giữa LICHSU và NGUOIDUNG
ALTER TABLE LICHSU
DROP CONSTRAINT FK_LICHSU_NGUOIDUNG;

-- Xóa khóa ngoại giữa CHITIETNHOM và LIENHE
ALTER TABLE CHITIETNHOM
DROP CONSTRAINT FK_LIENHE_CHITIETNHOM;

-- Xóa khóa ngoại giữa CHITIETNHOM và NHOM
ALTER TABLE CHITIETNHOM
DROP CONSTRAINT FK_NHOM_CHITIETNHOM;

-- Xóa khóa ngoại giữa SAOLUU và NGUOIDUNG
ALTER TABLE SAOLUU
DROP CONSTRAINT FK_SAOLUU_NGUOIDUNG;

DROP TABLE CHITIETNHOM;
DROP TABLE LIENHE;
DROP TABLE LICHSU;
DROP TABLE SAOLUU;
DROP TABLE NHOM;
DROP TABLE NGUOIDUNG;

--Bảng người dùng
CREATE TABLE NGUOIDUNG (
    MaNguoiDung INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15) NOT NULL CHECK (SDT LIKE '[0-9]%'),
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    PhanQuyen NVARCHAR(20) NOT NULL CHECK (PhanQuyen IN ('Admin', 'User', 'Guest'))
);
--Bảng liên hệ
CREATE TABLE LIENHE (
    MaLienHe INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15) NOT NULL CHECK (SDT LIKE '[0-9]%'),
	Email NVARCHAR(100),
	DiaChi NVARCHAR(255), 
	NgaySinh DATETIME,
	GhiChu NTEXT,

	MaNguoiDung INT
);

--Bảng nhóm
CREATE TABLE NHOM (
    MaNhom INT PRIMARY KEY,
    TenNhom NVARCHAR(100) NOT NULL,
	GhiChu NTEXT,
);

--Bảng lịch sử
CREATE TABLE LICHSU(
	MaLichSu INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	HanhDong NVARCHAR(50) NOT NULL,
	ThoiGian DATETIME,

	MaNguoiDung INT
);

--Bảng thành viên nhóm
CREATE TABLE CHITIETNHOM(
	MaLienHe INT,
	MaNhom INT,

	PRIMARY KEY(MaLienHe, MaNhom )
);

--Bảng sao lưu
CREATE TABLE SAOLUU (
    MaSaoLuu INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                      
    TenBanSao NVARCHAR(100) NOT NULL,              
    NgaySaoLuu DATETIME NOT NULL DEFAULT GETDATE(),
    LoaiSaoLuu NVARCHAR(50) NOT NULL,              
    DuongDan NVARCHAR(255) NOT NULL,  
	
    MaNguoiDung INT NOT NULL,                     
);


alter table LIENHE
add constraint FK_LIENHE_NGUOIDUNG
foreign key (MaNguoiDung)
references NGUOIDUNG (MaNguoiDung)

alter table LICHSU
add constraint FK_LICHSU_NGUOIDUNG
foreign key (MaNguoiDung)
references NGUOIDUNG (MaNguoiDung)

alter table CHITIETNHOM
add constraint FK_LIENHE_CHITIETNHOM
foreign key (MaLienHe)
references LIENHE (MaLienHe)

alter table CHITIETNHOM
add constraint FK_NHOM_CHITIETNHOM
foreign key (MaNhom)
references NHOM (MaNhom)

alter table SAOLUU
add constraint FK_SAOLUU_NGUOIDUNG 
foreign key (MaNguoiDung)
references NGUOIDUNG(MaNguoiDung)

-- Các bảng đã có: NGUOIDUNG, LIENHE, NHOM, LICHSU, CHITIETNHOM, SAOLUU
-- Dữ liệu mẫu cho NGUOIDUNG
INSERT INTO NGUOIDUNG (TenDangNhap, MatKhau, Email, HoTen, SDT, PhanQuyen)
VALUES 
('admin1', '123456', 'admin1@example.com', 'Admin One', '0911111111', 'Admin'),
('user1', '123456', 'user1@example.com', 'User One', '0922222222', 'User'),
('guest1', '123456', 'guest1@example.com', 'Guest One', '0933333333', 'Guest'),
('admin2', '123456', 'admin2@example.com', 'Admin Two', '0944444444', 'Admin'),
('user2', '123456', 'user2@example.com', 'User Two', '0955555555', 'User'),
('guest2', '123456', 'guest2@example.com', 'Guest Two', '0966666666', 'Guest'),
('admin3', '123456', 'admin3@example.com', 'Admin Three', '0977777777', 'Admin'),
('user3', '123456', 'user3@example.com', 'User Three', '0988888888', 'User'),
('guest3', '123456', 'guest3@example.com', 'Guest Three', '0999999999', 'Guest'),
('user4', '123456', 'user4@example.com', 'User Four', '0900000000', 'User');

-- Dữ liệu mẫu cho NHOM
INSERT INTO NHOM (MaNhom, TenNhom, GhiChu)
VALUES 
(1, N'Gia đình', N'Nhóm liên hệ gia đình'),
(2, N'Bạn bè', N'Nhóm bạn học'),
(3, N'Công việc', N'Liên hệ công việc'),
(4, N'Khách hàng', N'Nhóm khách hàng VIP'),
(5, N'Trường học', N'Bạn học cũ'),
(6, N'Cộng đồng', N'Câu lạc bộ địa phương'),
(7, N'Online', N'Bạn qua mạng'),
(8, N'Thể thao', N'Nhóm đá bóng'),
(9, N'Âm nhạc', N'Ban nhạc cũ'),
(10, N'Khác', N'Chưa phân loại');

-- Dữ liệu mẫu cho LIENHE
INSERT INTO LIENHE (HoTen, SDT, Email, DiaChi, NgaySinh, GhiChu, MaNguoiDung)
VALUES 
(N'Nguyễn Văn A', '0911111111', 'a@gmail.com', N'Hà Nội', '1990-01-01', N'Bạn học', 1),
(N'Trần Thị B', '0922222222', 'b@gmail.com', N'Hồ Chí Minh', '1991-02-02', N'Đồng nghiệp', 2),
(N'Lê Văn C', '0933333333', 'c@gmail.com', N'Đà Nẵng', '1992-03-03', N'Khách hàng', 3),
(N'Phạm Thị D', '0944444444', 'd@gmail.com', N'Hải Phòng', '1993-04-04', N'Bạn bè', 4),
(N'Đỗ Văn E', '0955555555', 'e@gmail.com', N'Cần Thơ', '1994-05-05', N'Gia đình', 5),
(N'Bùi Thị F', '0966666666', 'f@gmail.com', N'Huế', '1995-06-06', N'Khác', 6),
(N'Hoàng Văn G', '0977777777', 'g@gmail.com', N'Vinh', '1996-07-07', N'Thể thao', 7),
(N'Ngô Thị H', '0988888888', 'h@gmail.com', N'Quảng Ninh', '1997-08-08', N'Ban nhạc', 8),
(N'Dương Văn I', '0999999999', 'i@gmail.com', N'Bắc Ninh', '1998-09-09', N'Online friend', 9),
(N'Trịnh Thị J', '0900000000', 'j@gmail.com', N'Nam Định', '1999-10-10', N'Đồng nghiệp mới', 10);


-- Dữ liệu mẫu cho LICHSU
INSERT INTO LICHSU (HanhDong, ThoiGian, MaNguoiDung)
VALUES 
(N'Đăng nhập', GETDATE(), 1),
(N'Thêm liên hệ', GETDATE(), 2),
(N'Sửa liên hệ', GETDATE(), 3),
(N'Xóa liên hệ', GETDATE(), 4),
(N'Tạo nhóm', GETDATE(), 5),
(N'Thêm vào nhóm', GETDATE(), 6),
(N'Xuất file sao lưu', GETDATE(), 7),
(N'Khôi phục dữ liệu', GETDATE(), 8),
(N'Đăng xuất', GETDATE(), 9),
(N'Xem lịch sử', GETDATE(), 10);

-- Dữ liệu mẫu cho CHITIETNHOM
INSERT INTO CHITIETNHOM (MaLienHe, MaNhom)
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

-- Dữ liệu mẫu cho SAOLUU
INSERT INTO SAOLUU (TenBanSao, LoaiSaoLuu, DuongDan, MaNguoiDung)
VALUES 
('Backup_01', N'Toàn bộ', 'C:\\Backup\\backup1.bak', 1),
('Backup_02', N'Liên hệ', 'C:\\Backup\\backup2.bak', 2),
('Backup_03', N'Nhóm', 'C:\\Backup\\backup3.bak', 3),
('Backup_04', N'Toàn bộ', 'C:\\Backup\\backup4.bak', 4),
('Backup_05', N'Toàn bộ', 'C:\\Backup\\backup5.bak', 5),
('Backup_06', N'Liên hệ', 'C:\\Backup\\backup6.bak', 6),
('Backup_07', N'Nhóm', 'C:\\Backup\\backup7.bak', 7),
('Backup_08', N'Toàn bộ', 'C:\\Backup\\backup8.bak', 8),
('Backup_09', N'Toàn bộ', 'C:\\Backup\\backup9.bak', 9),
('Backup_10', N'Toàn bộ', 'C:\\Backup\\backup10.bak', 10);


