USE QuanLyDanhBa
GO

--Bảng người dùng
CREATE TABLE NGUOIDUNG (
    MaNguoiDung INT PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    HoTen NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15) NOT NULL CHECK (SDT LIKE '[0-9]%'),
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    PhanQuyen NVARCHAR(20) NOT NULL CHECK (PhanQuyen IN ('Admin', 'User', 'Guest'))
);
--Bảng liên hệ
CREATE TABLE LIENHE (
    MaLienHe INT PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15) NOT NULL CHECK (SDT LIKE '[0-9]%'),
	Email NVARCHAR(100) UNIQUE,
	DiaChi NVARCHAR(255), 
	NgaySinh DATETIME,
	GhiChu TEXT,

	MaNguoiDung INT
);

--Bảng nhóm
CREATE TABLE NHOM (
    MaNhom INT PRIMARY KEY,
    TenNhom NVARCHAR(100) NOT NULL,
	GhiChu TEXT,
);

--Bảng lịch sử
CREATE TABLE LICHSU(
	MaLichSu INT PRIMARY KEY,
	HanhDong NVARCHAR(50) NOT NULL,
	ThoiGian DATETIME,

	MaNguoiDung INT
);

--Bảng thành viên nhóm
CREATE TABLE CHITIETNHOM(
	MaLienHe INT,
	MaNguoiDung INT,
	MaNhom INT,

	PRIMARY KEY(MaLienHe, MaNguoiDung, MaNhom )
);

