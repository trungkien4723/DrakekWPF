USE [master]
GO
/****** Object:  Database [drakek]    Script Date: 14/03/2025 10:48:02 CH ******/
CREATE DATABASE [drakek]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'drakek', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\drakek.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'drakek_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\drakek_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [drakek] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [drakek].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [drakek] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [drakek] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [drakek] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [drakek] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [drakek] SET ARITHABORT OFF 
GO
ALTER DATABASE [drakek] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [drakek] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [drakek] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [drakek] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [drakek] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [drakek] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [drakek] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [drakek] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [drakek] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [drakek] SET  DISABLE_BROKER 
GO
ALTER DATABASE [drakek] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [drakek] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [drakek] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [drakek] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [drakek] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [drakek] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [drakek] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [drakek] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [drakek] SET  MULTI_USER 
GO
ALTER DATABASE [drakek] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [drakek] SET DB_CHAINING OFF 
GO
ALTER DATABASE [drakek] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [drakek] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [drakek] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [drakek] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [drakek] SET QUERY_STORE = ON
GO
ALTER DATABASE [drakek] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [drakek]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 14/03/2025 10:48:03 CH ******/
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
	[description] [text] NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 14/03/2025 10:48:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [nchar](10) NOT NULL,
	[name] [varchar](50) NULL,
	[phone] [nchar](13) NOT NULL,
	[address] [text] NULL,
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
/****** Object:  Table [dbo].[Order]    Script Date: 14/03/2025 10:48:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [nchar](10) NOT NULL,
	[products] [text] NULL,
	[people] [nchar](10) NOT NULL,
	[customer] [nchar](10) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[coupon] [nchar](10) NULL,
	[paymentType] [varchar](50) NOT NULL,
	[totalPrice] [int] NULL,
	[orderType] [varchar](50) NOT NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 14/03/2025 10:48:03 CH ******/
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
	[password] [text] NOT NULL,
	[image] [text] NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 14/03/2025 10:48:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[id] [varchar](16) NOT NULL,
	[name] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 14/03/2025 10:48:03 CH ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 14/03/2025 10:48:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [nchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[permission] [text] NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 14/03/2025 10:48:03 CH ******/
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
/****** Object:  Table [dbo].[Stock]    Script Date: 14/03/2025 10:48:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[storage] [nchar](10) NOT NULL,
	[product] [nchar](10) NOT NULL,
	[quantity] [int] NULL,
	[expiredDate] [datetime] NULL,
	[createdDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Storage]    Script Date: 14/03/2025 10:48:03 CH ******/
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
INSERT [dbo].[Coupon] ([id], [name], [value], [valueType], [createdDate], [description], [startDate], [endDate]) VALUES (N'cpn574dcc ', N'test', 0, N'%         ', CAST(N'2025-03-14T17:37:54.440' AS DateTime), N'tttt', CAST(N'2025-03-14T00:00:00.000' AS DateTime), CAST(N'2025-03-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Customer] ([id], [name], [phone], [address], [city], [district], [ward], [createdDate]) VALUES (N'cstmffa90 ', N'test customer', N'0123456789   ', N'', N'Bac Ninh', N'', N'', CAST(N'2025-03-13T17:53:53.683' AS DateTime))
GO
INSERT [dbo].[People] ([id], [name], [role], [createdDate], [phone], [Birthday], [email], [password], [image]) VALUES (N'pplsuperad', N'Super admin', N'superadmin                                        ', CAST(N'2025-03-13T00:00:00.000' AS DateTime), N'0384234723   ', CAST(N'2000-11-27' AS Date), N'ntrungkien4723@gmail.com', N'186cf774c97b60a1c106ef718d10970a6a06e06bef89553d9ae65d938a886eae', NULL)
GO
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'access_all', N'Access all pages')
INSERT [dbo].[Permission] ([id], [name]) VALUES (N'update_all', N'Update all entities')
GO
INSERT [dbo].[Role] ([id], [name], [permission], [createdDate]) VALUES (N'superadmin                                        ', N'Super admin', N'access_all,update_all', CAST(N'2025-03-13T00:00:00.000' AS DateTime))
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Coupon] FOREIGN KEY([coupon])
REFERENCES [dbo].[Coupon] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Coupon]
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
ALTER DATABASE [drakek] SET  READ_WRITE 
GO
