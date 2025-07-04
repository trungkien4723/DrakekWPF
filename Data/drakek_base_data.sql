USE [master]
GO
/****** Object:  Database [Drakek]    Script Date: 20/04/2025 3:14:21 CH ******/
CREATE DATABASE [Drakek]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Drakek', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Drakek.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Drakek_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Drakek_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Drakek] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Drakek].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Drakek] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Drakek] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Drakek] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Drakek] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Drakek] SET ARITHABORT OFF 
GO
ALTER DATABASE [Drakek] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Drakek] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Drakek] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Drakek] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Drakek] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Drakek] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Drakek] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Drakek] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Drakek] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Drakek] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Drakek] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Drakek] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Drakek] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Drakek] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Drakek] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Drakek] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Drakek] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Drakek] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Drakek] SET  MULTI_USER 
GO
ALTER DATABASE [Drakek] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Drakek] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Drakek] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Drakek] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Drakek] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Drakek] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Drakek', N'ON'
GO
ALTER DATABASE [Drakek] SET QUERY_STORE = ON
GO
ALTER DATABASE [Drakek] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Drakek]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 20/04/2025 3:14:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[id] [nchar](10) NOT NULL,
	[name] [varchar](50) NULL,
	[value] [int] NULL,
	[valueType] [nchar](10) NULL,
	[createdDate] [datetime] NULL,
	[description] [nvarchar](max) NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [nchar](10) NOT NULL,
	[name] [varchar](50) NULL,
	[phone] [nchar](13) NOT NULL,
	[address] [nvarchar](max) NULL,
	[city] [nvarchar](50) NULL,
	[district] [nvarchar](50) NULL,
	[ward] [nvarchar](50) NULL,
	[createdDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [nchar](10) NOT NULL,
	[products] [nvarchar](max) NULL,
	[people] [nchar](10) NOT NULL,
	[customer] [nchar](10) NOT NULL,
	[coupon] [nchar](10) NULL,
	[totalPrice] [int] NULL,
	[orderType] [varchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
	[paid] [int] NULL,
	[createdDate] [datetime] NULL,
	[status] [nchar](10) NULL,
	[completedDate] [datetime] NULL,
	[discount] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[id] [nchar](10) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[role] [nchar](50) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[phone] [nchar](13) NULL,
	[Birthday] [date] NULL,
	[email] [varchar](50) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[id] [varchar](16) NOT NULL,
	[name] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [nchar](10) NOT NULL,
	[name] [varchar](50) NULL,
	[price] [int] NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [nchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[permission] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[id] [nchar](10) NOT NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[storage] [nchar](10) NOT NULL,
	[product] [nchar](10) NOT NULL,
	[quantity] [int] NULL,
	[expiredDate] [datetime] NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_Stock_Id] PRIMARY KEY CLUSTERED 
(
	[product] ASC,
	[storage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Storage]    Script Date: 20/04/2025 3:14:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Storage](
	[id] [nchar](10) NOT NULL,
	[name] [varchar](50) NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_Storage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Coupon] ([id], [name], [value], [valueType], [createdDate], [description], [startDate], [endDate]) VALUES (N'cpn574dcc ', N'test coupon 20%', 20, N'%         ', CAST(N'2025-03-14T17:37:54.440' AS DateTime), N'tttt', CAST(N'2025-03-14T00:00:00.000' AS DateTime), CAST(N'2025-03-15T00:00:00.000' AS DateTime))
INSERT [dbo].[Coupon] ([id], [name], [value], [valueType], [createdDate], [description], [startDate], [endDate]) VALUES (N'cpna4f8a1 ', N'test c50%', 50, N'%         ', CAST(N'2025-03-26T14:06:12.797' AS DateTime), N'', CAST(N'2025-03-26T00:00:00.000' AS DateTime), CAST(N'2025-03-30T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Customer] ([id], [name], [phone], [address], [city], [district], [ward], [createdDate]) VALUES (N'cstmec526 ', N'notbug', N'notbug       ', N'absolutly', N'bug', N'bug', N'bug', CAST(N'2025-03-26T17:08:22.293' AS DateTime))
INSERT [dbo].[Customer] ([id], [name], [phone], [address], [city], [district], [ward], [createdDate]) VALUES (N'cstmf6091 ', N'no', N'no           ', N'no', N'non', N'on', N'no', CAST(N'2025-03-24T18:41:20.060' AS DateTime))
INSERT [dbo].[Customer] ([id], [name], [phone], [address], [city], [district], [ward], [createdDate]) VALUES (N'cstmffa90 ', N'test customer', N'0123456789   ', N'', N'Bac Ninh', N'', N'', CAST(N'2025-03-13T17:53:53.683' AS DateTime))
GO
INSERT [dbo].[Order] ([id], [products], [people], [customer], [coupon], [totalPrice], [orderType], [description], [paid], [createdDate], [status], [completedDate], [discount]) VALUES (N'ordr202ee ', N'[{"product":"prdcta5813","storage":"strg7d050 ","quantity":100,"price":0,"expiredDate":null}]', N'pplcd5fc  ', N'cstmf6091 ', N'noCoupon  ', 6000000, N'buy', N'test buy product 2', 6000000, CAST(N'2025-03-24T18:44:10.483' AS DateTime), N'Completed ', CAST(N'2025-03-24T18:44:10.483' AS DateTime), 0)
INSERT [dbo].[Order] ([id], [products], [people], [customer], [coupon], [totalPrice], [orderType], [description], [paid], [createdDate], [status], [completedDate], [discount]) VALUES (N'ordr24153 ', N'[{"product":"prdcta5813","storage":"strg7d050 ","quantity":1,"price":0,"expiredDate":null}]', N'ppl40ab5  ', N'cstmf6091 ', N'noCoupon  ', 500000, N'sell', N'', 500000, CAST(N'2025-04-01T15:55:41.663' AS DateTime), N'Completed ', CAST(N'2025-04-01T15:55:41.663' AS DateTime), 0)
INSERT [dbo].[Order] ([id], [products], [people], [customer], [coupon], [totalPrice], [orderType], [description], [paid], [createdDate], [status], [completedDate], [discount]) VALUES (N'ordr34fcd ', N'[{"product":"prdctd9bf3","storage":"strg7d050 ","quantity":50,"price":0,"expiredDate":null}]', N'pplcd5fc  ', N'cstmf6091 ', N'noCoupon  ', 500000, N'buy', N'test buy product', 500000, CAST(N'2025-03-24T18:41:20.077' AS DateTime), N'Completed ', CAST(N'2025-03-24T18:41:20.077' AS DateTime), 0)
INSERT [dbo].[Order] ([id], [products], [people], [customer], [coupon], [totalPrice], [orderType], [description], [paid], [createdDate], [status], [completedDate], [discount]) VALUES (N'ordr42d3e ', N'[{"product":"prdcta5813","storage":"strg7d050","quantity":1,"price":0,"expiredDate":null}]', N'ppl40ab5  ', N'cstmf6091 ', N'noCoupon  ', 0, N'sell', N'', 500000, CAST(N'2025-04-01T16:12:38.890' AS DateTime), N'Completed ', CAST(N'2025-04-01T16:12:38.890' AS DateTime), 0)
INSERT [dbo].[Order] ([id], [products], [people], [customer], [coupon], [totalPrice], [orderType], [description], [paid], [createdDate], [status], [completedDate], [discount]) VALUES (N'ordrb6990 ', N'[{"product":"prdcta5813","storage":"strg811f4 ","quantity":20,"price":0,"expiredDate":null}]', N'pplcd5fc  ', N'cstmf6091 ', N'noCoupon  ', 0, N'buy', N'no', 0, CAST(N'2025-03-25T14:53:22.460' AS DateTime), N'Completed ', CAST(N'2025-03-25T14:53:22.460' AS DateTime), 0)
INSERT [dbo].[Order] ([id], [products], [people], [customer], [coupon], [totalPrice], [orderType], [description], [paid], [createdDate], [status], [completedDate], [discount]) VALUES (N'ordrd8762 ', N'[{"product":"prdcta5813","storage":"strg7d050","quantity":1,"price":0,"expiredDate":null}]', N'ppl40ab5  ', N'cstmf6091 ', N'noCoupon  ', 0, N'sell', N'', 500000, CAST(N'2025-03-31T21:11:04.703' AS DateTime), N'Completed ', CAST(N'2025-03-31T21:11:04.700' AS DateTime), 0)
GO
INSERT [dbo].[People] ([id], [name], [role], [createdDate], [phone], [Birthday], [email], [password], [image]) VALUES (N'ppl40ab5  ', N'test staff', N'roleb2804                                         ', CAST(N'2025-03-19T15:38:33.263' AS DateTime), N'0987654321   ', CAST(N'2013-01-01' AS Date), N'teststaff@gmail.com', N'b155530a0f49c7c66ba1ba02341158d9d8d799f44898fb818d091f8764f0ae41', N'')
INSERT [dbo].[People] ([id], [name], [role], [createdDate], [phone], [Birthday], [email], [password], [image]) VALUES (N'pplcd5fc  ', N'admin test', N'role02cc1                                         ', CAST(N'2025-03-22T14:48:36.433' AS DateTime), N'0987654321   ', CAST(N'2025-07-18' AS Date), N'testadmin@gmail.com', N'597f5441e7d174b607873874ed54b974674986ad543e7458e28a038671c9f64c', NULL)
INSERT [dbo].[People] ([id], [name], [role], [createdDate], [phone], [Birthday], [email], [password], [image]) VALUES (N'pplsuperad', N'Super admin', N'superadmin                                        ', CAST(N'2025-03-13T00:00:00.000' AS DateTime), N'0384234723   ', CAST(N'2000-11-27' AS Date), N'ntrungkien4723@gmail.com', N'186cf774c97b60a1c106ef718d10970a6a06e06bef89553d9ae65d938a886eae', NULL)
GO
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_all', N'Access all pages')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_all', N'Update all entities')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_role', N'Access role page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_role', N'Update roles')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_people', N'Access people page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_people', N'Update people')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_order', N'Access order page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_order', N'Update sell orders')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_customer', N'Access customer page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_customer', N'Update customers')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_coupon', N'Access coupon page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_coupon', N'Update coupons')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_stock', N'Access stock page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_stock', N'Update buy orders')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_product', N'Access product page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_product', N'Update products')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_storage', N'Access storage page')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_storage', N'Update storages')
GO
INSERT [dbo].[Product] ([id], [name], [price], [createdDate]) VALUES (N'prdcta5813', N'test product 2', 100000, CAST(N'2025-03-19T15:31:45.913' AS DateTime))
INSERT [dbo].[Product] ([id], [name], [price], [createdDate]) VALUES (N'prdctd9bf3', N'test product 1', 500000, CAST(N'2025-03-19T15:31:13.753' AS DateTime))
GO
INSERT [dbo].[Role] ([id], [name], [permission], [createdDate]) VALUES (N'role02cc1                                         ', N'admin', N'access_people,update_people,access_order,update_order,access_customer,update_customer,access_coupon,update_coupon,access_stock,update_stock,access_product,update_product,access_storage,update_storage', CAST(N'2025-03-22T14:45:03.460' AS DateTime))
INSERT [dbo].[Role] ([id], [name], [permission], [createdDate]) VALUES (N'roleb2804                                         ', N'Staff', N'access_order,update_order,update_customer', CAST(N'2025-03-19T15:37:00.840' AS DateTime))
INSERT [dbo].[Role] ([id], [name], [permission], [createdDate]) VALUES (N'superadmin                                        ', N'Super admin', N'access_all,access_all,update_all', CAST(N'2025-03-13T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Stock] ([storage], [product], [quantity], [expiredDate], [createdDate]) VALUES (N'strg7d050 ', N'prdcta5813', 95, NULL, CAST(N'2025-03-24T18:44:10.467' AS DateTime))
INSERT [dbo].[Stock] ([storage], [product], [quantity], [expiredDate], [createdDate]) VALUES (N'strg811f4 ', N'prdcta5813', 20, NULL, CAST(N'2025-03-25T14:53:22.360' AS DateTime))
INSERT [dbo].[Stock] ([storage], [product], [quantity], [expiredDate], [createdDate]) VALUES (N'strg7d050 ', N'prdctd9bf3', 50, NULL, CAST(N'2025-03-24T18:41:20.003' AS DateTime))
GO
INSERT [dbo].[Storage] ([id], [name], [createdDate]) VALUES (N'strg7d050 ', N'test storage 1', CAST(N'2025-03-19T15:16:23.313' AS DateTime))
INSERT [dbo].[Storage] ([id], [name], [createdDate]) VALUES (N'strg811f4 ', N'test storage 2', CAST(N'2025-03-19T15:31:59.307' AS DateTime))
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([customer])
REFERENCES [dbo].[Customer] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_People] FOREIGN KEY([people])
REFERENCES [dbo].[People] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_People]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Role] FOREIGN KEY([role])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Role]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Product] FOREIGN KEY([product])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Product]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Storage] FOREIGN KEY([storage])
REFERENCES [dbo].[Storage] ([id])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Storage]
GO
USE [master]
GO
ALTER DATABASE [Drakek] SET  READ_WRITE 
GO
