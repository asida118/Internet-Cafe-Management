--ALL THE QUERY CODE IS CREATED BY LEGIAHUY-185050861

CREATE DATABASE QuanLyInternetCafe
GO

USE QuanLyInternetCafe
GO

CREATE TABLE ComputerFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Máy chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Admin',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO
CREATE TABLE MenuCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Menu
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.MenuCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idComputer INT NOT NULL,
	status INT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán
	
	FOREIGN KEY (idComputer) REFERENCES dbo.ComputerFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Menu(id)
)
GO

INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'admin' , -- UserName - nvarchar(100)
          N'Administrator' , -- DisplayName - nvarchar(100)
          N'admin' , -- PassWord - nvarchar(1000)
          1  -- Type - int
        )
INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'staff' , -- UserName - nvarchar(100)
          N'staff' , -- DisplayName - nvarchar(100)
          N'1' , -- PassWord - nvarchar(1000)
          0  -- Type - int
        )
GO


SELECT * FROM dbo.Account WHERE UserName = N'admin' AND PassWord = N'admin' 



DECLARE @i INT = 0

WHILE @i <= 10
BEGIN
	INSERT dbo.ComputerFood ( name)VALUES  ( N'Máy ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO

CREATE PROC USP_GetComputerList
AS SELECT * FROM dbo.ComputerFood
GO
EXEC dbo.USP_GetComputerList
GO

SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo
SELECT * FROM dbo.Menu
SELECT * FROM dbo.MenuCategory

--thêm category
INSERT dbo.MenuCategory
        ( name )
VALUES  ( N'Nạp tiền'  -- name - nvarchar(100)
          )
INSERT dbo.MenuCategory
        ( name )
VALUES  ( N'Thức ăn' )
INSERT dbo.MenuCategory
        ( name )
VALUES  ( N'Giải khát' )
INSERT dbo.MenuCategory
        ( name )
VALUES  ( N'Snack' )
INSERT dbo.MenuCategory
        ( name )
VALUES  ( N'Nước đóng chai' )

-- thêm vào menu
	--nạp tiền
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nạp 1 giờ sử dụng', -- name - nvarchar(100)
          1, -- idCategory - int
          8000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nạp 2 giờ sử dụng', 1, 15000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nạp 3 giờ sử dụng', 1, 22000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nạp 5 giờ sử dụng', 1, 35000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nạp 10 giờ sử dụng', 1, 60000)

	--thức ăn

INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Cơm chiên dương châu', 2, 30000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Cơm chiên trứng', 2, 25000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Cơm gà', 2, 35000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Cơm xào bò', 2, 35000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Cơm thịt xá xíu', 2, 35000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Mì trứng xúc xích nước', 2, 25000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Mì trứng xúc xích khô', 2, 25000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Mì xào bò', 2, 30000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Mì ý', 2, 35000)

		--Giải khát
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Trà đá đường', 3, 10000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Cà phê đá', 3, 15000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Cà phê sữa', 3, 20000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nước cam', 3, 15000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nước sữa chua', 3, 15000)
		--Snack
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Oishi bí đỏ vị bò', 4, 8000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Oishi bắp ngọt', 4, 8000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Oishi hành tây chiên', 4, 8000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Oishi phô mai miếng', 4, 8000)
		--đóng chai
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Trà c2', 5, 10000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Trà lipton', 5, 12000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Trà Ô long', 5, 12000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Sting', 5, 10000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Red bull', 5, 12000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Trà drThanh', 5, 12000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Nước suối', 5, 7000)
INSERT dbo.Menu
        ( name, idCategory, price )
VALUES  ( N'Tẩy đá', 5, 2000)

-- thêm bill
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idComputer ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          3 , -- idComputer - int
          0  -- status - int
        )
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idComputer ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          4, -- idComputer - int
          0  -- status - int
        )
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idComputer ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          GETDATE() , -- DateCheckOut - date
          5 , -- idComputer - int
          1  -- status - int
        )
