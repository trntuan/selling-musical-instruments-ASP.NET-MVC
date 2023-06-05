CREATE DATABASE QLCHNC
Go
USE QLCHNC

GO
CREATE TABLE LoaiNhacCu (
  MaLoaiNC varchar(10) primary key,
  TenLoaiNC nvarchar(50) NOT NULL,
  AnhLoaiNC nvarchar(100)
) 
GO


CREATE TABLE HangSanXuat (
  MaHSX varchar(10) primary key,
  TenHSX nvarchar(100) NOT NULL,
  DiaChi nvarchar(500) NOT NULL,
  SDT varchar(10) NOT NULL
)
GO
CREATE TABLE NhaCungCap (
  MaNCC varchar(10) primary key,
  TenNCC nvarchar(100) NOT NULL,
  SDT varchar(10) NOT NULL,
  DiaChi nvarchar(500) NOT NULL,
  Email varchar(50)
)
go
CREATE TABLE NhacCu (
  MaNC varchar(10) primary key,
  TenNC nvarchar(100) NOT NULL,
  MoTa nvarchar(4000) NOT NULL,
  AnhNC nvarchar(100) NOT NULL, 
  SoLuong int NOT NULL,
  ThoiGianBaoHanh nvarchar(50) ,
  DonGia int NOT NULL,
  MaLoaiNC varchar(10) foreign key references LoaiNhacCu(MaLoaiNC),
  MaNCC varchar(10) foreign key references NhaCungCap(MaNCC),
  MaHSX varchar(10) foreign key references HangSanXuat(MaHSX)
)

GO
CREATE TABLE ChucVu (
  MaCV varchar(10) primary key,
  TenCV nvarchar(50) NOT NULL
)
GO
CREATE TABLE NhanVien (
  MaNV varchar(10) primary key,
  HoTenNV nvarchar(100) NOT NULL,
  SDT varchar(10) NOT NULL,
  Email varchar(50) NOT NULL,
  DiaChi nvarchar(500) NOT NULL,
  TenDN varchar(30) NOT NULL,
  MatKhau varchar(100) NOT NULL,
  MaCV varchar(10) foreign key references ChucVu(MaCV)
)

GO
CREATE TABLE KhachHang (
  MaKH varchar(10) primary key,
  HoTenKH nvarchar(100) NOT NULL,
  SDT varchar(10) ,
  Email varchar(50) NOT NULL,
  DiaChi nvarchar(500) ,
  TaiKhoan varchar(30) ,
  MatKhau varchar(100) NOT NULL,
  AnhKH nvarchar(100)
)

GO
CREATE TABLE HoaDon (
  MaHD varchar(10) primary key,
  NgayDH date NOT NULL,
  NgayGH date DEFAULT NULL,
  MaKH varchar(10)  foreign key references KhachHang(MaKH),
  MaNV varchar(10) foreign key references NhanVien(MaNV),
  TongTien int ,
  TinhTrangDuyet bit NOT NULL,
  TinhTrangDonHang bit NOT NULL
)
GO
CREATE TABLE  CTHoaDon (
  MaHD varchar(10) foreign key references HoaDon(MaHD) ,
  MaNC varchar(10) foreign key references NhacCu(MaNC),
  SoLuongBan int NOT NULL,
  DonGiaBan int NOT NULL
  primary key(MaHD, MaNC)

)

go
create TABLE ThamSo (
  MaTS varchar(10) primary key,
  TenTS nvarchar(100) NOT NULL,
  DVT nvarchar(10) NOT NULL,
  GiaTri nvarchar(20) NOT NULL,
  TrangThai bit NOT NULL
)

GO

INSERT INTO LoaiNhacCu (MaLoaiNC, TenLoaiNC, AnhLoaiNC)
VALUES
	('ATCD', N'Âm thanh chuyên dụng', 'ATCD.jpg' ),                                                                                                                                                         
	('AmpMon', N'Amplifiers & Monitors', 'AmpMon.jpg'),
	('B_Stock', N'B-Stock', 'B_Stock.jpg'),
	('CaseGig', N'Cases & Gig Bags', 'CaseGig.jpg'),
	('KeyPia', N'Keyboards & Pianos', 'KeyPia.jpg'),
	('Pedal', N'Pedals & Pedalboards', 'Pedal.jpg'),
	('PMTU', N'Phần mềm & Thu âm', 'PMTU.jpg'),
	('TBG', N'Trống & Bộ gõ', 'TBG.jpg')

