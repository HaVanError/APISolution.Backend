Use SolutitonAPIDB -- DATABASE 
GO

---DANH SÁCH CÁC STORED PROCEDURE CRUD 

--CRUD CHO USER
ALTER  PROCEDURE AddUser
    @Email NVARCHAR(max),
    @Password NVARCHAR(max),
    @Name nvarchar(450),
    @Address NVARCHAR(max),
    @City NVARCHAR(max),
    @IdRole INT
AS
BEGIN
   -- SET NOCOUNT off; -- Ngăn SQL Server trả về số hàng bị ảnh hưởng
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
    
 --   SELECT @@ROWCOUNT AS RowsAffected; -- Trả về số lượng bản ghi bị xóa
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
ALTER TRIGGER trg_UserInformation_AfterInsertUpdateDelete
ON dbo.UserInformation
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
  --  SET NOCOUNT ON;

    DECLARE @ActionType VARCHAR(10);

    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        IF EXISTS (SELECT * FROM deleted)
            SET @ActionType = 'Update';
        ELSE
            SET @ActionType = 'AddUser';

        -- Thực hiện stored procedure InsertUser khi có sự thêm mới
        IF @ActionType = 'AddUser'
        BEGIN
		DECLARE @Email NVARCHAR(max);
		DECLARE @Password NVARCHAR(max);
		DECLARE	@Name nvarchar(450);
		DECLARE @Address NVARCHAR(max);
		DECLARE @City NVARCHAR(max);
		DECLARE @IdRole INT
          SELECT @Name =Name, @Email = Email,@Password = Password , @Address = Address , @City =City ,@IdRole = IdRole FROM inserted;
       --  EXEC AddUser @Name, @Email,@Password,@Address,@City,@IdRole From inserted;
		    PRINT N'Đã thực hiện thêm mới vào bảng UserInformation.';
        END;
    END
    ELSE IF EXISTS (SELECT * FROM deleted)
    BEGIN
        SET @ActionType = 'DeleteUser';

        -- Thực hiện stored procedure DeleteUser khi có sự xóa
        DECLARE @UserId INT;
        SELECT @UserId = Id FROM deleted;

        EXEC DeleteUser @UserId;
		PRINT N'Đã thực hiện Delete vào bảng UserInformation.';  
    END
   ELSE 
    BEGIN
	Set @ActionType = 'UpdateUser';
        -- Thực hiện cập nhật
		 DECLARE @Id INT;
		   SELECT @Id=Id, @Name =Name, @Email = Email,@Password = Password , @Address = Address , @City =City ,@IdRole = IdRole FROM deleted;

		PRINT N'Đã thực hiện update vào bảng UserInformation.';
    END;
END;

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
 exec GetAllQuyen 1,100
 go
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
select * from Phong
GO
ALTER PROCEDURE AddPhong
@Name nvarchar(max),
@Mota nvarchar(max),
@IdLoaiPhong int,
@status bit,
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
Alter VIEW GHEP_PHONG_LOAI as 
SELECT  
U.IdPhong,
U.Name,
U.Describe,
U.GiaPhong,
U.StatusPhong,
U.IdLoaiPhong,
Q.Name as tenPhong,
Q.MoTa
FROM dbo.Phong AS U 
JOIN dbo.LoaiPhong AS Q  ON U.IdLoaiPhong = Q.IdLoaiPhong

go
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
 
 Go
 select * from PhieuDatDichVu

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
EXEC AddPhong N'C11',N'PHÒNG SỐ C10',3,1,2000
EXEC UpdatePhong 4,N'C1',N'PHÒNG SỐ C1',1,1,2000
EXEC GetAllPhong 1,2
EXEC DeletePhong 6
EXEC GetbyIdPhong  4
SELECT * FROM Phong
SET STATISTICS IO On;
--SELECT * FROM GHEP_PHONG_LOAI
select * from LoaiPhong 
where LoaiPhong.Name=N'THƯỜNG'

--- TEST CHO DỊCH VỤ 
EXEC AddDichVu N'MỰC KHÔsa',2,12223
EXEC UpdateDichVu 2,N'PESSI',2,2222
EXEC DeleteDichVu 3
EXEC GetByIdDichVu 2


SET STATISTICS IO OFF;