Go
select * from dbo.Menu
select * from dbo.Bill
select * from dbo.BillInfo
-- thêm bill info
INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
VALUES  ( 1, -- idBill - int
          1, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
VALUES  ( 1, -- idBill - int
          3, -- idFood - int
          4  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
VALUES  ( 2, -- idBill - int
          5, -- idFood - int
          1  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
VALUES  ( 2, -- idBill - int
          1, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
VALUES  ( 3, -- idBill - int
          6, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
VALUES  ( 3, -- idBill - int
          5, -- idFood - int
          2  -- count - int
          )             
GO

CREATE  PROC USP_InsertBill
@idComputer INt
AS
BEGIN
	INSERT dbo.Bill
		(	DateCheckIn,
			DateCheckOut,
			idComputer,
			status,
			discount
			)
	VALUES( GETDATE() ,
			NULL,
			@idComputer, 
			0,
			0)
END
GO

CREATE PROC USP_InsertBillInfo
@idBill INT, @idMenu INT, @count INT
AS
BEGIN
	INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
	VALUES  ( @idBill, -- idBill - int
			  @idMenu, -- idFood - int
			  @count  -- count - int
			  )             
END
GO

CREATE PROC USP_InsertBillInfo
@idBill INT, @idMenu INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1
	
	SELECT @isExitsBillInfo = id, @foodCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill = @idBill AND idMenu = @idMenu

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo	SET count = @foodCount + @count WHERE idMenu = @idMenu
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idMenu = @idMenu
	END
	ELSE
	BEGIN
		INSERT	dbo.BillInfo
        ( idBill, idMenu, count )
		VALUES  ( @idBill, -- idBill - int
          @idMenu, -- idFood - int
          @count  -- count - int
          )
	END
END
GO

CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = idBill FROM Inserted
	
	DECLARE @idComputer INT
	
	SELECT @idComputer = idComputer FROM dbo.Bill WHERE id = @idBill AND status = 0
	
	DECLARE @count INT
	SELECT @count = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill
	IF (@count > 0)
	BEGIN
	
		PRINT @idComputer
		PRINT @idBill
		PRINT @count
		
		UPDATE dbo.ComputerFood SET status = N'Có người' WHERE id = @idComputer		
		
	END
	ELSE
	BEGIN
	PRINT @idComputer
		PRINT @idBill
		PRINT @count
	UPDATE dbo.ComputerFood SET status = N'Trống' WHERE id = @idComputer	
	end
	
END


GO


CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = id FROM Inserted	
	
	DECLARE @idComputer INT
	
	SELECT @idComputer = idComputer FROM dbo.Bill WHERE id = @idBill
	
	DECLARE @count int = 0
	
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idComputer = @idComputer AND status = 0
	
	IF (@count = 0)
		UPDATE dbo.ComputerFood SET status = N'Trống' WHERE id = @idComputer
END
Go

alter table dbo.Bill
add discount int

UPDATE dbo.Bill SET discount = 0

GO

CREATE PROC USP_SwitchComputer
@idComputer1 INT, @idComputer2 int
AS BEGIN

	DECLARE @idFirstBill int
	DECLARE @idSeconrdBill INT
	
	DECLARE @isFirstComputerEmty INT = 1
	DECLARE @isSecondComputerEmty INT = 1
	
	
	SELECT @idSeconrdBill = id FROM dbo.Bill WHERE idComputer = @idComputer2 AND status = 0
	SELECT @idFirstBill = id FROM dbo.Bill WHERE idComputer = @idComputer1 AND status = 0
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'
	
	IF (@idFirstBill IS NULL)
	BEGIN
		PRINT '0000001'
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idComputer ,
		          status
		        )
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idComputer1 , -- idTable - int
		          0  -- status - int
		        )
		        
		SELECT @idFirstBill = MAX(id) FROM dbo.Bill WHERE idComputer = @idComputer1 AND status = 0
		
	END
	
	SELECT @isFirstComputerEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idFirstBill
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'
	
	IF (@idSeconrdBill IS NULL)
	BEGIN
		PRINT '0000002'
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idComputer ,
		          status
		        )
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idComputer2 , -- idTable - int
		          0  -- status - int
		        )
		SELECT @idSeconrdBill = MAX(id) FROM dbo.Bill WHERE idComputer = @idComputer2 AND status = 0
		
	END
	
	SELECT @isSecondComputerEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'

	SELECT id INTO IDBillInfoComputer FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	
	UPDATE dbo.BillInfo SET idBill = @idSeconrdBill WHERE idBill = @idFirstBill
	
	UPDATE dbo.BillInfo SET idBill = @idFirstBill WHERE id IN (SELECT * FROM IDBillInfoComputer)
	
	DROP TABLE IDBillInfoComputer
	
	IF (@isFirstComputerEmty = 0)
		UPDATE dbo.ComputerFood SET status = N'Trống' WHERE id = @idComputer2
		
	IF (@isSecondComputerEmty= 0)
		UPDATE dbo.ComputerFood SET status = N'Trống' WHERE id = @idComputer1
END
GO

alter table dbo.Bill add totalPrice float
GO

CREATE PROC USP_GetListBillByDate
@checkIn date, @checkOut date
AS
BEGIN
	SELECT t.name AS [Tên máy], b.totalPrice AS [Tổng tiền], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], discount AS [Giảm giá]
	FROM dbo.Bill AS b,dbo.ComputerFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idComputer
END

GO

CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password
	
	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END		
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
	end
END
GO

CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo FOR DELETE
AS 
BEGIN
	DECLARE @idBillInfo INT
	DECLARE @idBill INT
	SELECT @idBillInfo = id, @idBill = Deleted.idBill FROM Deleted
	
	DECLARE @idComputer INT
	SELECT @idComputer = idComputer FROM dbo.Bill WHERE id = @idBill
	
	DECLARE @count INT = 0
	
	SELECT @count = COUNT(*) FROM dbo.BillInfo AS bi, dbo.Bill AS b WHERE b.id = bi.idBill AND b.id = @idBill AND b.status = 0
	
	IF (@count = 0)
		UPDATE dbo.ComputerFood SET status = N'Trống' WHERE id = @idComputer
END
GO