go
INSERT INTO HangSanXuat (MaHSX, TenHSX, DiaChi, SDT) 
VALUES
('HSX01', N'CÔNG TY TNHH SẢN XUẤT – THƯƠNG MẠI VÀ DỊCH VỤ QUEEN’S GUITARS', N'Số 100 Đường Nguyễn Thiện Thuật, Phường 02, Quận 3, Thành phố Hồ Chí Minh', '0313550560'),
('HSX02', N'CÔNG TY TNHH SÁO TRÚC MÃO MÈO', N'Số 8, ngõ 2, đường Cao Lỗ, Phường Lê Mao, Thành phố Vinh, Tỉnh Nghệ An', '0316186575'),
('HSX03', N'CÔNG TY TNHH NHẠC CỤ GUITAR HAT', N'Tổ 8 - Phường Phú Lương - Quận Hà Đông - Hà Nội', '0316346344'),
('HSX04', N'CÔNG TY CỔ PHẦN XUẤT NHẬP KHẨU VÀ CHẾ TÁC NHẠC CỤ BWG', N'Số 10 ngõ 9 phố Huỳnh Thúc Kháng - Phường Láng Hạ - Quận Đống đa - Hà Nội', '0467455745'),
('HSX05', N'CÔNG TY TNHH NGHỆ THUẬT VÀ TRUYỀN THÔNG NHÂN QUÝ', N'29/16 Đường số 5 - Phường Tăng Nhơn Phú B - Quận 9 - TP Hồ Chí Minh', '0313234555'),
('HSX06', N'CÔNG TY TNHH SẢN XUẤT VÀ XUẤT NHẬP KHẨU GUITAR GỖ', N'5/7, Tổ 6, Suối Cát 1 - Xã Suối Cát - Huyện Xuân Lộc - Đồng Nai', '0896464224'),
('HSX07', N'CÔNG TY TNHH MỘT THÀNH VIÊN LÂM MINH NGUYỆT', N'Số 9, Ngách 82/23 Phố Nguyễn Phúc Lai - Phường ô Chợ Dừa - Quận Đống đa - Hà Nội', '0345656564'),
('HSX08', N'CÔNG TY ABC XYZ', N'Số 7 Nguyễn Trung Trực Ninh Hòa Khánh Hòa', '0134569852')
go
INSERT INTO NhaCungCap(MaNCC, TenNCC, SDT, DiaChi, Email) VALUES
('NCC01', N'NHẠC VIỆT – CÔNG TY TNHH THƯƠNG MẠI NHẠC VIỆT', '0838341500', N'319 Điện Biên Phủ, P. 4, Q. 3, Tp. Hồ Chí Minh (TPHCM)', 'musicland@hcm.vnn.vn'),
('NCC02', N'CÔNG TY TNHH HUY QUANG PIANO VÀ NHẠC CỤ', '0243512314', N'23 & 108 Hào nam, Đống Đa, Hà Nội', 'huyquangpiano@yahoo.com.vn'),
('NCC03', N'CƠ SỞ THẾ GIA SẢN XUẤT TRỐNG', '0283823375', N'18B1S/5 Nguyễn Thị Minh Khai, P. Đa Kao, Q. 1, Tp. Hồ Chí Minh (TPHCM)', 'trongthegia18bis@gmail.com'),
('NCC04', N'CÔNG TY CP TM & DV KT THÀNH ĐẠT – TRUNG TÂM ÂM NHẠC NHẠC CỤ TIẾN ĐẠT', '0246669953', N'Số 71 Đường Bờ Sông Quan Hoa, P. Quan Hoa, Q. Cầu Giấy, Hà Nội', 'info@nhaccutiendat.vn'),
('NCC05', N'HOÀNG HUY MUSIC', '0969474152', N'46 Hào Nam, Đống Đa, Hà Nội', 'quangdai1091@gmail.com'),
('NCC06', N'CÔNG TY TNHH THƯƠNG MẠI TRÙNG DƯƠNG', '0282400736', N'362 Điện Biên Phủ, P. 11, Q. 10, Tp. Hồ Chí Minh (TPHCM)', 'trungduongpiano@yahoo.com'),
('NCC07', N'CÔNG TY TNHH THƯƠNG MẠI HÓA QUANG', '0288207436', N'112 Điện Biên Phủ, P. Đa Kao, Q. 1, Tp. Hồ Chí Minh (TPHCM)', 'hoaquang@gmail.com'),
('NCC08', N'CÔNG TY TNHH ÂM THANH NHẠC CỤ MINH PHỤNG', '0969068606', N'Số 347 Điện Biên Phủ, P. 4, Q. 3, Tp. Hồ Chí Minh (TPHCM)', 'cskh.minhphung@gmail.com'),
('NCC09', N'CÔNG TY TNHH GIÁO DỤC VĂN HÓA NGHỆ THUẬT HIẾU NHẠC', '0235888666', N'220 Huỳnh Thúc Kháng, Tp. Tam Kỳ, Quảng Nam', 'hieunhac@gmail.com'),
('NCC10', N'CÔNG TY TNHH THƯƠNG MẠI MINH THANH P.I.A.N.O', '0251227603', N'369 Điện Biên Phủ, P4, Q3, Tp. Hồ Chí Minh (TPHCM)', 'info@pianominhthanh.com'),
('NCC11', N'CÔNG TY TNHH VĂN HÓA NGHỆ THUẬT ĐỨC THƯƠNG', '0316160560', N'Số 12, Đ. 30 Tháng 4, P. Trung Dũng, Tp. Biên Hòa, Đồng Nai', 'ducthuong8888@gmail.com'),
('NCC12', N'CÔNG TY TNHH 696', '0123456789', N'Hành tinh B89 -Số 972 hệ hành tinh Germa tinh hà Green Pea', 'tcoe65@gmail.com')
go
INSERT INTO NhacCu(MaNC, TenNC, MoTa, AnhNC, SoLuong,ThoiGianBaoHanh, DonGia, MaLoaiNC, MaNCC, MaHSX) VALUES
('ATCD001', N'Fender Passport Event Series 2 375W Portable PA System, 230V UK — F03-694-3004-900', N'Với điều chỉnh linh hoạt, kết nối tuyệt vời và tiện dụng khắp mọi nơi, dàn mixer di động Fender Passport® Event Series 2 hoàn hảo để khuyếch đại voice, đàn và nhạc nền ở bất cứ đâu, bất cứ lúc nào. Loa full dải tần, nhiều tính năng linh hoạt, bản điều chỉnh dễ dàng và công suất 375W mang đến âm thanh Fender vững chắc, mạnh mẽ và rành mạch, lý tưởng để dạy học, trong các sự kiện thể thao và nơi tôn giáo; cuộc họp, hội thảo và thuyết trình; và trình diễn nhạc tại các buổi tiệc, club nhỏ, quán cafe. Passport Event Series 2 có kết hợp giắc XLR/¼” để tăng kết nối.\r\nDễ setup và di chuyển, Passport Event Series 2 cũng có kết nối Bluetooth® để phát âm thanh không dây từ thiết bị di động.', 'anh1_ATCD.jpg',5,N'1 năm', 21780000, 'ATCD', 'NCC01', 'HSX02'),
('ATCD002', N'Fender Passport Venue Series 2 600W Portable PA System, 230V UK — F03-694-4004-900', N'Tối đa hóa độ chắc âm thanh của mọi màn trình diễn lớn bằng dàn mixer Fender Passport Venue Series 2. Kết nối Bluetooth® và giắc kết hợp XLR ¼” để tăng kết nối. Passport® Venue Series 2 có loa full dải tần, nhiều tính năng linh hoạt, bản điều chỉnh dễ dàng và công suất 600W mang đến âm thanh Fender vững chắc, mạnh mẽ và rành mạch, lý tưởng cho những dịp và không gian lớn—như show diễn band nhạc và DJ; dạy học, sự kiện thể thao và tôn giáo; cuộc họp, hội thảo và thuyết trình,...', 'anh2_ATCD.jpg',3,N'2 năm', 27280000, 'ATCD', 'NCC02', 'HSX01'),
('ATCD003', N'Behringer Pro Mixer DX2000USB 7-channel DJ Mixer — B03-000-96801', N'Ngày nay một DJ thực hiện nhiều thao tác hơn là chỉ dùng mic và một vài turntable! Trong bất kỳ buổi biểu diễn nào bạn đều cần làm quen với các phương thức trình diễn đa phương tiện, các nền tảng âm thanh đa dạng (vinyl, CD, MP3) và nhiều mic. Đó là lý do vì sao chúng tôi tự hào giới thiệu PRO MIXER DX2000USB 7 kênh với Crossfader vô cực không tiếp xúc có thể điều chỉnh hoàn toàn và có giao diện USB/audio tích hợp. Giờ đây DJ đa năng đã sẵn sàng để làm bùng nổ sân khấu!', 'anh3_ATCD.jpg',8,N'18 tháng', 7300000, 'ATCD', 'NCC09', 'HSX03'),
('ATCD004', N'Fender Passport Conference Series 2 175W Portable PA System, 230V EU — F03-694-2006-900', N'Với các tính năng dễ sử dụng và tính di động cao, dàn âm Fender Passport® Conference Series 2 lý tưởng cho những nơi vừa và nhỏ. Với kết nối Bluetooth®, loa full dãi tần, nhiều tính năng linh hoạt, điều chỉnh ở mặt trước và công suất 175W thể hiện âm thanh Fender rành mạch và chất lượng, lý tưởng trong nhiều ứng dụng—dạy học, sự kiện thể thao và nơi tôn giáo; cuộc họp, hội thảo và thuyết trình; và trình diễn nhạc tại các buổi tiệc, club nhỏ, quán cafe,... Độc lập, dễ setup và di chuyển, Passport Conference Series 2 có đủ mọi thứ bạn cần để tạo nên âm thanh ở bất cứ đâu và bất cứ khi nào.', 'anh4_ATCD.jpg',6,N'6 tháng', 11800000, 'ATCD', 'NCC11', 'HSX05'),
('ATCD005', N'Turbosound iQ15 2500W 15 inch Powered Speaker — T12-000-APM01', N'iQ15 là một loudspeaker power 2 chiều 2,500W hoàn hảo để lắp đặt cố định hoặc di động và cho các ứng dụng tăng cường âm thanh, âm nhạc và giọng nói. Driver bổ sung gồm driver 15” tần số thấp với voice coil low mass để cải thiện đáp ứng quá độ nhất thời, một driver compression 1” nhiệt độ cao với voice coil bằng nhôm bọc đồng để tái tạo tần số cao mở rộng.', 'anh5_ATCD.jpg',4,N'18 tháng', 21600000, 'ATCD', 'NCC03', 'HSX04'),
('AmpMon001', N'EVH 5150 III 50W Guitar Amplifier Head, Black — E08-225-3006-010-BSTK', N'EVH 5150III 50-Watt Head là phiên bản thu nhỏ của 5150 III 100-Watt head hùng mạnh với nhiều đặc tính tuyệt vời tương đồng. Kích cỡ nhỏ hơn và mang tính di động hơn đã đưa EVH 5150III 50-Watt Head trở thành bộ amp hoàn hảo dành cho người chơi muốn sở hữu cả một vũ đài với tất cả âm lượng, âm sắc và hiệu suất mà chỉ trong dáng hình nhỏ gọn. Là bộ amp có ba kênh với trở kháng được tuỳ chọn (4, 8 hoặc 16 ohms), các dắt output cho loa kép tương đồng, mạch hiệu ứng, dắt headphone và phần cứng màu đen, tuyến ngoài.', 'anh1_Ampli.jpg',2,N'3 năm', 27232000, 'AmpMon', 'NCC06', 'HSX05'),
('AmpMon002', N'Ibanez IFS2L Dual Footswitch Latching — I01-IFS2L-BSTK', N'Dành để sử dụng với IL15 Iron Label Tube Combo Amplifier, IFS2L cho phép bạn dễ dàng chuyển đổi giữa các amps hai kênh.', 'anh2_Ampli.jpg',2,N'2 năm 6 tháng', 2722000, 'AmpMon', 'NCC04', 'HSX03'),
('AmpMon003', N'Fender Mustang LT50 Guitar Combo Amplifier, 230V UK — F03-231-1204-000', N'Mustang LT50 kết hợp những gì chúng tôi đã tích luỹ qua hàng thập kỷ tạo dựng amp guitar đỉnh nhất thế giới. Lý tưởng để luyện tập hoặc trình diễn, với công suất 50W, loa 12\" chất lượng cao, giao diện người dùng cực kỳ đơn giản, màn hình màu, và nhiều preset đa dạng các thể loại nhạc — \"hit tuyệt nhất\" của âm guitar electric. Chuỗi tín hiệu linh hoạt với model hiệu ứng và amp chất lượng tạo ra âm thanh tuyệt vời, giúp amp 50W có nguồn cảm hứng bậc nhất trong cùng hạng mục. Và tải Fender TONE 3.0 miễn phí, sử dụng máy tính Mac hoặc PC để edit, lưu trữ và quản lý preset dễ dàng. Mustang LT50 là amp modelling vui tươi, linh hoạt và dễ sử dụng dành cho mọi guitarist. Amp chỉ có công suất 50W và nhiều tính năng.', 'anh3_Ampli.jpg',5,N'18 tháng', 6600000, 'AmpMon', 'NCC05', 'HSX07'),
('AmpMon004', N'HeadRush FRFR-112 2000watt 1x12Inch Guitar Cabinet, EU Plug — H32-FRFR-112', N'HeadRush Pedalboard làm mưa làm gió thị trường nhạc cụ với sự đổi mới mang tính đột phá trong modelling FX và Amp. Giờ đây bạn đã có thể nâng tầm thiết bị của mình lên những tiêu chuẩn sáng tạo mới của những thiết bị modeller ngày nay, đừng phụ thuộc vào đáp ứng tần số bị giới hạn của những hệ thống PA hay amp truyền thống. Bạn cần FRFR-112.', 'anh4_Ampli.jpg',2,N'3 năm 6 tháng', 9600000, 'AmpMon', 'NCC07', 'HSX01'),
('AmpMon005', N'Fender Mustang GTX50 Guitar Combo Amplifier, 230V UK — F03-231-0604-000', N'Mustang GTX amp guitar mạnh dạng hơn, tốt hơn với đặc tính chưa từng có và hiệu suất không thể đánh bại. Nhiều lựa chọn model amp chính xác và linh hoạt, nhiều hiệu ứng và 200 preset, cho bạn âm thanh guitar mà bạn cần trong hầu hết các thể loại nhạc. Đường truyền tín hiệu module linh hoạt giúp bạn di chuyển hiệu ứng mọi vị trí trong chuỗi, và màn hình màu hiển thị sắc nét cho biết các chế độ đang sử dụng. Kết nối Bluetooth để trải nghiệm Fender TONE 3.0 (dành cho iOS và Android) hoàn toàn mới, ứng dụng miễn phí mang đến giao diện và cảm giác chân thực khi bạn xoay nút chỉnh hiệu ứng và amp nổi tiếng nhất mọi thời đại. Bạn có thể edit, duyệt preset từ tất cả Fender Tone, dự phòng preset và khôi phục, và nhiều chức năng khác. Có thể truyền âm thanh qua Bluetooth để chơi cùng track bạn yêu thích. Chức năng WIFI của amp (độc quyền Fender) giúp bạn kết nối để cập nhật sản phẩm, amp của bạn luôn được nâng cấp không ngừng.', 'anh5_Ampli.jpg',2,N'3 năm', 10000000, 'AmpMon', 'NCC09', 'HSX04'),
('B_Stock001', N'Epiphone Thunderbird Pro-IV Bass Guitar, Natural Oil (B-Stock)', N'Guitar bass Thunderbird kinh điển đã trở thành một hình tượng trong dòng nhạc pop. Epiphone Thunderbird™ PRO thuộc Professional Series và là một cây rock and roll bass đỉnh cao với hình dáng đặc biệt, nổi tiếng thế giới. Được trình làng lần đầu tiên vào năm 1963, Thunderbird PRO kết hợp các yếu tố thiết kế truyền thống với công nghệ hiện đại, có mức giá phải chăng mà mọi tay bass có thể sở hữu.', 'anh1_Bstock.jpg',3,N'2 năm', 12000000, 'B_Stock', 'NCC08', 'HSX05'),
('B_Stock002', N'Ibanez AE205-BS Acoustic Guitar, Brown Sunburst High Gloss (B-Stock)', N'Sinh ra từ dòng dõi đàn dây lâu đời, acoustic guitar mang đậm nét lịch sử và truyền thống. Nhưng âm nhạc không ngừng phát triển, và người nghệ sỹ luôn yêu cầu các công cụ mới mang niềm cảm hứng để tạo ra âm thanh đặc trưng của riêng họ. Với tinh thần đó, Ibanez ra mắt Ibanez AE, series guitar acoustic và acoustic-electric chủ đạo mới. Thể hiện âm thanh và tính năng chơi xuất sắc, Ibanez AE là bước tiến tiếp theo trong truyền thống tuyệt vời. Với một loạt các AE models để lựa chọn, tất cả đều mang kết hợp tonewoods và đa dạng màu sắc, và được chế tác theo đúng chất lượng bạn mong đợi từ Ibanez, chắc chắn sẽ có một cây guitar hợp với phong cách và sở thích của mọi người chơi.', 'anh2_Bstock.jpg',10,N'1 năm', 11100000, 'B_Stock', 'NCC10', 'HSX04'),
('B_Stock003', N'Epiphone PR-5E Acoustic/Electric Guitar, Natural (B-Stock)', N'Được Epiphone thiết kế năm 1990, dáng đàn được lấy từ phong cách đặc trưng của ES-335 cổ điển theo hai dòng Sheraton và Casino. Nhưng thay vì cutaway ở cả hai bên thân thì PR-5E lại mang dáng vẻ cutaway một bên thân sắc nét. Phần thân dưới to nhằm cho ra chất âm trầm đặc sắc và dễ dàng di chuyển tay ở những nốt cao trên cần đàn.', 'anh3_Bstock.jpg',5,N'3 năm', 8300000, 'B_Stock', 'NCC11', 'HSX03'),
('B_Stock004', N'Ibanez AP7 Analog Phaser Guitar Effects Pedal (B-Stock)', N'Tone-Lok Series AP7 Analog Phaser Pedal từ Ibanez sở hữu mạch analog thuần tuý tạo ra được chất âm phase cổ điển trầm, ấm. Điều chỉnh Speed, Depth, Feedback cho phép bạn nhập được nhiều sắc âm. Mạch phase 3 cách chỉnh giúp bạn có thể chọn phase 4-, 6-, hoặc 8 đoạn. Khi chuyển sang pha 4 đoạn, pedal hiệu ứng tái tạo âm thanh phase cổ điển. Khi chuyển sang phase 8 đoạn, guitar pedal tạo ra các đặc tính phase trầm và mạnh mẽ.', 'anh4_Bstock.jpg',5,N'4 năm', 2116000, 'B_Stock', 'NCC02', 'HSX02'),
('B_Stock005', N'Ibanez SH7 7TH Heaven Guitar Effects Pedal (B-Stock)', N'Tạo ra cổ máy tàn phá âm thanh đầy tinh tuý chỉ với một hộp distortion được tạc tác dành riêng cho ghita 7 dây đã quá nổi tiếng của Ibanez. Được thiết kế cùng sự kết hợp với một số người chơi đàn 7 dây đình đám nhất hiện nay, 7th Heaven pedal cho phép bạn nêu bật đường low-end của dây Xi thấp trong khi vẫn giữ được tối đa độ rõ nhất có thể.', 'anh5_Bstock.jpg',6,N'4 năm', 1472000, 'B_Stock', 'NCC04', 'HSX01'),
('CaseGig001', N'koda essential Dreadnought Acoustic Guitar Bag TWO', N'Dreadnought Acoustic Guitar Bag TWO vừa vặn với guitar dreadnought tiêu chuẩn. Dây thắt bên trong có băng dán giữ cần đàn cố định khi di chuyển, tay cầm giúp cân bằng trọng lượng đàn nên có thể xách bao đàn bằng một tay. Đi kèm dây đeo kiểu ba lô để thuận tiện cho những chuyến đi dài.', 'anh1_Case.jpg',3,N'2 năm', 229000, 'CaseGig', 'NCC03', 'HSX01'),
('CaseGig002', N'MONO Vertigo Ultra Electric Guitar Case, Black', N'Sản phẩm bảo vệ tốt nhất đã được nâng cấp. Vertigo Ultra Electric hoàn toàn mới với một loạt những cải tiến về độ bền, công thái học và cấu tạo để khiến những chuyến đi dài trở nên dễ chịu hơn. Sở hữu mọi khả năng và sẵn sàng cho mọi điều - một sản phẩm mà mọi nhạc sĩ nhất định phải sở hữu khi lưu diễn.', 'anh2_Case.jpg',3,N'1 năm', 6700000, 'CaseGig', 'NCC09', 'HSX03'),
('CaseGig003', N'Ibanez IUBS301-BK Gig Bag For Soprano Ukulele, Black', N'Bao đàn Ibanez 301 Ukulele hoàn hảo dành cho người chơi ukulele thường phải di chuyển để luyện tập hoặc đến điểm diễn. Dây strap chắc chắn giúp dễ dàng đeo trên vai mà không cần tay phải cầm nắm. Logo Ibanez cổ điển và khoá kéo lớn với màu cam sành điệu trên nền đen.', 'anh3_Case.jpg',10,N'1 năm', 290000, 'CaseGig', 'NCC07', 'HSX05'),
('CaseGig004', N'Fender FA405 Dreadnought Acoustic Guitar Gig Bag', N'Bao đàn F405 Series của Fender có kiểu dáng đẹp và giá thành tốt, giúp giữ cho acoustic dreadnought an toàn khi di chuyển. Mặt ngoài bền chắc kết cấu từ polyester dày 400 Denier, bảo vệ đàn tránh va đập và chống xước, rách. Mặt trong được lót đệm dày 5mm bằng nhung mềm mịn, giữ cho đàn ngay ngắn và an toàn, tránh hư hại lớp phủ đàn. Bao đàn F405 có tay cầm và dây strap kiểu ba lô tiện dụng, bạn có thể mang đàn một cách nhẹ nhàng. Hơn nữa, còn có ngăn túi trượt phía trước dành cho các phụ kiện nhỏ.', 'anh4_Case.jpg',15,N'18 tháng', 890000, 'CaseGig', 'NCC05', 'HSX07'),
('CaseGig005', N'SKB 1SKB-5820W ATA 88 Note Keyboard Case w/Wheels', N'Hộp đựng bàn phím 88 nốt 1SKB-4214W có thiết kế khóa ở góc đã được cấp bằng sáng chế, giúp giữ bàn phím của bạn cố định trên bề mặt móc và dây buộc vòng không thể phá hủy. Lớp bọt ở trên cùng của nắp giúp hỗ trợ thêm và bảo vệ bàn phím khi đang vận chuyển. Các tấm cản bên ngoài bảo vệ phần diềm và phần cứng khỏi bị hư hại do va đập. Các chốt được làm bằng nylon gia cố bằng sợi thủy tinh không thể phá hủy và bao gồm hệ thống chốt nhả kích hoạt được TSA công nhận và chấp nhận cho phép bạn khóa hộp đựng của mình và vẫn được kiểm tra tại an ninh sân bay.', 'anh5_Case.jpg',8,N'2 năm', 16200000, 'CaseGig', 'NCC02', 'HSX04'),
('KeyPia001', N'Alesis Prestige Artist 88-Key Digital Piano with Graded Hammer Action Keys', N'Mang trải nghiệm chơi piano chân thực về tổ ấm với Alesis Prestige Artist, một cây piano kỹ thuật số đầy đủ tính năng với các phím dạng búa tiêu chuẩn và 30 âm thanh tích hợp cực kỳ chân thực. Đây là sự thay thế hoàn hảo cho việc sở hữu một cây đàn piano acoustic kích thước đầy đủ, với nhiều voice đa mẫu cao cấp, 256 âm phức điệu và hệ thống speaker micro-array 50W tùy chỉnh cho âm thanh dày dặn và rực rỡ bất kể bạn đặt piano ở vị trí nào trong phòng.', 'anh1_Piano.jpg',6,N'3 năm', 14300000, 'KeyPia', 'NCC08', 'HSX07'),
('KeyPia002', N'Alesis Virtue 88-Key Digital Piano with Wood Stand and Bench', N'Đây là bộ trang thiết bị toàn diện sẽ làm bạn ngạc nhiên! Bao gồm chân đế piano bằng gỗ màu đen, một giá nhạc và 3 pedal (sustain, soft, sostenuto) và ghế ngồi tuỳ chỉnh, Virtue có đủ trang thiết bị mà người mới tập chơi piano cần để bắt đầu, Virtue digital piano có 88 phím full-size, hai loa 30W (15W x 2) và phức điệu tối đa 128 nốt, mang đến trải nghiệm âm thanh chân thực và lôi cuốn.', 'anh2_Piano.jpg',10,N'5 năm', 12400000, 'KeyPia', 'NCC01', 'HSX05'),
('KeyPia003', N'Alesis Recital Pro 88-key Hammer Action Digital Piano', N'Bạn có thể tùy chỉnh tiếng bằng cách kết hợp hai trong một ở chế độ Layer Mode để tạo ra chất âm dày, đầy đặn với việc sử dụng các điều chỉnh phía trên và màn hình hiển thị. Tiếng có thể được chỉ định ở tay trái hoặc phải theo chế độ Split Mode. Bạn còn có thể bổ sung Modulation, Reverb và Chorus tuỳ chỉnh để tạo nét âm thanh riêng cho mình. Với 20 loa 20W mạnh mẽ và đa âm tối đa 128 nốt, Recital Pro mang đến âm thanh cực kỳ chân thực với trải nghiệm chơi nhạc thuần chất.', 'anh3_Piano.jpg',3,N'2 năm 6 tháng', 11500000, 'KeyPia', 'NCC04', 'HSX06'),
('KeyPia004', N'Novation Impulse 49 USB MIDI Controller KB 4 Octave, Touch Sensitive Controls, LED Light Rings', N'Impulse là keyboard siêu nhạy và biểu cảm với vô số điều khiển tuỳ chuyển. Được tạo ra dành cho mọi người yêu thích chơi keyboard, Impulse sẵn sàng hoạt động và kết hợp với Ableton Live, Logic Pro, Pro Tools và các phần mềm âm nhạc chính yếu khác.', 'anh4_Piano.jpg',6,N'1 năm', 8200000, 'KeyPia', 'NCC03', 'HSX03'),
('KeyPia005', N'Nord Piano 5 73-key Stage Piano', N'Phiên bản mới nhất của dòng Piano từng đoạt giải thưởng của chúng tôi được trang bị động cơ piano kép, synth mẫu kép và bộ nhớ gấp đôi thế hệ trước. Với sự kết hợp của phím bấm Cảm biến ba cao cấp và Công nghệ hành động búa ảo độc quyền của chúng tôi, Nord Piano 5 di động mang đến trải nghiệm chơi đặc biệt.', 'anh5_Piano.jpg',4,N'1 năm', 80100000, 'KeyPia', 'NCC02', 'HSX01'),
('Pedal001', N'Hotone MP-300 Ampero II Stomp Multieffects Pedal', N'Ampero II Stomp giới thiệu chuỗi hiệu ứng kép có thể tùy chỉnh cao hỗ trợ nhiều định tuyến tín hiệu nối tiếp / song song, có thể sử dụng đồng thời 12 mô-đun hiệu ứng. Hãy thử nó khi bạn thấy phù hợp.', 'anh1_Pedal.jpg',10,N'5 năm', 13400000, 'Pedal', 'NCC10', 'HSX02'),
('Pedal002', N'Strymon Zuma Power Supply', N'Zuma là con chiến mã mạnh bậc nhất, có bộ nguồn pedal hiệu ứng được phát triển công nghệ tối tân nhất. Zuma cung ứng 9 output độ ồn cực thấp, được tách riêng lẻ và có dòng điện mạnh, mỗi ouput đều có có bộ điều chỉnh riêng và máy biến áp tùy chỉnh. Được thiết kế nhằm đáp ứng nhu cầu của dân chơi ngày nay, mỗi output cung cấp dòng điện 500mA đến kinh ngạc.', 'anh2_Pedal.jpg',8,N'1 năm', 7100000, 'Pedal', 'NCC11', 'HSX03'),
('Pedal003', N'TC Electronic Hall of Fame 2 Reverb Guitar Effects Pedal', N'HALL OF FAME REVERB nguyên bản truyền tải nhiều reverb mang tính biểu tượng nhất mọi thời đại, nhưng HALL OF FAME 2 REVERB duy trì lâu hơn nữa những di sản của cải tiến đó. Công nghệ MASH đáng kinh ngạc của HALL OF FAME 2 REVERB thêm pedal expression vào stompbos reverb của thế giới, một reverb không chỉ là phản hồi với cái chạm của bạn mà còn tiết kiệm nhiều không gian hơn trên pedalboard trước đó. Hãy thêm effect Shimmer đầy tinh tế và bạn đã có được một pedal reverb không giống bất kì reverb nào.', 'anh3_Pedal.jpg',15,N'2 năm 4 tháng', 3850000, 'Pedal', 'NCC04', 'HSX04'),
('Pedal004', N'Strymon Iridium Amp & IR Cab Guitar Effects Pedal', N'Không gì biểu lộ được bản chất thật sự của cây guitar và lối chơi của bạn như tube amp đẳng cấp thế giới hoàn toàn phù hợp với speaker cabinet trong một không gian âm thanh tuyệt vời. Giờ đây đã có một pedal có thể thực sự truyền tải được âm thanh và cảm nhận đó, hơn nữa, loa cabinet này sử dụng cực kỳ dễ dàng. Bạn hãy thử khám phá độ phản hồi tuyệt vời của tube amp, tính chân thực chưa từng có trước đây của speaker cabinet trong đáp ứng xung (impulse response), và không gian âm thanh tự nhiên trong phòng có thể kiểm soát được.', 'anh4_Pedal.jpg',7,N'2 năm', 10300000, 'Pedal', 'NCC07', 'HSX05'),
('Pedal005', N'Strymon Zelzah Multidimensional Phaser', N'Bước lên Zelzah, chơi một hợp âm và bạn ngay lập tức được đưa trở lại thế giới của các âm phaser cổ điển vì chúng luôn được dùng để phát ra âm thanh, được lồng tiếng hoàn hảo, với nhiều rung cảm. Giai đoạn 4 giai đoạn và 6 giai đoạn cổ điển với các nút điều khiển cho phép chuyển đổi liền mạch sang giai điệu rung, bích tuyệt đẹp và điệp khúc, và những âm thanh mới chưa từng nghe thấy trước đây — tất cả đều ngay lập tức âm nhạc với giai điệu tuyệt vời trên mọi mặt số.', 'anh5_Pedal.jpg',8,N'16 tháng', 9000000, 'Pedal', 'NCC06', 'HSX06'),
('PMTU001', N'Focusrite Scarlett Solo (3rd Generation)', N'Nếu bạn muốn bắt đầu tạo ra các bản thu chất lượng studio cùng với guitar thì 3rd Generation Scarlett Solo mang đến phương thức dễ dàng để thực hiện điều đó.Bắt trọn âm nhạc của bạn ở bất cứ đâu bằng cách cắm guitar trực tiếp hoặc mic up, và kiểm âm trực tiếp để foldback không bị trễ. Scarlett Solo mang đến cho nhạc sỹ khắp thế giới âm thanh chuyên nghiệp mọi lúc, mọi nơi.', 'anh1_Phanmem.jpg',4,N'2 năm 6 tháng', 2750000, 'PMTU', 'NCC11', 'HSX01'),
('PMTU002', N'TC Helicon GO XLR Audio Interface, EU', N'Hãy điểm lại những gì bạn từng biết về phát thanh và quên toàn bộ những điều đó đi. Những gì đã từng tốn của bạn hàng tá thiết bị phần cứng và phần mềm giờ đây đã được thay thế bằng một giải pháp trực quan và đẹp mắt. Mix nhạc theo thời gian thực, thay đổi giọng nói, sử dụng phần cứng và phần mềm chuyên dụng và thu hút người nghe của bạn hơn bao giờ hết.', 'anh2_Phanmem.jpg',3,N'2 năm', 12400000, 'PMTU', 'NCC02', 'HSX02'),
('PMTU003', N'Novation Launchkey Mini MK3 Keyboard Controller', N'LaunchKey Mini là một keyboard controller 24 phím mini nhỏ gọn và di động. Nó cho bạn mọi thứ bạn cần để bắt đầu sáng tạo với Ableton Live - và nó vừa vặn trong túi của bạn. Bạn có thể sáng tác các đoạn nhạc ở mọi nơi với điều khiển Ableton siêu nhạy của LaunchKey Mini, sáng tác hợp âm, chế độ Fixed Chord, MIDI out, và rất nhiều âm thanh khác nhau.', 'anh3_Phanmem.jpg',5,N'2 năm', 2900000, 'PMTU', 'NCC08', 'HSX03'),
('PMTU004', N'Arturia Spark LE Hybrid Creative Drum Machine', N'Được xây dựng dựa trên Spark nguyên bản, LE Hybrid Creative Drum Machine của ARTURIA là một chiếc máy trống năng động, mạnh mẽ, cung cấp giao diện dễ dàng và kích thước nhỏ hơn, di động hơn cho những nhạc sĩ đánh giá cao hiệu quả.', 'anh4_Phanmem.jpg',7,N'4 năm', 7750000, 'PMTU', 'NCC03', 'HSX04'),
('PMTU005', N'M-Audio BX8 D3 - 8 Inch Active Studio Monitor Speaker, Each', N'Loa kiểm âm studio không phải là bộ loa thông thường. Monitor thật sự phải luôn chuẩn xác, cùng phản hồi âm thanh trung thực và distortion cực thấp. Trong loa kiểm âm đẳng cấp phòng thu, không có chỗ cho danh sách những giới hạn hay màu mè có trong nhiều loa thông thường. Trường âm thanh vô địch, THD cao, nasal midrange, mid-bass nổi bật, và không liên kết pha không có chức năng gì khác ngoài việc tăng sự tự tin cần thiết khi phối âm tốt hơn.', 'anh5_Phanmem.jpg',8,N'5 năm', 5500000, 'PMTU', 'NCC05', 'HSX05'),
('TBG001', N'Alesis Nitro Mesh Electronic Drum Kit', N'Alesis Nitro Mesh là bộ trống điện 8 món hoàn chỉnh tập trung vào công nghệ mặt trống Alesis Mesh thế hệ mới. Mặt trống lưới là sở thích của số đông các tay trống khi họ sử dụng trống điện vì cảm giác tự nhiên và phản ánh âm thanh cực kỳ êm. Alesis Nitro Mesh có trống snare mặt lưới kép 8\" và 3 trống tom mặt lưới 8\". Đủ mọi thứ bạn cần để làm nên bộ trống hoàn chỉnh; 3 cymbal 10\", hi-hat pedal Alesis thiết kế riêng và pedal trống bass, và dàn rack bằng nhôm 4 trụ chắc bền. Tất cả kết nối với module trống điện Nitro mạnh mẽ cùng hàng trăm âm thanh bộ gõ, 40 bộ khác nhau và 60 track chơi kèm.', 'anh1_Trong.jpg',10,N'6 năm', 13400000, 'TBG', 'NCC07', 'HSX01'),
('TBG002', N'Evans EQPB1 EQ Black Nylon Single Patch', N'Thành viên của đại gia đình Addario, đơn vị dẫn đầu trong lĩnh vực phụ kiện âm nhạc trên toàn cầu, thương hiệu Evans đi đôi với chất lượng và sự vững chắc. Cách đây hàng thập kỷ, Evans là cá nhân đi tiên phong trong thiết kế và sản xuất mặt trống, và ở hiện tại họ là nhà cách tân. Miếng patch trống bass Evans được dùng trực tiếp trên trống bass và mang thiết kế nhằm bảo vệ mặt trống tránh sức đập pedal và kéo dài tuổi thọ. Patch bằng chất liệu nylon màu đen làm dịu attack và cũng cố mặt trống mà không gây ảnh hưởng đến độ ngân hoặc low-end, và có cả phiên bản pedal đơn và pedal đôi.', 'anh2_Trong.jpg',20,N'1 năm', 160000, 'TBG', 'NCC04', 'HSX02'),
('TBG003', N'Evans B14EC2S 14inch EC2 Frosted - Snare/Tom/Timbale', N'Thành viên của đại gia đình Addario, đơn vị dẫn đầu trong lĩnh vực phụ kiện âm nhạc trên toàn cầu, thương hiệu Evans đi đôi với chất lượng và sự vững chắc. Cách đây hàng thập kỷ, Evans là cá nhân đi tiên phong trong thiết kế và sản xuất mặt trống, và ở hiện tại họ là nhà cách tân. Được thiết kế, gia công và sản xuất tại Hoa Kỳ, mặt trống được làm từ hai lớp màn 7mil, tạo độ chắc bền giúp tăng thời gian sử dụng.', 'anh3_Trong.jpg',14,N'3 năm', 560000, 'TBG', 'NCC03', 'HSX03'),
('TBG004', N'MEINL Percussion SUB18 18inch Bahia Surdo, Aluminium', N'Bahia Surdos được làm ra để kỷ niệm Samba-Reggae. Mỗi nhạc cụ được cắt ngắn hơn so với Rio và thường được đeo thấp hơn nhiều, ở trên thắt lưng.', 'anh4_Trong.jpg',7,N'3 năm 6 tháng', 7000000, 'TBG', 'NCC03', 'HSX04'),
('TBG005', N'Latin Percussion LP1424 Americana Series Kevin Ricard Signature Cajon', N'Được thiết kế cùng với Kevin Ricard, cajon này có cơ chế điều chỉnh hàng đầu đã được cấp bằng sáng chế của chúng tôi được trang bị bốn dây đàn guitar D’Addario được điều chỉnh độc lập. Được làm bằng cây trồng 11 lớp được chọn lọc thủ công, thân Baltic Birch và thùng đàn Heartwood tuyệt đẹp, cajon đi kèm với các góc cạnh để tăng thêm sự thoải mái khi chơi.', 'anh5_Trong.jpg',6,N'1 năm', 12000000, 'TBG', 'NCC09', 'HSX05')
GO
INSERT INTO ChucVu(MaCV,TenCV) 
VALUES
	('BH', N'Nhân viên bán hàng'),
	('QL', N'Quản lý')

