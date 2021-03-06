USE [master]
GO
/****** Object:  Database [Aware]    Script Date: 2015-05-19 16:10:46 ******/
CREATE DATABASE [Aware]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Aware', FILENAME = N'C:\Users\Alexandra\Aware.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Aware_log', FILENAME = N'C:\Users\Alexandra\Aware_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Aware] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Aware].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Aware] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Aware] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Aware] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Aware] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Aware] SET ARITHABORT OFF 
GO
ALTER DATABASE [Aware] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Aware] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Aware] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Aware] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Aware] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Aware] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Aware] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Aware] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Aware] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Aware] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Aware] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Aware] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Aware] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Aware] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Aware] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Aware] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Aware] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Aware] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Aware] SET  MULTI_USER 
GO
ALTER DATABASE [Aware] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Aware] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Aware] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Aware] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Aware]
GO
/****** Object:  Table [dbo].[Authentication]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Authentication](
	[Password] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[AddressLine1] [varchar](50) NOT NULL,
	[AddressLine2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[Region] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Exceptions]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Exceptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Exception_Type] [varchar](30) NOT NULL,
	[Message] [varchar](1024) NOT NULL,
	[Source] [varchar](128) NOT NULL,
	[Stacktrace] [varchar](2048) NOT NULL,
	[Time] [datetime] NOT NULL CONSTRAINT [DF_Exceptions_Time]  DEFAULT (getdate()),
 CONSTRAINT [PK_Exceptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderRows]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderRows](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_OrderRows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[OrderStatus] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[PaymentStatus] [int] NULL,
	[PaymentMethod] [varchar](50) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[SKU] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Weight] [decimal](10, 4) NULL,
	[StorageSpace] [varchar](15) NULL,
	[EAN] [varchar](30) NULL,
	[ImageLocation] [varchar](128) NULL,
	[LastInventory] [datetime] NULL DEFAULT ((0)),
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[OrderRows]  WITH CHECK ADD  CONSTRAINT [FK_OrderRows_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderRows] CHECK CONSTRAINT [FK_OrderRows_Orders]
GO
ALTER TABLE [dbo].[OrderRows]  WITH CHECK ADD  CONSTRAINT [FK_OrderRows_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[OrderRows] CHECK CONSTRAINT [FK_OrderRows_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_OrderStatus] FOREIGN KEY([OrderStatus])
REFERENCES [dbo].[OrderStatus] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_OrderStatus]
GO
/****** Object:  StoredProcedure [dbo].[usp_Authenticate]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Authenticate]
@Password varchar(50) = ''
AS
	BEGIN
		SELECT *
		FROM Authentication
		WHERE @Password = Password
		RETURN @@ROWCOUNT
	/*	IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)*/
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_CheckIfProductBusy]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CheckIfProductBusy]
@ProductId int = 0
AS

BEGIN
	SELECT *
	FROM OrderRows
	INNER JOIN Orders
	ON Orders.OrderId = OrderRows.OrderId
	WHERE OrderRows.ProductId = @ProductId AND Orders.OrderStatus = 2

/*	IF @@ROWCOUNT = 0
		RAISERROR('Order with this Id dosent exist.', 16,1)
		RETURN(1) */
END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetCustomer]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetCustomer]
@Id int = 0
AS
	BEGIN
		SELECT Id, CustomerId, FirstName, LastName, AddressLine1, AddressLine2, City, Region, ZipCode, Country, PhoneNumber, Email
		FROM Customers
		WHERE Id = @Id;
		IF @@ROWCOUNT = 0
			RAISERROR('Customer with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetOrder]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetOrder]
@Id int = 0
AS
IF @Id = 0
	BEGIN
		SELECT Id, OrderId, CustomerId, OrderStatus, Date, LastUpdate, PaymentStatus, PaymentMethod
		FROM Orders
	END
ELSE
	BEGIN
		SELECT Id, OrderId, CustomerId, OrderStatus, Date, LastUpdate, PaymentStatus, PaymentMethod
		FROM Orders
		WHERE Id = @Id;
		IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetOrderRow]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetOrderRow]
@Id int = 0
AS
IF @Id = 0
	BEGIN
		SELECT Id, OrderId, ProductId, Amount
		FROM OrderRows
	END
