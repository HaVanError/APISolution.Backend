Use SolutitonAPIDB
GO
--CRUD CHO USER
ALTER  PROCEDURE AddUser
    @Email NVARCHAR(max),
    @Password NVARCHAR(max),
    @Name nvarchar(450),
    @Address NVARCHAR(max),
    @City NVARCHAR(max),
    @IdRole INT
as
BEGIN
 SET NOCOUNT ON;
	DECLARE @soluongName int ;
		DECLARE @soluongEmail int ;
	SELECT @soluongName= COUNT(*)
	FROM UserInformation AS U
	WHERE Name =@Name 
	SELECT @soluongEmail= COUNT(*)
	FROM UserInformation AS U
		WHERE Email =@Email 
		if @soluongName >0 
		BEGIN 
		  RAISERROR ('Tên đã tồn tại. Không thể chèn dữ liệu.', 16, 1);
		END 
		if @soluongEmail >0 
		BEGIN 
		  RAISERROR ('Email đã tồn tại. Không thể chèn dữ liệu.', 16, 1);
		END 
		ELSE 
    INSERT INTO dbo.UserInformation(Name,Email,Password,Address, City, IdRole)
    VALUES (@Name,@Email, @Password, @Address, @City, @IdRole);
    

END
go
CREATE PROCEDURE UpdateUser
	@Id int,
	@Email NVARCHAR(max),
    @Password NVARCHAR(max),
    @Name nvarchar(450),
    @Address NVARCHAR(max),
    @City NVARCHAR(max),
    @IdRole INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE UserInformation
    SET Email = @Email,
		Password = @Password,
        Name = @Name,
        Address = @Address,
        City = @City,
        IdRole = @IdRole
    WHERE Id = @Id;
END
go
CREATE PROCEDURE DeleteUser
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
	
    DELETE FROM UserInformation
    WHERE Id = @Id;
    
END
go 
CREATE PROCEDURE GetUserByName
    @Name nvarchar(450)
AS
BEGIN
SET NOCOUNT ON;
    SELECT *
    FROM UserInformation
   WHERE Name LIKE '%' + @Name + '%';
END

go

ALTER PROCEDURE GetAllUser 
	@PageNumber INT,
    @PageSize INT
AS 
BEGIN
	SET NOCOUNT ON;
	DECLARE @Offset INT;
    SET @Offset = (@PageNumber - 1) * @PageSize;
	
  select 
	p.Id,
  p.Name,
  p.Address,
  p.City,
  p.Email,
  p.Password,
  p.IdRole,
  c.NameRole,
  c.MoTa
  from (
	SELECT 
	Id,
	Name,
	Email , 
	Password , 
	Address , 
	City ,
	IdRole,
	ROW_NUMBER() OVER (ORDER BY Id ) AS RowNum
	FROM dbo.UserInformation) as p join dbo.Quyen as c  on  p.IdRole =c.IdRole
	WHERE p.RowNum > @Offset
    AND p.RowNum <= (@Offset + @PageSize);
END

GO


GO
--STORED PROCEDURE CRUD QUYÊN
ALTER PROCEDURE AddQuyen
@NameRole nvarchar(max),
@MoTa nvarchar(max)
AS 
BEGIN 
SET NOCOUNT ON;
	Declare @SoluongName int ; 
	SELECT @SoluongName = COUNT(Q.NameRole)
	FROM Quyen AS Q
	WHERE NameRole = @NameRole
	IF @SoluongName >0 
	BEGIN 
	print (N'TÊN ROLE ĐÃ TỒN TẠI')
	END 
	ELSE 
	INSERT INTO dbo.Quyen(NameRole , MoTa)
	VALUES (@NameRole , @MoTa);
END 
go
CREATE PROCEDURE UpdateQuyen
@IdQuyen int,
@NameRole nvarchar(max),
@MoTa nvarchar(max) 
AS 
BEGIN 
SET NOCOUNT ON;
UPDATE dbo.Quyen 
	SET NameRole = @NameRole ,
		MoTa = @MoTa
		 WHERE IdRole= @IdQuyen;
	END
GO
CREATE PROCEDURE DeleteQuyen
@Id int 
AS 
BEGIN 
SET NOCOUNT ON ; 
	DELETE dbo.Quyen 
		WHERE IdRole = @Id
END
---STORED PROCEDURE CRUD LoaiPhong
go
create Procedure GetAllQuyen
@PageNumber INT,
    @PageSize INT