--Bảng sao lưu
CREATE TABLE SAOLUU (
    MaSaoLuu INT PRIMARY KEY,                      
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
add constraint FK_NGUOIDUNG_CHITIETNHOM
foreign key (MaNguoiDung)
references NGUOIDUNG (MaNguoiDung)

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
INSERT INTO NGUOIDUNG (MaNguoiDung, TenDangNhap, MatKhau, Email, HoTen, SDT, PhanQuyen)
VALUES 
(1, 'admin1', '123456', 'admin1@example.com', 'Admin One', '0911111111', 'Admin'),
(2, 'user1', '123456', 'user1@example.com', 'User One', '0922222222', 'User'),
(3, 'guest1', '123456', 'guest1@example.com', 'Guest One', '0933333333', 'Guest'),
(4, 'admin2', '123456', 'admin2@example.com', 'Admin Two', '0944444444', 'Admin'),
(5, 'user2', '123456', 'user2@example.com', 'User Two', '0955555555', 'User'),
(6, 'guest2', '123456', 'guest2@example.com', 'Guest Two', '0966666666', 'Guest'),
(7, 'admin3', '123456', 'admin3@example.com', 'Admin Three', '0977777777', 'Admin'),
(8, 'user3', '123456', 'user3@example.com', 'User Three', '0988888888', 'User'),
(9, 'guest3', '123456', 'guest3@example.com', 'Guest Three', '0999999999', 'Guest'),
(10, 'user4', '123456', 'user4@example.com', 'User Four', '0900000000', 'User');

-- Dữ liệu mẫu cho NHOM
INSERT INTO NHOM (MaNhom, TenNhom, GhiChu)
VALUES 
(1, 'Gia đình', 'Nhóm liên hệ gia đình'),
(2, 'Bạn bè', 'Nhóm bạn học'),
(3, 'Công việc', 'Liên hệ công việc'),
(4, 'Khách hàng', 'Nhóm khách hàng VIP'),
(5, 'Trường học', 'Bạn học cũ'),
(6, 'Cộng đồng', 'Câu lạc bộ địa phương'),
(7, 'Online', 'Bạn qua mạng'),
(8, 'Thể thao', 'Nhóm đá bóng'),
(9, 'Âm nhạc', 'Ban nhạc cũ'),
(10, 'Khác', 'Chưa phân loại');

-- Dữ liệu mẫu cho LIENHE
INSERT INTO LIENHE (MaLienHe, HoTen, SDT, Email, DiaChi, NgaySinh, GhiChu, MaNguoiDung)
VALUES 
(1, 'Nguyễn Văn A', '0911111111', 'a@gmail.com', 'Hà Nội', '1990-01-01', 'Bạn học', 1),
(2, 'Trần Thị B', '0922222222', 'b@gmail.com', 'Hồ Chí Minh', '1991-02-02', 'Đồng nghiệp', 2),
(3, 'Lê Văn C', '0933333333', 'c@gmail.com', 'Đà Nẵng', '1992-03-03', 'Khách hàng', 3),
(4, 'Phạm Thị D', '0944444444', 'd@gmail.com', 'Hải Phòng', '1993-04-04', 'Bạn bè', 4),
(5, 'Đỗ Văn E', '0955555555', 'e@gmail.com', 'Cần Thơ', '1994-05-05', 'Gia đình', 5),
(6, 'Bùi Thị F', '0966666666', 'f@gmail.com', 'Huế', '1995-06-06', 'Khác', 6),
(7, 'Hoàng Văn G', '0977777777', 'g@gmail.com', 'Vinh', '1996-07-07', 'Thể thao', 7),
(8, 'Ngô Thị H', '0988888888', 'h@gmail.com', 'Quảng Ninh', '1997-08-08', 'Ban nhạc', 8),
(9, 'Dương Văn I', '0999999999', 'i@gmail.com', 'Bắc Ninh', '1998-09-09', 'Online friend', 9),
(10, 'Trịnh Thị J', '0900000000', 'j@gmail.com', 'Nam Định', '1999-10-10', 'Đồng nghiệp mới', 10);

-- Dữ liệu mẫu cho LICHSU
INSERT INTO LICHSU (MaLichSu, HanhDong, ThoiGian, MaNguoiDung)
VALUES 
(1, 'Đăng nhập', GETDATE(), 1),
(2, 'Thêm liên hệ', GETDATE(), 2),
(3, 'Sửa liên hệ', GETDATE(), 3),
(4, 'Xóa liên hệ', GETDATE(), 4),
(5, 'Tạo nhóm', GETDATE(), 5),
(6, 'Thêm vào nhóm', GETDATE(), 6),
(7, 'Xuất file sao lưu', GETDATE(), 7),
(8, 'Khôi phục dữ liệu', GETDATE(), 8),
(9, 'Đăng xuất', GETDATE(), 9),
(10, 'Xem lịch sử', GETDATE(), 10);

-- Dữ liệu mẫu cho CHITIETNHOM
INSERT INTO CHITIETNHOM (MaLienHe, MaNguoiDung, MaNhom)
VALUES 
(1, 1, 1),
(2, 2, 2),
(3, 3, 3),
(4, 4, 4),
(5, 5, 5),
(6, 6, 6),
(7, 7, 7),
(8, 8, 8),
(9, 9, 9),
(10, 10, 10);

-- Dữ liệu mẫu cho SAOLUU
INSERT INTO SAOLUU (MaSaoLuu, TenBanSao, LoaiSaoLuu, DuongDan, MaNguoiDung)
VALUES 
(1, 'Backup_01', 'Toàn bộ', 'C:\\Backup\\backup1.bak', 1),
(2, 'Backup_02', 'Liên hệ', 'C:\\Backup\\backup2.bak', 2),
(3, 'Backup_03', 'Nhóm', 'C:\\Backup\\backup3.bak', 3),
(4, 'Backup_04', 'Toàn bộ', 'C:\\Backup\\backup4.bak', 4),
(5, 'Backup_05', 'Toàn bộ', 'C:\\Backup\\backup5.bak', 5),
(6, 'Backup_06', 'Liên hệ', 'C:\\Backup\\backup6.bak', 6),
(7, 'Backup_07', 'Nhóm', 'C:\\Backup\\backup7.bak', 7),
(8, 'Backup_08', 'Toàn bộ', 'C:\\Backup\\backup8.bak', 8),
(9, 'Backup_09', 'Toàn bộ', 'C:\\Backup\\backup9.bak', 9),
(10, 'Backup_10', 'Toàn bộ', 'C:\\Backup\\backup10.bak', 10);