ELSE
	BEGIN
		SELECT Id, OrderId, ProductId, Amount
		FROM OrderRows
		WHERE Id = @Id;
		IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetOrderRowsByOrderId]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetOrderRowsByOrderId]
@OrderId int = 0
AS
IF @OrderId = 0
	BEGIN
		SELECT Id, OrderId, ProductId, Amount
		FROM OrderRows
	END
ELSE
	BEGIN
		SELECT Id, OrderId, ProductId, Amount
		FROM OrderRows
		WHERE OrderId = @OrderId;
		IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetOrderStatus]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetOrderStatus]
@Id int = 0
AS
	BEGIN
		SELECT Id, Status
		FROM OrderStatus
		WHERE Id = @Id;
		IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetOrderStatusByOrderId]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetOrderStatusByOrderId]
@OrderId int = 0
AS
IF @OrderId = 0
	BEGIN
		SELECT OrderStatus
		FROM Orders
	END
ELSE
	BEGIN
		SELECT OrderStatus
		FROM Orders
		WHERE Id = @OrderId;
		IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetProduct]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetProduct]
@Id int = 0
AS
IF @Id = 0
	BEGIN
		SELECT Id, Name, SKU, Quantity, Weight, StorageSpace, EAN, ImageLocation, LastInventory
		FROM Products
	END