AS 
BEGIN
	SET NOCOUNT ON;
	DECLARE @Offset INT;
    SET @Offset = (@PageNumber - 1) * @PageSize;
	
   SELECT * FROM dbo.Quyen
    ORDER BY IdRole
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
    
    SELECT COUNT(*) AS TotalCount FROM dbo.Quyen;
END
go
--CRUD CHO LOAI PHONG 
 

ALTER PROCEDURE AddLoaiPhong	
@Name nvarchar(max),
@Mota  nvarchar(max)
AS 
BEGIN 
SET NOCOUNT ON ; 
	DECLARE @count INT;
	SELECT @count = COUNT(*)
    FROM LoaiPhong
    WHERE Name = @Name;
	if @count> 0 
	Begin
	   RAISERROR ('Tên đã tồn tại. Không thể chèn dữ liệu.', 16, 1);
    END
	else
	INSERT INTO dbo.LoaiPhong (Name , MoTa)
		Values (@Name,@Mota)
END 
GO 
ALTER PROCEDURE UpdateLoaiPhong
@Id int,
@Name nvarchar(max),
@Mota nvarchar(max)
AS 
BEGIN 
SET NOCOUNT ON ; 
	UPDATE dbo.LoaiPhong
	SET Name = @Name ,
		Mota = @Mota
		WHERE IdLoaiPhong = @Id
END
GO
CREATE PROCEDURE GetByIdLoaiPhong 
@Id int 
AS 
BEGIN
SELECT L.Name ,L.MoTa FROM dbo.LoaiPhong AS L WHERE IdLoaiPhong =@Id
END 
GO 

ALTER PROCEDURE DeleteLoaiPhong
	@Id int
AS 
BEGIN 
SET NOCOUNT ON;
	DELETE dbo.LoaiPhong
	WHERE IdLoaiPhong = @Id
	End
GO
CREATE PROCEDURE GetAllLoaiPhong
AS 
BEGIN 
	SELECT L.Name,L.MoTa
	FROM dbo.LoaiPhong as L
END
Go
--- CRUD CHO PHONG 
GO
ALTER PROCEDURE AddPhong
@Name nvarchar(max),
@Mota nvarchar(max),
@IdLoaiPhong int,
@GiaPhong float
AS 
BEGIN
SET NOCOUNT ON ; 
	DECLARE @SoLuongTenPhong int ;
	Select @SoLuongTenPhong = Count(*)
	FROM Phong AS P
	WHERE Name = @Name;
	if @SoLuongTenPhong >0
	BEGIN
	print 'TÊN PHÒNG ĐÃ TỒN TẠI'
	END 
	ELSE 
	BEGIN 
	INSERT INTO dbo.PHONG (Name,Describe,IdLoaiPhong,StatusPhong,GiaPhong)
	VALUES (@Name,@Mota,@IdLoaiPhong,0,@GiaPhong)
	End
END

GO
CREATE PROCEDURE UpdatePhong
@Id int,
@Name nvarchar(max),
@Mota nvarchar(max),
@IdLoaiPhong int,
@status bit,
@GiaPhong float
AS 
BEGIN 
SET NOCOUNT ON ; 

UPDATE Phong 
SET Name = @Name ,
	Describe = @Mota ,
	GiaPhong = @GiaPhong ,
	IdLoaiPhong = @IdLoaiPhong , 
	StatusPhong = @status
	WHERE IdPhong = @Id
	END 
GO
create PROCEDURE GetAllPhong
	@PageNumber INT,
    @PageSize INT
AS 
BEGIN
	SET NOCOUNT ON;
	DECLARE @Offset INT;
    SET @Offset = (@PageNumber - 1) * @PageSize;
	
  select 
	p.Name,
	p.GiaPhong,
	p.Describe,
	p.StatusPhong,
	p.IdLoaiPhong,
	c.Name


  from(
	SELECT 
	IdPhong,
	Name,
	Describe , 
	IdLoaiPhong, 
	StatusPhong, 
	GiaPhong,
	ROW_NUMBER() OVER (ORDER BY IdPhong ) AS RowNum
	FROM dbo.Phong) as p join dbo.LoaiPhong as c  on  p.IdLoaiPhong =c.IdLoaiPhong
	   WHERE p.RowNum > @Offset
    AND p.RowNum <= (@Offset + @PageSize);