go
INSERT INTO NhanVien(MaNV,HoTenNV , SDT, Email, DiaChi,TenDN, MatKhau, MaCV) VALUES
('NV01', N'Nguyễn Thành Danh','0367841249','danh@gmail.com' , N'Ninh Hòa - Khánh Hòa','Danh1102', 'fcea920f7412b5da7be0cf42b8c93759',  'QL'),
('NV02', N'Nguyễn Trúc Vy', '0973741256','vy@gmail.com' , N'Nha Trang - Khánh Hòa','Vy0204', 'fcea920f7412b5da7be0cf42b8c93759', 'BH'),
('NV03', N'Nguyễn Duy Long', '0567345390','long@gmail.com' , N'Cam Ranh - Khánh Hòa','Long0403', 'fcea920f7412b5da7be0cf42b8c93759', 'BH')
Go
INSERT INTO KhachHang(MaKH, HoTenKH, SDT, Email, DiaChi, TaiKhoan, MatKhau, AnhKH) 
VALUES
('KH0001', N'Nguyễn Hữu Hưng', '0293899685', 'nguyenhuuhung@gmail.com', N'Khánh Hòa', 'hung123', 'fcea920f7412b5da7be0cf42b8c93759',null),
('KH0002', N'Trần Bình An', '0940358395', 'tranbinhan@gmail.com', N'Khánh Hòa', 'an1234', 'fcea920f7412b5da7be0cf42b8c93759', null),
('KH0003', N'Trần Long', '0123125100', 'tranlong@gmail.com', N'34 Vĩnh Thái Nha Trang', 'long2343', 'fcea920f7412b5da7be0cf42b8c93759', null)