ELSE
	BEGIN
		SELECT Id, Name, SKU, Quantity, Weight, StorageSpace, EAN, ImageLocation, LastInventory
		FROM Products
		WHERE Id = @Id;
		IF @@ROWCOUNT = 0
			RAISERROR('Product with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetProductByEAN]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetProductByEAN]
@Ean int = 0
AS
	BEGIN
		SELECT Id, Name, SKU, Quantity, Weight, StorageSpace, EAN, ImageLocation, LastInventory
		FROM Products
		WHERE EAN = @Ean;
		IF @@ROWCOUNT = 0
			RAISERROR('Product with this Barcodenumber dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetProductsQuantitySum]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetProductsQuantitySum]
AS
BEGIN
	SELECT SUM(Quantity)
	FROM Products
END


GO
/****** Object:  StoredProcedure [dbo].[usp_InsertAndUpdateCustomer]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertAndUpdateCustomer]
	@CustomerId int = 0,
	@FirstName varchar(50) = '',
	@LastName varchar(50) = '',
	@AddressLine1 varchar(50) = '',
	@AddressLine2 varchar(50) = '',
	@City varchar(50) = '',
	@Region varchar(50) = '',
	@ZipCode varchar(50) = '',
	@Country varchar(50) = '',
	@PhoneNumber varchar(20) = '',
	@Email varchar(50) = ''
AS
DECLARE @Id AS INT
SELECT @Id = Id FROM Customers WHERE CustomerId = @CustomerId
IF @Id IS NULL
	BEGIN
			INSERT INTO Customers(CustomerId, FirstName, LastName, AddressLine1, AddressLine2,
			City, Region, ZipCode, Country, PhoneNumber, Email)
			VALUES (@CustomerId, @FirstName, @LastName, @AddressLine1, @AddressLine2,
			@City, @Region, @ZipCode, @Country, @PhoneNumber, @Email);
	END
ELSE
	BEGIN
		UPDATE Customers
		SET CustomerId = @CustomerId, FirstName = @FirstName, LastName = @LastName, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2,
		City = @City, Region = @Region, ZipCode = @ZipCode, Country = @Country, PhoneNumber = @PhoneNumber, Email = @Email
		WHERE CustomerId = @CustomerId
		IF @@ROWCOUNT = 0
			RAISERROR('Customer with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_InsertAndUpdateOrder]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertAndUpdateOrder]
	@OrderId int = 0,
	@CustomerId int = 0,
	@OrderStatus int = 0,
	@Date datetime = 0,
	@LastUpdate datetime = 0,
	@PaymentStatus int = 0,
	@PaymentMethod varchar(50) = ''
AS
DECLARE @Id AS INT
SELECT @Id = Id FROM Orders WHERE OrderId = @OrderId
IF @Id IS NULL
	BEGIN
			INSERT INTO Orders (OrderId, CustomerId, OrderStatus, Date, LastUpdate, PaymentStatus, PaymentMethod)
			VALUES (@OrderId, @CustomerId, @OrderStatus, @Date, @LastUpdate, @PaymentStatus, @PaymentMethod);
	END
ELSE
	BEGIN
		UPDATE Orders
		SET OrderId = @OrderId, CustomerId = @CustomerId, OrderStatus = @OrderStatus, Date = @Date, LastUpdate = @LastUpdate,
		PaymentStatus = @PaymentStatus, PaymentMethod = @PaymentMethod
		WHERE OrderId = @OrderId
		IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_InsertAndUpdateProduct]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertAndUpdateProduct]
	@Name varchar(50) = '',
	@SKU varchar(50) = '',
	@Quantity int = 0,
	@Weight decimal(10,4) = 0,
	@Space varchar(10) = '',
	@Ean int = 0,
	@Image varchar(128) = ''
AS
DECLARE @Id AS INT
SELECT @Id = Id FROM Products WHERE SKU = @SKU
IF @Id IS NULL
	BEGIN
			INSERT INTO Products (Name, SKU, Quantity, Weight, StorageSpace, EAN, ImageLocation)
			VALUES (@Name, @SKU, @Quantity, @Weight, @Space, @Ean, @Image);
	END
ELSE
	BEGIN
		UPDATE Products
		SET Name = @Name, SKU = @SKU, Quantity = @Quantity, Weight = @Weight,
		StorageSpace = @Space, EAN = @Ean, ImageLocation = @Image
		WHERE SKU = @SKU
		IF @@ROWCOUNT = 0
			RAISERROR('Product with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_InsertException]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertException]
	@Exception_Type VarChar(30) = '',
	@Message Varchar(1024) = '',
	@Source VarChar(128) = '',
	@Stacktrace VarChar(2048) = ''
AS
	BEGIN
			INSERT INTO Exceptions(Exception_Type, Message, Source, Stacktrace)
			VALUES (@Exception_Type, @Message, @Source, @Stacktrace)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_InsertPassword]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertPassword]
	@Password varchar(50) = ''
AS
	BEGIN
			INSERT INTO Authentication(Password)
			VALUES (@Password)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_ProductInventory]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ProductInventory]
	@Id int = 0,
	@Quantity int = 0,
	@LastInventory DateTime
AS
	BEGIN
		UPDATE Products
		SET Quantity = @Quantity, LastInventory = @LastInventory
		WHERE Id = @Id
		IF @@ROWCOUNT = 0
			RAISERROR('Product with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateOrder]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateOrder]
	@Id int = 0,
	@OrderId int = 0,
	@CustomerId int = 0,
	@OrderStatus int = 0,
	@Date datetime = 0,
	@LastUpdate datetime = 0,
	@PaymentStatus int = 0,
	@PaymentMethod varchar(50) = ''
AS
	BEGIN
			UPDATE Orders 
			SET OrderId = @OrderId, CustomerId = @CustomerId, OrderStatus = @OrderStatus, Date = @Date, LastUpdate = @LastUpdate,
			PaymentStatus = @PaymentStatus, PaymentMethod = @PaymentMethod
			WHERE Id = @Id
			IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateOrderStatus]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateOrderStatus]
	@Id int = 0,
	@OrderStatus int = 0
AS
	BEGIN
			UPDATE Orders 
			SET OrderStatus = @OrderStatus
			WHERE Id = @Id
			IF @@ROWCOUNT = 0
			RAISERROR('Order with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateProduct]    Script Date: 2015-05-19 16:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateProduct]
	@Id int = 0,
	@Name varchar(50) = '',
	@SKU varchar(50) = '',
	@Quantity int = 0,
	@Weight decimal(10,4) = 0,
	@Space varchar(10) = '',
	@Ean varchar(30) = 0,
	@Image varchar(128) = ''
AS
	BEGIN
		UPDATE Products
		SET Name = @Name, SKU = @SKU, Quantity = @Quantity, Weight = @Weight,
		StorageSpace = @Space, EAN = @Ean, ImageLocation = @Image
		WHERE Id = @Id
		IF @@ROWCOUNT = 0
			RAISERROR('Product with this Id dosent exist.', 16,1)
			RETURN(1)
	END


GO
USE [master]
GO
ALTER DATABASE [Aware] SET  READ_WRITE 
GO