END
GO
ALTER PROCEDURE GetbyIdPhong
@Id int
AS 
BEGIN
	SELECT
	p.IdPhong,
	p.Name,
	p.Describe,
	p.StatusPhong,
	p.GiaPhong,
	p.IdLoaiPhong
	FROM Phong  as p
	WHERE IdPhong = @Id
END
GO
CREATE PROCEDURE DeletePhong
@Id int 
AS 
BEGIN 
SET NOCOUNT ON ;
DELETE dbo.Phong WHERE IdPhong= @Id
END
GO



 -- CRUD CHO DICH VU 
 CREATE PROCEDURE AddDichVu
 @NameDichVu nvarchar(max),
 @SoLuong int ,
 @Gia float
 AS 
 BEGIN 
		DECLARE @KiemTra int; 
		SELECT @KiemTra = COUNT(*)
		FROM DichVu 
		WHERE NameDichVu =@NameDichVu
		IF(@KiemTra > 0)
		
		BEGIN
		PRINT (N'ĐÃ TỒN TẠI TÊN DỊCH VỤ RỒI')
		END
		ELSE 
		BEGIN 
		INSERT INTO dbo.DichVu (NameDichVu,SoLuong,Gia)
		Values (@NameDichVu, @SoLuong, @Gia)

		End
 END
 Go 
 CREATE PROCEDURE UpdateDichVu
 @Id int,
 @NameDichVu nvarchar(max),
 @SoLuong int ,
 @Gia float
 AS 
 BEGIN 
		UPDATE DichVu 
		Set 
		NameDichVu = @NameDichVu ,
		SoLuong  = @SoLuong ,
		Gia = @Gia 
		WHERE IdDichVu = @Id
 END
 GO
 CREATE PROCEDURE DeleteDichVu
 @Id int 
 AS 
 BEGIN 
		DELETE DichVu WHERE IdDichVu = @Id
 END
 GO
 ALTER PROCEDURE GetByIdDichVu
 @Id int 
 AS 
 BEGIN
		SELECT D.IdDichVu, D.NameDichVu , D.SoLuong,D.Gia FROM DichVu AS D WHERE IdDichVu = @Id
 END 
GO
 --CRUD CHO PHIEUDATPHONG
 
ALTER PROCEDURE AddPhieuDatDichVu
    @NameDichVu NVARCHAR(MAX),
    @idDichVu INT,
    @idPhieuDatPhong INT,
    @SoLuong INT,
    @NgayDatDichVu DATE 
AS
BEGIN
    SET NOCOUNT ON; 

    -- Kiểm tra sự tồn tại của PhieuDatPhong và DichVu
    IF Not EXISTS (SELECT 1 FROM PhieuDatPhong WHERE IdPhieuDatPhong = @idPhieuDatPhong)
    BEGIN
        PRINT N'Không tồn tại khách hàng tại phòng đã đặt';
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM DichVu WHERE IdDichVu = @idDichVu)
    BEGIN
        PRINT N'Không tồn tại ID dịch vụ';
        RETURN;
    END

    -- Bắt đầu transaction để đảm bảo tính nhất quán và toàn vẹn dữ liệu
    BEGIN TRANSACTION;

    DECLARE @SoLuongConLai INT;
    DECLARE @NgayDatDichVuConverted DATETIME;

    -- Chuyển đổi @NgayDatDichVu từ chuỗi sang kiểu ngày tháng
    SET @NgayDatDichVuConverted = CONVERT(DATETIME, @NgayDatDichVu, 103); -- 103 là định dạng dd/MM/yyyy

    -- Lấy số lượng hiện tại của dịch vụ
    SELECT @SoLuongConLai = SoLuong
    FROM DichVu
    WHERE IdDichVu = @idDichVu;

    -- Kiểm tra xem có đủ số lượng để giảm không
    IF @SoLuongConLai >= @SoLuong
    BEGIN
        -- Chèn dữ liệu vào PhieuDatDichVu
        INSERT INTO PhieuDatDichVu (TenPhong, TenKhachHang, SoLuong, TenDichVu, IdDichVu, IdPhieuDatPhong, Gia, ThanhTien, PhongIdPhong, NgayDatDichVu)
        SELECT 
            pp.TenPhong,
            pp.TenNguoiDat,
            @SoLuong,
            @NameDichVu,
            @idDichVu,
            @idPhieuDatPhong,
            dv.Gia,
            (dv.Gia * @SoLuong) AS ThanhTien, -- Tính ThanhTien
            pp.IdPhong,
            @NgayDatDichVuConverted
        FROM 
            PhieuDatPhong AS pp
         JOIN 
            DichVu AS dv ON dv.IdDichVu = @idDichVu
        WHERE 
            pp.IdPhieuDatPhong = @idPhieuDatPhong;
			
        UPDATE DichVu
        SET SoLuong = SoLuong - @SoLuong
        WHERE IdDichVu = @idDichVu;

        -- Commit transaction nếu thành công
        COMMIT TRANSACTION;
    END
    ELSE
    BEGIN
        -- Rollback transaction nếu không đủ số lượng
        ROLLBACK TRANSACTION;
        PRINT N'Không đủ số lượng dịch vụ để đáp ứng yêu cầu';
    END