go
INSERT INTO HoaDon (MaHD, NgayDH, NgayGH, MaKH, MaNV, TongTien, TinhTrangDuyet, TinhTrangDonHang) VALUES
('HD00001', '2022-12-02', '2022-12-05', 'KH0002', 'NV02',145000, 1, 0),
('HD00002', '2022-12-02', '2022-12-15', 'KH0003', 'NV03',345000, 1, 1),
('HD00003', '2022-12-03', '2022-12-06', 'KH0003', 'NV01',478000, 0, 0)
go
INSERT INTO CTHoaDon(MaHD, MaNC, SoLuongBan, DonGiaBan) VALUES
('HD00001', 'ATCD002', 1, 27280000),
('HD00001', 'ATCD003', 1, 7300000),
('HD00001', 'ATCD004', 2, 11800000),
('HD00002', 'ATCD003', 3, 7300000),
('HD00002', 'ATCD005', 1, 21600000),
('HD00003', 'ATCD002', 1, 27280000),
('HD00003', 'ATCD003', 1, 7300000),
('HD00003', 'AmpMon002', 1, 2722000)
go
INSERT INTO ThamSo (MaTS, TenTS, DVT, GiaTri, TrangThai) VALUES
('TS001',N'Số lượng nhập hàng tối đa', N'Cái', '100',1)


