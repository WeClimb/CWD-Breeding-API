USE [master]
GO
/****** Object:  Database [PreKno_DEV]    Script Date: 6/6/2022 8:41:13 PM ******/
CREATE DATABASE [PreKno_DEV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PreKno_DEV', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PreKno_DEV.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PreKno_DEV_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PreKno_DEV_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PreKno_DEV] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PreKno_DEV].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PreKno_DEV] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PreKno_DEV] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PreKno_DEV] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PreKno_DEV] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PreKno_DEV] SET ARITHABORT OFF 
GO
ALTER DATABASE [PreKno_DEV] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PreKno_DEV] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PreKno_DEV] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PreKno_DEV] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PreKno_DEV] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PreKno_DEV] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PreKno_DEV] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PreKno_DEV] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PreKno_DEV] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PreKno_DEV] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PreKno_DEV] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PreKno_DEV] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PreKno_DEV] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PreKno_DEV] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PreKno_DEV] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PreKno_DEV] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PreKno_DEV] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PreKno_DEV] SET RECOVERY FULL 
GO
ALTER DATABASE [PreKno_DEV] SET  MULTI_USER 
GO
ALTER DATABASE [PreKno_DEV] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PreKno_DEV] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PreKno_DEV] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PreKno_DEV] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PreKno_DEV] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PreKno_DEV] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PreKno_DEV', N'ON'
GO
ALTER DATABASE [PreKno_DEV] SET QUERY_STORE = OFF
GO
USE [PreKno_DEV]
GO
/****** Object:  Table [dbo].[ChangePasswords]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangePasswords](
	[Id] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NULL,
	[ServiceProviderId] [uniqueidentifier] NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_ChangePasswords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangeRequests]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangeRequests](
	[Id] [uniqueidentifier] NOT NULL,
	[Reason] [varchar](120) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Status] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[LoginDataId] [uniqueidentifier] NULL,
	[SubDataId] [uniqueidentifier] NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Clients__3214EC074C5A0FEF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientsReviews]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientsReviews](
	[Id] [uniqueidentifier] NOT NULL,
	[ReviewId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginData]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginData](
	[Id] [uniqueidentifier] NOT NULL,
	[Password] [varchar](100) NULL,
	[Salt] [varchar](50) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[LastLoginDate] [datetime2](7) NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK__LoginDat__3214EC07C0BEAEAB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] [uniqueidentifier] NOT NULL,
	[Service] [varchar](50) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Rating] [decimal](18, 0) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Reviews__3214EC07C7033646] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewsChangeRequests]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewsChangeRequests](
	[Id] [uniqueidentifier] NOT NULL,
	[ReviewId] [uniqueidentifier] NOT NULL,
	[ChangeRequestId] [uniqueidentifier] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProviders]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProviders](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[LoginDataId] [uniqueidentifier] NOT NULL,
	[SubDataId] [uniqueidentifier] NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK__ServiceP__3214EC07340C2C3B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__ServiceP__A9D105345E0FA24E] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProvidersReviews]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProvidersReviews](
	[Id] [uniqueidentifier] NOT NULL,
	[ReviewId] [uniqueidentifier] NOT NULL,
	[ServiceProviderId] [uniqueidentifier] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubData]    Script Date: 6/6/2022 8:41:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubData](
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__SubData__3214EC077530FA12] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChangePasswords]  WITH CHECK ADD  CONSTRAINT [FK_ChangePasswords_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[ChangePasswords] CHECK CONSTRAINT [FK_ChangePasswords_Clients]
GO
ALTER TABLE [dbo].[ChangePasswords]  WITH CHECK ADD  CONSTRAINT [FK_ChangePasswords_ServiceProviders] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProviders] ([Id])
GO
ALTER TABLE [dbo].[ChangePasswords] CHECK CONSTRAINT [FK_ChangePasswords_ServiceProviders]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_LoginData] FOREIGN KEY([LoginDataId])
REFERENCES [dbo].[LoginData] ([Id])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_LoginData]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_SubData] FOREIGN KEY([SubDataId])
REFERENCES [dbo].[SubData] ([Id])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_SubData]
GO
ALTER TABLE [dbo].[ClientsReviews]  WITH CHECK ADD  CONSTRAINT [FK__ClientsRe__Clien__3B75D760] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[ClientsReviews] CHECK CONSTRAINT [FK__ClientsRe__Clien__3B75D760]
GO
ALTER TABLE [dbo].[ClientsReviews]  WITH CHECK ADD  CONSTRAINT [FK__ClientsRe__Revie__3A81B327] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Reviews] ([Id])
GO
ALTER TABLE [dbo].[ClientsReviews] CHECK CONSTRAINT [FK__ClientsRe__Revie__3A81B327]
GO
ALTER TABLE [dbo].[ReviewsChangeRequests]  WITH CHECK ADD FOREIGN KEY([ChangeRequestId])
REFERENCES [dbo].[ChangeRequests] ([Id])
GO
ALTER TABLE [dbo].[ReviewsChangeRequests]  WITH CHECK ADD  CONSTRAINT [FK__ReviewsCh__Revie__36B12243] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Reviews] ([Id])
GO
ALTER TABLE [dbo].[ReviewsChangeRequests] CHECK CONSTRAINT [FK__ReviewsCh__Revie__36B12243]
GO
ALTER TABLE [dbo].[ServiceProviders]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProviders_LoginData] FOREIGN KEY([LoginDataId])
REFERENCES [dbo].[LoginData] ([Id])
GO
ALTER TABLE [dbo].[ServiceProviders] CHECK CONSTRAINT [FK_ServiceProviders_LoginData]
GO
ALTER TABLE [dbo].[ServiceProviders]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProviders_SubData] FOREIGN KEY([SubDataId])
REFERENCES [dbo].[SubData] ([Id])
GO
ALTER TABLE [dbo].[ServiceProviders] CHECK CONSTRAINT [FK_ServiceProviders_SubData]
GO
ALTER TABLE [dbo].[ServiceProvidersReviews]  WITH CHECK ADD  CONSTRAINT [FK__ServicePr__Revie__3E52440B] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Reviews] ([Id])
GO
ALTER TABLE [dbo].[ServiceProvidersReviews] CHECK CONSTRAINT [FK__ServicePr__Revie__3E52440B]
GO
ALTER TABLE [dbo].[ServiceProvidersReviews]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProvidersReviews_ServiceProviders] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProviders] ([Id])
GO
ALTER TABLE [dbo].[ServiceProvidersReviews] CHECK CONSTRAINT [FK_ServiceProvidersReviews_ServiceProviders]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [CK__Reviews__Rating__31EC6D26] CHECK  (([Rating]>=(0) AND [Rating]<=(5)))
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [CK__Reviews__Rating__31EC6D26]
GO
USE [master]
GO
ALTER DATABASE [PreKno_DEV] SET  READ_WRITE 
GO