END;
GO


ALTER PROCEDURE DeletePhieuDatDichVu
    @id int
AS
BEGIN
    DELETE PhieuDatDichVu WHERE IdPhieuDichVu  = @id
END;

GO
CREATE PROCEDURE UpdatePhieuDatDichVu
    @idPhieuDatDichVu INT,
    @NameDichVu NVARCHAR(MAX),
    @idDichVu INT,
    @idPhieuDatPhong INT,
    @SoLuong INT
AS
BEGIN
    DECLARE @OldSoLuong INT;
    DECLARE @OldIdDichVu INT;
    
    -- Bắt đầu transaction
    BEGIN TRANSACTION;
    
    -- Lấy thông tin cũ từ phiếu đặt dịch vụ
    SELECT @OldSoLuong = SoLuong, @OldIdDichVu = IdDichVu
    FROM PhieuDatDichVu
    WHERE IdPhieuDichVu = @idPhieuDatDichVu;
    
    -- Kiểm tra nếu phiếu đặt dịch vụ tồn tại
    IF @OldSoLuong IS NOT NULL AND @OldIdDichVu IS NOT NULL
    BEGIN
        -- Tăng số lượng dịch vụ cũ trong bảng DichVu
        UPDATE DichVu
        SET SoLuong = SoLuong + @OldSoLuong
        WHERE IdDichVu = @OldIdDichVu;

        -- Cập nhật phiếu đặt dịch vụ
        UPDATE PhieuDatDichVu
        SET TenDichVu = @NameDichVu,
            IdDichVu = @idDichVu,
            IdPhieuDatPhong = @idPhieuDatPhong,
            SoLuong = @SoLuong
        WHERE IdPhieuDichVu = @idPhieuDatDichVu;

        -- Giảm số lượng dịch vụ mới trong bảng DichVu
        UPDATE DichVu
        SET SoLuong = SoLuong - @SoLuong
        WHERE IdDichVu = @idDichVu;
        
        -- Commit transaction nếu thành công
        COMMIT TRANSACTION;
    END
    ELSE
    BEGIN
        -- Rollback transaction nếu không tìm thấy phiếu đặt dịch vụ
        ROLLBACK TRANSACTION;
        PRINT N'Không tìm thấy phiếu đặt dịch vụ để cập nhật';
    END
END;



 select * from PhieuDatDichVu
  select * from DichVu
    select * from PhieuDatPhong
 --EXEC DeletePhieuDatDichVu 3
 go


 --CRUD PHIEUDATPHONG
ALTER PROCEDURE [dbo].[AddPhieuDatPhong]
    @TenNguoiDat nvarchar(max),
    @SDT nvarchar(max),
    @IdPhong int,
    @NgayDatPhong Date -- Định dạng dd/MM/yyyy
AS
BEGIN
    SET NOCOUNT ON; -- Ngăn không trả về số bản ghi ảnh hưởng

    -- Kiểm tra nếu phòng không tồn tại hoặc đang có trạng thái là 1
    IF not EXISTS (
        SELECT 
		1
        FROM Phong
        WHERE IdPhong = @IdPhong
    )
    BEGIN
        PRINT N'Không tồn tại phòng hoặc phòng đã có người đặt.';
        RETURN;
    END

    -- Bắt đầu transaction để đảm bảo tính nhất quán và toàn vẹn dữ liệu
    BEGIN TRANSACTION;

    -- Chuyển đổi @NgayDatPhong từ chuỗi sang kiểu ngày tháng
    DECLARE @NgayDatPhongConverted datetime;
    SET @NgayDatPhongConverted = CONVERT(datetime, @NgayDatPhong, 103); -- 103 là định dạng dd/MM/yyyy

    INSERT INTO PhieuDatPhong (TenPhong, GiaPhong, TenNguoiDat, SoDienThoai, Status, IdPhong, NgayDatPhong)
    SELECT 
        p.Name,
        p.GiaPhong,
        @TenNguoiDat,
        @SDT,
        1, -- Status = 1
        @IdPhong,
        @NgayDatPhongConverted
    FROM 
        Phong AS p
    WHERE 
        p.IdPhong = @IdPhong;

    COMMIT TRANSACTION;

    PRINT N'Đã thêm phiếu đặt phòng thành công.';