go
CREATE PROCEDURE NhacCu_TimKiem
    @MaNC varchar(10)=NULL,
	@TenNC nvarchar(100)=NULL,
	@ThoiGianBaoHanh nvarchar(50)= NULL,
	@DonGiaMin varchar(30)=NULL,
	@DonGiaMax varchar(30)=NULL,
	@MaLoaiNC nvarchar(10)=NULL,
	@MaNCC nvarchar(10)=NULL,
	@MaHSX nvarchar(10)=NULL,	
	@SoLuong nvarchar(70)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhacCu
       WHERE  (1=1)
       '
IF @MaNC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNC LIKE ''%'+@MaNC+'%'')
              '
IF @TenNC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (TenNC LIKE N''%' + @TenNC + '%'')
			  '
IF @ThoiGianBaoHanh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (ThoiGianBaoHanh LIKE N''%'+@ThoiGianBaoHanh+'%'')
			 '
IF @DonGiaMin IS NOT NULL and @DonGiaMax IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (DonGia Between Convert(int,'''+@DonGiaMin+''') AND Convert(int, '''+@DonGiaMax+'''))
             '
IF @MaLoaiNC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaLoaiNC LIKE ''%'+@MaLoaiNC+'%'')
              '
IF @MaNCC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNCC LIKE ''%'+@MaNCC+'%'')
              '