END
exec AddPhieuDatPhong  N'Nguyen van b','92312',1



go
ALTER PROCEDURE AddThanhToan 
@IdPhieuDatPhong int,
@NgayTraPhong DATE 
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM ThanhToan 
        WHERE idPhieuDatPhong = @IdPhieuDatPhong
    )
    BEGIN 
        PRINT N'KHÔNG THỂ THANH TOÁN LỖI';
    END
    ELSE
    BEGIN
        -- Bắt đầu transaction để đảm bảo tính nhất quán và toàn vẹn dữ liệu
        BEGIN TRANSACTION;

        -- Khai báo biến
        DECLARE @NgayNhanPhong DATE;
        DECLARE @TongThanhTien MONEY;
        DECLARE @SoNgayO INT;

        -- Lấy thông tin ngày nhận phòng từ bảng PhieuDatPhong
        SELECT @NgayNhanPhong = p.NgayDatPhong
        FROM PhieuDatPhong as p
        WHERE idphieudatphong = @IdPhieuDatPhong;

		  SET @SoNgayO = DATEDIFF(DAY, @NgayNhanPhong, @NgayTraPhong);
        -- Tính tổng thành tiền
        SELECT 
            @TongThanhTien = ((P.GiaPhong*@SoNgayO) + ISNULL(SUM(D.SoLuong * D.Gia), 0)) 
        FROM 
            PhieuDatPhong AS P
            JOIN PhieuDatDichVu AS D ON P.IdPhong = D.PhongIdPhong
        WHERE 
            P.idphieudatphong = @IdPhieuDatPhong
        GROUP BY 
            P.GiaPhong;
        -- Insert vào bảng ThanhToan
        INSERT INTO ThanhToan (TenPhong, TenKhachHang, TongThanhTien, idPhieuDatPhong, TrangThaiThanhToan, NgayThanhToan, NgayTraPhong)
        SELECT 
            P.TenPhong,
            P.TenNguoiDat AS TenKhachHang,
            @TongThanhTien AS TongThanhTien,
            P.IdPhieuDatPhong,
            2,
            GETDATE() AS NgayThanhToan, -- Ngày hiện tại là ngày thanh toán
			@NgayTraPhong
        FROM 
            PhieuDatPhong AS P
        WHERE 
            P.idphieudatphong = @IdPhieuDatPhong;

        COMMIT TRANSACTION;
    END
END;
go
ALTER PROCEDURE [dbo].[DuyetThanhToan]
    @IdThanhToan int 
AS 
BEGIN 
    IF EXISTS (
        SELECT IdThanhToan
        FROM ThanhToan 
        WHERE IdThanhToan = @IdThanhToan
    )
    BEGIN
        UPDATE ThanhToan 
        SET TrangThaiThanhToan = 1
        WHERE IdThanhToan = @IdThanhToan;
        
        PRINT N'Đã duyệt thanh toán thành công.';
		Update Phong 
		set 
		StatusPhong = 0
		where Phong.Name = (SELECT TenPhong FROM ThanhToan WHERE IdThanhToan = @IdThanhToan);
		  PRINT N'Đã cập nhật trạng thái của phòng thành công.';
		DELETE FROM PhieuDatDichVu 
        WHERE IdPhieuDatPhong = (select IdPhieuDatPhong FROM ThanhToan WHERE IdThanhToan = @IdThanhToan); 
		 PRINT N'Đã xóa PHIEU DICH VU thành công.';
		 DELETE FROM PhieuDatPhong  
		WHERE PhieuDatPhong.IdPhieuDatPhong = (SELECT IdPhieuDatPhong FROM ThanhToan WHERE IdThanhToan = @IdThanhToan);
			  PRINT N'Đã xóa PhieuDatPhong thành công.';
	UPDATE ThanhToan
	SET idPhieuDatPhong = NULL
	WHERE IdThanhToan = @IdThanhToan;

	
    END
    ELSE
    BEGIN
        PRINT N'Không tồn tại Id thanh toán.';
    END
END
go
 SET STATISTICS time On;
 EXEC AddPhieuDatDichVu 
    @NameDichVu = N'123',  -- Thay thế bằng tên dịch vụ cần chèn
    @idDichVu = 1,  -- Thay thế bằng ID dịch vụ cần chèn
    @idPhieuDatPhong = 1,  -- Thay thế bằng ID phiếu đặt phòng
    @SoLuong = 1;  
	
 SET STATISTICS time OFF;
 EXEC UpdatePhieuDatDichVu
	@idPhieuDatDichVu = 5,
    @NameDichVu = N'123',  
    @idDichVu = 1, 
    @idPhieuDatPhong = 1, 
    @SoLuong = 4;  

	go

SET STATISTICS IO On;
--TEXT PHẦN USER
EXEC AddUser @Name='121', @Email='11',@Password='1',@Address='12',@City='12',@IdRole=1;
select * from UserInformation
exec DeleteUser @Id = 35
select * from UserInformation
EXEC UpdateUser @Id = 35 ,@Name='11', @Email='11',@Password='11',@Address='11',@City='11',@IdRole=1;
select * from UserInformation

--TEST PHẦN QUYỀN
EXEC AddQuyen @NameRole = 'Admin',@MoTa=N'QUẢN TRỊ HỆ THỐNG'
EXEC UpdateQuyen @IdQuyen = 1 , @NameRole = N'User' , @MoTa = N'NGƯỜI DÙNG TRONG HỆ THỐNG '
EXEC AddQuyen @NameRole = 'Test',@MoTa=N'test thử delete'
EXEC DeleteQuyen @Id  = 3
EXEC AddQuyen @NameRole = 'Admin',@MoTa=N'QUẢN TRỊ HỆ THỐNG'

SELECT * FROM Quyen 
--TEST LOAI Phong
EXEC AddLoaiPhong N'THƯỜNG',N'PHÒNG THƯỜNG'
EXEC UpdateLoaiPhong 1,'VIP',N'PHÒNG VIP'
EXEC DeleteLoaiPhong 2
EXEC GetAllLoaiPhong 
EXEC GetByIdLoaiPhong 3
SELECT * FROM LoaiPhong

--TEST PHONG
EXEC AddPhong N'C1',N'PHÒNG SỐ c1',1,2000
EXEC UpdatePhong 4,N'C1',N'PHÒNG SỐ C1',1,1,2000
EXEC GetAllPhong 1,2
EXEC DeletePhong 5
EXEC GetbyIdPhong  4
SELECT * FROM Phong
SET STATISTICS IO On;
--SELECT * FROM GHEP_PHONG_LOAI
select * from LoaiPhong 
where LoaiPhong.Name=N'THƯỜNG'

--- TEST CHO DỊCH VỤ 
EXEC AddDichVu N'MỰC KHÔsa',2,12223
EXEC UpdateDichVu 2,N'PESSI',2,2222
EXEC DeleteDichVu 5
EXEC GetByIdDichVu 2
 

select * from Phong
select * from DichVu
select * from ThanhToan
select * from PhieuDatDichVu
select * from PhieuDatPhong
exec AddPhieuDatPhong  N'Nguyen van bc','92311232',3


 EXEC AddPhieuDatDichVu 
    @NameDichVu = N'1',  -- Thay thế bằng tên dịch vụ cần chèn
    @idDichVu = 1,  -- Thay thế bằng ID dịch vụ cần chèn
    @idPhieuDatPhong = 1,  -- Thay thế bằng ID phiếu đặt phòng
    @SoLuong = 1;  
	
SET STATISTICS IO OFF;

go
-- tesst logic


select * from PhieuDatPhong
select * from thanhtoan
select * from Phieudatdichvu
select * from phong

exec AddThanhToan 6,'7/7/2024'
exec DuyetThanhToan 3
exec AddPhieuDatPhong  N'Nguyen van b','92312',3,'2024/7/3'
exec AddPhieuDatDichVu N'Sting',1,6,1,'2024/7/3'
exec AddPhieuDatDichVu N'Sting',1,2,1,'3/7/2024'