IF @MaHSX IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaHSX LIKE ''%'+@MaHSX+'%'')
              '
IF @SoLuong IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (SoLuong = Convert(int,'''+@SoLuong+'''))
              '
	EXEC SP_EXECUTESQL @SqlStr
END
go
CREATE PROCEDURE NhaCungCap_TimKiem
    @MaNCC varchar(10)=NULL,
	@TenNCC nvarchar(100)=NULL,
	@SDT nvarchar(10)= NULL,
	@Email nvarchar(50)=NULL,
	@DiaChi nvarchar(500)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhaCungCap
       WHERE  (1=1)
       '
IF @MaNCC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNCC LIKE ''%'+@MaNCC+'%'')
              '
IF @TenNCC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (TenNCC LIKE N''%' + @TenNCC + '%'')
			  '
IF @SDT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (SDT LIKE N''%'+@SDT+'%'')
			 '
IF @Email IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (Email LIKE N''%'+@Email+'%'')
			 '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (DiaChi LIKE N''%'+@DiaChi+'%'')
			 '         
	EXEC SP_EXECUTESQL @SqlStr
END
go
CREATE PROCEDURE LoaiNhacCu_TimKiem
    @MaLoaiNC varchar(10)=NULL,
	@TenLoaiNC nvarchar(50)=NULL

AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM LoaiNhacCu
       WHERE  (1=1)
       '
IF @MaLoaiNC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaLoaiNC LIKE ''%'+@MaLoaiNC+'%'')
              '
IF @TenLoaiNC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (TenLoaiNC LIKE N''%' + @TenLoaiNC + '%'')
			  ' 
	EXEC SP_EXECUTESQL @SqlStr
END
go
CREATE PROCEDURE HangSanXuat_TimKiem
    @MaHSX varchar(10)=NULL,
	@TenHSX nvarchar(50)=NULL,
	@DiaChi nvarchar(500)=NULL,
	@SDT nvarchar(10)= NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM HangSanXuat
       WHERE  (1=1)
       '
IF @MaHSX IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaHSX LIKE ''%'+@MaHSX+'%'')
              '
IF @TenHSX IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (TenHSX LIKE N''%' + @TenHSX + '%'')
			  '   
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (DiaChi LIKE N''%'+@DiaChi+'%'')
			 '  
IF @SDT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (SDT LIKE N''%'+@SDT+'%'')
			 '
	EXEC SP_EXECUTESQL @SqlStr
END
go
CREATE PROCEDURE KhachHang_TimKiem
	@MaKH varchar(10) = NULL,
	@HoTenKH nvarchar(100) = NULL,
	@SDT nvarchar(10) = NULL,
	@Email nvarchar(50) = NULL,
	@DiaChi nvarchar(500) = NULL,
	@TaiKhoan nvarchar(30) = NULL

AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM KhachHang
       WHERE  (1=1)
       '
IF @MaKH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaKH LIKE ''%'+@MaKH+'%'')
              '
IF @HoTenKH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoTenKH LIKE N''%' + @HoTenKH + '%'')
			  '
IF @SDT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (SDT LIKE N''%'+@SDT+'%'')
			 '
IF @Email IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (Email LIKE N''%'+@Email+'%'')
			 '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (DiaChi LIKE N''%'+@DiaChi+'%'')
              '
IF @TaiKhoan IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (TaiKhoan LIKE N''%'+@TaiKhoan+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END
go
-- tìm kiếm nhân viên
CREATE PROCEDURE NhanVien_TimKiem
	@MaNV varchar(10) = NULL,
	@HoTenNV nvarchar(100) = NULL,
	@SDT nvarchar(10) = NULL,
	@Email nvarchar(50) = NULL,
	@DiaChi nvarchar(500) = NULL,
	@TenDN nvarchar(30) = NULL,
	@MaCV varchar(10) = NULL

AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhanVien
       WHERE  (1=1)
       '
IF @MaNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNV LIKE ''%'+@MaNV+'%'')
              '
IF @HoTenNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoTenNV LIKE N''%' + @HoTenNV + '%'')
			  '
IF @SDT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (SDT LIKE N''%'+@SDT+'%'')
			 '
IF @Email IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (Email LIKE N''%'+@Email+'%'')
			 '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (DiaChi LIKE N''%'+@DiaChi+'%'')
              '
IF @TenDN IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (TenDN LIKE N''%'+@TenDN+'%'')
              '
IF @MaCV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaCV LIKE ''%'+@MaCV+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END








-- dang lam tim kiem hoa don







--EXEC GetSalesReport '2022-12-14' , '2022-12-14'


GO
CREATE PROCEDURE GetSalesReport
    @FromDate date = NULL,
    @ToDate date = NULL
AS
BEGIN
    SELECT  NhacCu.TenNC, NhacCu.MaNC, NhaCungCap.TenNCC, LoaiNhacCu.TenLoaiNC, HangSanXuat.TenHSX,NhacCu.DonGia, NhacCu.SoLuong AS TON, 
            SUM(CTHoaDon.SoLuongBan) as SoLuongBan, 
            SUM(CTHoaDon.SoLuongBan * CTHoaDon.DonGiaBan) as Gia
    FROM NhacCu
    JOIN CTHoaDon ON NhacCu.MaNC = CTHoaDon.MaNC 
    JOIN NhaCungCap on NhacCu.MaNCC = NhaCungCap.MaNCC 
    JOIN LoaiNhacCu on LoaiNhacCu.MaLoaiNC = NhacCu.MaLoaiNC 
    JOIN HangSanXuat on HangSanXuat.MaHSX = NhacCu.MaHSX
    JOIN HoaDon ON HoaDon.MaHD = CTHoaDon.MaHD
    WHERE HoaDon.TinhTrangDonHang = CAST('1' AS BIT) 
        AND (HoaDon.NgayGH >= ISNULL(@FromDate, HoaDon.NgayGH) 
        AND HoaDon.NgayGH <= ISNULL(@ToDate, HoaDon.NgayGH))
    GROUP BY NhacCu.TenNC, NhacCu.MaNC, NhacCu.SoLuong, NhaCungCap.TenNCC, LoaiNhacCu.TenLoaiNC, HangSanXuat.TenHSX,NhacCu.DonGia;
END
select * from LoaiNhacCu