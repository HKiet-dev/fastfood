/****** Object:  Database [FatFoodDB]    Script Date: 8/5/2024 8:24:32 AM ******/
CREATE DATABASE [FatFoodDB]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [FatFoodDB] SET COMPATIBILITY_LEVEL = 160
GO
ALTER DATABASE [FatFoodDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FatFoodDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FatFoodDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FatFoodDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FatFoodDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FatFoodDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FatFoodDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FatFoodDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FatFoodDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FatFoodDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FatFoodDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FatFoodDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FatFoodDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FatFoodDB] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [FatFoodDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FatFoodDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FatFoodDB] SET  MULTI_USER 
GO
ALTER DATABASE [FatFoodDB] SET ENCRYPTION ON
GO
ALTER DATABASE [FatFoodDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [FatFoodDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Avatar] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[UserId] [nvarchar](450) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_CartDetail] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ReceviedDate] [datetime2](7) NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[PaymentType] [nvarchar](max) NOT NULL,
	[PaymentStatus] [nvarchar](max) NOT NULL,
	[OrderStatus] [nvarchar](max) NOT NULL,
	[note] [nvarchar](max) NULL,
	[OrderDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 8/5/2024 8:24:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[View] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsCombo] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CreateAt] [datetime2](7) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240705050116_init', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240709080811_modify-data-type-all-table', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240709095508_Add-Cart-Table', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240714125131_updateDB', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240716064506_update-imageurl-product', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240718151051_add-status-user', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240722120533_update-cart', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240723020214_update-orderDetail', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240723022822_update-order', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240724035918_UpdateDb-CartDetail', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240725023303_UpdateOrder&OrderDetail', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240728010906_d', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240728013609_address', N'8.0.6')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a82a002d-c654-495e-b71d-d4a7f6dc9fed', N'CUSTOMER', N'CUSTOMER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'bad29c9f-bc68-46f4-a845-e6a6a32017bd', N'ADMIN', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0fee28f6-b8b1-4092-9979-f3a10edebf7f', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0fee28f6-b8b1-4092-9979-f3a1aedexf7f', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3e43f4e6-9f2e-4d21-acc2-265e141d7520', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'46920465-8de8-4828-b608-8dae118f2c82', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4d7d3e9a-47d1-4d4d-890e-1d322127f61a', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'66fdbbbb-6712-4fab-a074-d021dd76d23b', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6735c0e8-3201-47fe-b5a7-515bc0504fd7', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6e3280d4-c90e-4816-b725-a93a47b704a3', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7d0d4c8c-826a-4f5a-8f70-5865e6499670', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'86a7e944-b06f-43d0-9fe8-dd8fe9f16b04', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8b81fa05-522e-44b3-ac60-f3237617385d', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'935707cb-4fa5-4315-b5fb-0c740ca03690', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'95a2e0c6-32a0-4add-877d-60aa3f70b649', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'976b58f3-5afa-42d3-955a-8eb32e40c919', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9bd8aa03-98f3-4f94-a087-753824f870eb', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a7d56ce3-ff8c-410d-9609-c3aec739bb26', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c6a608e7-292d-484b-9ddb-4f0f122fe9a8', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd7db2ebc-bfba-4c91-b04b-9bdc0a5d8e33', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'df616ea5-06d0-49bc-805d-4fb8830e60bc', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e9942b5d-af84-4696-b314-6c72449daa84', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fdc87154-20a5-4b72-8f4f-2b418640d7a6', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fe8588cb-e305-4837-8ac6-2b2806ad99ac', N'a82a002d-c654-495e-b71d-d4a7f6dc9fed')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'26feb2d3-4915-487e-a896-ca9da7bb798d', N'bad29c9f-bc68-46f4-a845-e6a6a32017bd')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dee68335-e3d0-44f8-96b1-e8b5c703cae2', N'bad29c9f-bc68-46f4-a845-e6a6a32017bd')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'0fee28f6-b8b1-4092-9979-f3a10edebf7f', 1, N'ádfghjk', N'/Img/default_avatar.png', N'mily4001', N'MILY4001', N'chienprivate@gmail.com', N'CHIENPRIVATE@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEGhoKyO1PE6kU7UAIsSbJltlcUYkzOI8hVkkwG+N1fPdNe950uOzXBh4RhPFwA5ftQ==', N'J7WNKNBJVXQ4VTSJ4MI7PI2COPTCNBAZ', N'd10da18a-3642-45ea-a7c5-35b98374d457', N'0937806350', 0, 0, NULL, 0, 0, N'mily4001', CAST(N'2024-07-28T10:04:21.9042801' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'0fee28f6-b8b1-4092-9979-f3a1aedexf7f', 0, N'string', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722773746/r6jnu3fuawgo4qbqktnn.png', N'vinacafe', N'VINACAFE', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEMy4NDoUpwUjcuc9g7RM9m3PEHmHW2HFbwEN+XBGvyyoVsoikLwRHzDIg1lsxheHhA==', N'ABFK54F3VPEWJVLG7BMOHEWW7JFU7RRA', N'34b65ca8-fdbe-420a-a80b-ab5056c49696', N'123321123', 0, 0, NULL, 0, 0, N'vinacafe', CAST(N'2024-08-04T12:15:47.2096764' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'1a2b3c4d-5678-90ab-cdef-1234567890ab', 1, N'123 Main St, Springfield, IL', N'https://cdn-icons-png.flaticon.com/512/4333/4333609.png ', N'john.doe', N'JOHN.DOE', N'john.doe@example.com', N'JOHN.DOE@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEBvGEKQZJQdWueLvEV8LS9Zl3GV0JExzz1Zkl21KUlVX5Wf1q5fV0fN+ErEkm5S9Cg==', N'RANDOMSECURITYSTAMP1', N'RANDOMCONCURRENCYSTAMP1', N'0987654321', 1, 0, NULL, 1, 0, N'John Doe', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'1df697fb-0acf-4de5-afcc-9957622348c8', 1, N'Long Xuyên', NULL, N'TuanDepTrai', N'TUANDEPTRAI', N'tuandeptrainhat@gmail.com', N'TUANDEPTRAINHAT@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEEoiquApG6eGnV9haoE49GU6AMQTu8CAbZTsNHcovpIlp37w42F4pBeHFiG1Gu2g7g==', N'7JPSLSJSXEVXBJMQ42Y6UUQROA26WDNI', N'b2945941-e9b4-4e0c-b4a0-f03ab4f564d6', N'0379959385', 0, 0, NULL, 1, 0, N'TuanDepTrai', CAST(N'2024-08-01T18:14:07.5538391' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'1k253647-5678-90kl-mn12-34567h890123', 1, N'808 Spruce St, Hub City, OH', N'https://cdn-icons-png.flaticon.com/128/1326/1326377.png', N'henry.ivan', N'HENRY.IVAN', N'henry.ivan@example.com', N'HENRY.IVAN@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEQORMLYQO7VXQRqNLUZPW1VnBdO7MS5RQ8Z6Z1RA1ZH2SMWQO4O1U3QTTQON==', N'RANDOMSECURITYSTAMP11', N'RANDOMCONCURRENCYSTAMP11', N'888-999-0000', 1, 0, NULL, 1, 0, N'Henry Ivan', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'26feb2d3-4915-487e-a896-ca9da7bb798d', 1, N'3271836817212312', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722772223/wrr0btu0qdfnro9sqtcz.webp', N'admin4000', N'ADMIN4000', N'chienprivate321321321321@gmail.com', N'CHIENPRIVATE321321321321@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEIOB8IWhkyIKtkbVjnAYvpK16qj6MDLUGO3scee9YhHzpmUFuBStNWqx+4HW2AHylg==', N'G26VMQCRHVUJAF27XUKP5BULN7LUPHN4', N'9f95acb5-ece9-445a-805a-9a459928c87f', N'0937806350', 0, 0, NULL, 1, 0, N'admin4000', CAST(N'2024-08-04T11:50:36.0108200' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'2b3c4d5e-6789-01bc-def2-34567890abcd', 0, N'456 Oak St, Metropolis, NY', N'https://cdn-icons-png.flaticon.com/128/921/921124.png', N'jane.smith', N'JANE.SMITH', N'jane.smith@example.com', N'JANE.SMITH@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEC3HkNXKQk0PpC1gqPbW6y+FbCrh8l8yTSGiCwQIzRU71ZLZhRKHgW7rUuAFOA==', N'RANDOMSECURITYSTAMP2', N'RANDOMCONCURRENCYSTAMP2', N'0987654323', 1, 0, NULL, 1, 0, N'Jane Smith', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'2l364758-6789-01lm-no23-45678i901234', 0, N'909 Ash St, Opal City, PA', N'https://cdn-icons-png.flaticon.com/128/16683/16683419.png', N'isabel.james', N'ISABEL.JAMES', N'isabel.james@example.com', N'ISABEL.JAMES@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEROQLMZRO8WYRrPMMZPX2WOnCdO8MT6SR9Y7Y2RB2YH3TNXUP5P2V4QTURPO==', N'RANDOMSECURITYSTAMP12', N'RANDOMCONCURRENCYSTAMP12', N'999-000-1111', 1, 0, NULL, 1, 0, N'Isabel James', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'3c4d5e6f-7890-12cd-ef34-567890abcde1', 1, N'789 Pine St, Gotham, CA', N'https://cdn-icons-png.flaticon.com/128/4681/4681809.png', N'mike.jones', N'MIKE.JONES', N'mike.jones@example.com', N'MIKE.JONES@EXAMPLE.COM', 0, N'AQAAAAEAACcQAAAAED4HaKM6KWNd0Kp1vK3tXvAq8c4n4GJ5HHKoRA7PTvHSY5GnB1iRe2RmNMIzMw==', N'RANDOMSECURITYSTAMP3', N'RANDOMCONCURRENCYSTAMP3', N'0987654322', 0, 0, NULL, 1, 0, N'Mike Jones', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'3e43f4e6-9f2e-4d21-acc2-265e141d7520', 1, N'mbvc,xmbn,cmxvnb,cx', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1721895343/wdfjnxyilbutlc5kdmsb.webp', N'mily40009', N'MILY40009', N'chienprivate123456@gmail.com', N'CHIENPRIVATE123456@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEEGj1a8/0GhdY/Swi13AMnsezrgJQctuxyU/0erAxDmuHT+YedzuASxLz6BzurQUiQ==', N'RH46BUGYWNUM6Z6JOVNU2Y7TS6J6TMSD', N'a18e9364-d958-43d9-898a-e205df1970eb', N'0937806350', 0, 0, NULL, 1, 0, N'mily40009', CAST(N'2024-07-25T08:15:58.0393647' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'3m475869-7890-12mn-op34-56789j012345', 1, N'1010 Sycamore St, Dakota City, SD', N'https://cdn-icons-png.flaticon.com/128/921/921071.png', N'jack.king', N'JACK.KING', N'jack.king@example.com', N'JACK.KING@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEROPLMZSP9XZRTNNMYPY3XOnDdP9NU7TRAY8Z3RB3YI4UNYVQ5Q3W5QURQPQ==', N'RANDOMSECURITYSTAMP13', N'RANDOMCONCURRENCYSTAMP13', N'000-111-2222', 1, 0, NULL, 1, 0, N'Jack King', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'46920465-8de8-4828-b608-8dae118f2c82', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'hoangtmps35865@fpt.edu.vn', N'HOANGTMPS35865@FPT.EDU.VN', N'hoangtmps35865@fpt.edu.vn', N'HOANGTMPS35865@FPT.EDU.VN', 0, NULL, N'JTDVO6NKPE6Q3JAT7G3ESSGSHZTHYXDX', N'cece0347-9447-4ac4-8bd6-eb4aeaf3f074', NULL, 0, 0, NULL, 1, 0, N'Truong Minh Hoang (FPL HCM)', CAST(N'2024-07-27T09:00:15.9855635' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'4d5e6f70-8901-23de-ef45-67890abcdef2', 0, N'101 Maple St, Smallville, KS', N'https://cdn-icons-png.flaticon.com/128/547/547413.png', N'anne.johnson', N'ANNE.JOHNSON', N'anne.johnson@example.com', N'ANNE.JOHNSON@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEJfDfGJcHLUKmLGL3I5kZ9D2QTh6WZPZp5Y4J2tFUR5rM6VQ8M0WZ3yBzX9PQ==', N'RANDOMSECURITYSTAMP4', N'RANDOMCONCURRENCYSTAMP4', N'111-222-3333', 1, 0, NULL, 1, 0, N'Anne Johnson', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'4d7d3e9a-47d1-4d4d-890e-1d322127f61a', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'kiethttps32354@fpt.edu.vn', N'KIETHTTPS32354@FPT.EDU.VN', N'kiethttps32354@fpt.edu.vn', N'KIETHTTPS32354@FPT.EDU.VN', 0, NULL, N'IU6JEPT3XXW7FVYY6COA3C4LGCMIJRLP', N'fd1c7839-54c5-4cfc-baa0-08c39907956b', NULL, 0, 0, NULL, 1, 0, N'Huynh Tran Tuan Kiet (FPL HCM)', CAST(N'2024-07-29T01:47:40.0992630' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', 1, N'vncx,zvn,cxmvbm,cxz', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722168993/ywim8qc8gatjmqws3yd5.jpg', N'mily4022', N'MILY4022', N'chienprivate123456123@gmail.com', N'CHIENPRIVATE123456123@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEE74STSH0Ghp2gLbxCYBD0GlmknQy3reKLLwfpC6RFPJ2GtiR1c2llIC9O+uIdk5NA==', N'IOZLR5C5ZKC5ELP6TRLJESLQB35YXDAJ', N'3d0797af-a435-4218-a5d7-5dedeccc9e5b', N'0937806350', 0, 0, NULL, 1, 0, N'mily4022', CAST(N'2024-07-28T12:16:33.4231998' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'550e1fed-b4f3-4620-812b-a6d3e50cab64', 0, N'Phước Đông', N'string', N'kietaa', N'KIETAA', N'huynahtrantuankiet2901@gmail.com', N'HUYNAHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEAQ9H6dGHHnz9fgIN0ZgHfKgioxmpkqNwz7gqKpN3Y/oLfFzRMNhUXKONiIS1wCsqA==', N'5535SVS7PCU7AD4TV6PFNBEI2TXIQZ3C', N'a4343f99-57df-436e-897a-a16c9019d5f0', N'0965335702', 0, 0, NULL, 1, 0, N'kietaa', CAST(N'2024-07-27T16:38:54.9432631' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'57db76a6-92c7-400f-8d16-30a11486ebd3', 0, N'Go Vap HCM', N'string', N'NguyenAnhDung', N'NGUYENANHDUNG', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAED1DZOjMyAdY3BQy8QjkNSf2yM/JqKFaNon/wszLak0Vg0LNyqEYD/VBDs4fYvkbrA==', N'F5T2CB43IS3M26VRBOYX4HWYGGCFPP57', N'9fbe3727-e3d3-4aa3-b6dd-5d96c38a8a4e', N'1233211233', 0, 0, NULL, 1, 0, N'NguyenAnhDung', CAST(N'2024-08-04T11:04:07.6649780' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'5e6f7081-9012-34ef-gh56-78901bcdef34', 1, N'202 Cedar St, Star City, TX', N'https://cdn-icons-png.flaticon.com/128/11107/11107521.png', N'bob.brown', N'BOB.BROWN', N'bob.brown@example.com', N'BOB.BROWN@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEHOPL2XKLZMQQMLjHNYKT4Mk6cKRg4Q1JK0W2NPW5WF6PFTL9K2QZ8LJRBHJQ==', N'RANDOMSECURITYSTAMP5', N'RANDOMCONCURRENCYSTAMP5', N'222-333-4444', 1, 0, NULL, 1, 0, N'Bob Brown', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'5e6f7081-9012-34ef-gh56-78901bcdef36', 0, N'Tp Ho Chi Minh', N'https://cdn.akamai.steamstatic.com/steamcommunity/public/images/items/3099120/f0d9a3c4f80bc9eff2818098510dca0b7ec2a4bb.gif', N'kietadmin', N'KIETADMIN', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEC9wkiatYZMH4v43uw1agA0uzKv0PJBVwU9dW4SB6tyJpn0hVw2Nbs5pc84OF94ULg==', N'ZH2RGBSFYZESF5LJXBFAJLYC25DN6LDT', N'052e1d90-b692-4065-a50c-364e87f6c00d', NULL, 0, 0, NULL, 1, 0, N'kietadmin', CAST(N'2024-07-18T16:35:37.5678898' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'66fdbbbb-6712-4fab-a074-d021dd76d23b', 0, N'string', N'string', N'CMsNow5D7kh6BA9XAgApUhACnEnvqMWt4UQJWQJR71oWPaSvWu', N'CMSNOW5D7KH6BA9XAGAPUHACNENVQMWT4UQJWQJR71OWPASVWU', N'user@example.com', N'USER@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEGjIJhB546ac3bG8sFVWw38Fhr/xMrtIT1hrorVm650EQAheKHDEOl3W5cpeTMxIvQ==', N'LDAAMWCTOGE5357CF2R4GWS4NO2PUSEF', N'7c8a29c5-7c15-4684-a4fb-9a30b561ff2a', N'093232323232', 0, 0, NULL, 1, 0, N'CMsNow5D7kh6BA9XAgApUhACnEnvqMWt4UQJWQJR71oWPaSvWu', CAST(N'2024-07-24T07:47:42.2837546' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'67234d4a-bb39-4886-aae9-4c66ae13599c', 0, N'string', N'string', N'Kietne', N'KIETNE', N'user@example.com', N'USER@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEKB3GH1BMHHQzXynDrfN98d8OXMECzgENJfduZu0vUdwbkQCYPf8yVMtT/WxePO+9w==', N'PSNCEMK5XN2DQUAGEHQQ7OJ3626Q5Y6U', N'a3597993-8b6d-4b75-b5c0-0e13d1d01270', N'123321123', 0, 0, NULL, 1, 0, N'Kietne', CAST(N'2024-07-22T01:59:57.7501799' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'6735c0e8-3201-47fe-b5a7-515bc0504fd7', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'huynhkiet.dev@gmail.com', N'HUYNHKIET.DEV@GMAIL.COM', N'huynhkiet.dev@gmail.com', N'HUYNHKIET.DEV@GMAIL.COM', 0, NULL, N'H63TG3JGJ3JGQBJBOD6BTNIA4ZFFAJKZ', N'e0e4c9dc-dc03-45a8-8998-525ab5819795', NULL, 0, 0, NULL, 1, 0, N'KIET HUYNH', CAST(N'2024-07-29T01:39:34.1009234' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'6817d97b-2bbb-4b6c-b0b2-b2437d18be14', 0, N'Quang Trung Gò Vấp', NULL, N'kiet', N'KIET', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEPowHIderhVdJ4ZCNCh6MRwUYnY7qhD3aj8cWq53hwxAE6jIQNtQ4Rvlt2PImqOQlg==', N'YXUQARAE3FHLEXM3EMTPZYJVE5RNWQHZ', N'0815d79e-8a67-4434-894d-5d9ea84ad21c', N'09876543321', 0, 0, NULL, 1, 0, N'kiet', CAST(N'2024-07-29T00:54:46.4933584' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'6d709235-60d9-4b5a-a6e7-e0f8da36607d', 1, N'Phước Đông', NULL, N'tuan', N'TUAN', N'tuan@gmail.com', N'TUAN@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEEoGOIOKE7Y7xwTRsbkGR0rxUtmc0wdcM/7VbMhKVG+veO6fgHLjbxF8K4HVAJy2pw==', N'DV6RQEW4SHIAO3CSKMXI3UOACNFFE4WC', N'f477e9c7-03f5-4caf-85fa-f1389e800cae', N'0965335702', 0, 0, NULL, 1, 0, N'tuan', CAST(N'2024-07-29T00:53:17.8518552' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'6e3280d4-c90e-4816-b725-a93a47b704a3', 1, N'hjfksdfhkjdsfhksjd', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1721965239/j4vndugojx1flhttjnv5.jpg', N'mily4000321', N'MILY4000321', N'chienprivate78798798@gmail.com', N'CHIENPRIVATE78798798@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEOA00DUjnEawa7cga5FJINjHBSmCMbRZhX09ifJe3WE5neXfHcjsgymHhiXdFNasfw==', N'ZZYZDG2ZMMFDFB3BAUBKJY42HMDP44LE', N'629f71c3-b973-41b0-9ab6-8cc82586fd56', N'0937806350', 0, 0, NULL, 1, 0, N'mily4000321', CAST(N'2024-07-26T03:40:49.0435303' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'6eacfb0f-c7ad-438e-84b6-60beaa764338', 0, NULL, NULL, N'kietaabd', N'KIETAABD', N'huynahtrantuankiet2901@gmail.com', N'HUYNAHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEKsOp1NJSCnRCFDfSxjbi2ALSkOQwXnF8Q4UH+Lc1lfEjpQPS/Xodpx6ATR8IszL3g==', N'ASYDTUCKLJEHYSFOHYNA6GPDQSGREWJD', N'b088c2b6-d2fa-445d-9289-15f80835cd93', N'0965335702', 0, 0, NULL, 1, 0, N'kietaabd', CAST(N'2024-07-28T01:39:19.9676210' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'7137f158-abb7-4742-869e-745b26c2809c', 0, N'string', N'string', N'Kietttaaa', N'KIETTTAAA', N'Kietttaaa@example.com', N'KIETTTAAA@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEHCIdqS/X2nd5H8rIAgQGHSVvNzRJdrOclSjLMgfNkuzxOyFdRyel51araW9UffxXg==', N'EOOYDWI76UEXVAQ5HZ5IH5IPRZLNQXAW', N'499fcad6-06e6-4f75-b823-ac9c68fd02a9', N'123321123', 0, 0, NULL, 1, 0, N'Kietttaaa', CAST(N'2024-07-27T16:20:49.0508901' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'774ec38e-356a-4e7c-b15c-b7425a6e1002', 0, N'Phước Đông', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722450937/bachemw2nesmu8be2zbw.jpg', N'kietphake', N'KIETPHAKE', N'kiethttps32354@fpt.edu.vn', N'KIETHTTPS32354@FPT.EDU.VN', 0, N'AQAAAAIAAYagAAAAEP5I6ZiJtsjpxM43wvhidbLJbDduLNtNTNhFsFN1QgZwBZ2IGMstpXQG04nLz966OQ==', N'028b0854-9c1c-4bcb-af62-d50a984dcf11', N'6ea0e41e-1970-4ae6-a2d9-d34602b5ab70', N'0965335702', 0, 0, NULL, 0, 0, N'kietphake', CAST(N'2024-07-31T18:36:01.1220400' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'7d0d4c8c-826a-4f5a-8f70-5865e6499670', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'tienphan00116@gmail.com', N'TIENPHAN00116@GMAIL.COM', N'tienphan00116@gmail.com', N'TIENPHAN00116@GMAIL.COM', 0, NULL, N'JFNTR6JJTC4WGBXMSP3OG6SLYAUODLT2', N'21f33948-6690-498c-b120-3bbbc1130db0', NULL, 0, 0, NULL, 1, 0, N'Tiến Phan Đức', CAST(N'2024-07-31T14:47:01.0203948' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'7g819203-1234-56gh-ij78-90123def5678', 0, N'Phước Đông Cần Đước Long An', N'string', N'Nghia', N'NGHIA', N'Nghia@example.com', N'NGHIA@EXAMPLE.COM', 0, N'AQAAAAEAACcQAAAAENkRLHXON3RUQTnMKPXLT6Pl8cLRHQ2MM5Z3Z7QX7XF8QJTM0L4RZ9NRRCML==', N'a068e000-3c25-4d19-ba7b-d85f4bef82c5', N'f9223755-b055-40b2-ac7a-bed28535254d', N'3211233211', 0, 0, NULL, 0, 0, N'Nghia', CAST(N'2024-07-18T18:37:37.2002834' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'86a7e944-b06f-43d0-9fe8-dd8fe9f16b04', 1, N'WashingtonDc', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1721895570/tkytxzisegpyxngdrxpy.png', N'notfound404', N'NOTFOUND404', N'chienprivate321321321321@gmail.com', N'CHIENPRIVATE321321321321@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEFuSu/wdyxMprjbWT7Wb3iqMfUP4VfbRS7bD5AkJbsjvdsDnXtezmctGREZosQtz4g==', N'2RZ7EE3OGRMZZTFLD63QRHZHLXSOF27Q', N'fabbcbce-f57f-46a5-98e8-97d2c6b10890', N'0937806350', 0, 0, NULL, 1, 0, N'notfound404', CAST(N'2024-07-25T08:20:00.1463531' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'885b4306-2035-4b81-a9cb-3af2435f62cf', 1, N'Phước Đông', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722450189/dglwzfzdjetw7bpalprs.jpg', N'kietphake1', N'KIETPHAKE1', N'kiethttps32354@fpt.edu.vn', N'KIETHTTPS32354@FPT.EDU.VN', 0, N'AQAAAAIAAYagAAAAEFAwjrBxGydgskxCXxfuKbeZQ18xkmIaM+tcV7EByMx/FWNLSQm0VxysTn5FYHGinw==', N'0f026dc1-b6b5-4e44-aed3-315c4062fbeb', N'cb116c44-aa9a-49a6-8f86-9c93322579cf', N'0965335702', 0, 0, NULL, 0, 0, N'kietphake1', CAST(N'2024-07-31T18:31:00.8875803' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'8b81fa05-522e-44b3-ac60-f3237617385d', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'chiennguyen2841998@gmail.com', N'CHIENNGUYEN2841998@GMAIL.COM', N'chiennguyen2841998@gmail.com', N'CHIENNGUYEN2841998@GMAIL.COM', 0, NULL, N'2ZLPQPLVMEX7HSPHCMOXJP2JN5HYXSGG', N'6df20515-14e6-4fdd-8c24-bb49a4a26634', NULL, 0, 0, NULL, 1, 0, N'chien nguyen', CAST(N'2024-07-21T05:11:55.4023847' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'8h920314-2345-67hi-jk89-01234ef67890', 0, N'505 Fir St, Coast City, CA', N'https://cdn-icons-png.flaticon.com/128/236/236831.png', N'ellen.frank', N'ELLEN.FRANK', N'ellen.frank@example.com', N'ELLEN.FRANK@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEOKSLJXPO4SUQTnNLQXLU7Qn9dL4KQ3NN6Z3Z8QY8YG9QKUN1M5RZ0NSSPMK==', N'RANDOMSECURITYSTAMP8', N'RANDOMCONCURRENCYSTAMP8', N'555-666-7777', 1, 0, NULL, 1, 0, N'Ellen Frank', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'935707cb-4fa5-4315-b5fb-0c740ca03690', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'duchoangzzz24@gmail.com', N'DUCHOANGZZZ24@GMAIL.COM', N'duchoangzzz24@gmail.com', N'DUCHOANGZZZ24@GMAIL.COM', 0, NULL, N'KPASOTARGGNHHHJUXHJE3AFZD7RVZ6WU', N'531d2a5c-2d8b-4902-a1a5-124e1df28ef0', NULL, 0, 0, NULL, 1, 0, N'hoàng Duc', CAST(N'2024-07-31T02:34:21.4091724' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'95a2e0c6-32a0-4add-877d-60aa3f70b649', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'chiennnps27765@fpt.edu.vn', N'CHIENNNPS27765@FPT.EDU.VN', N'chiennnps27765@fpt.edu.vn', N'CHIENNNPS27765@FPT.EDU.VN', 0, NULL, N'CUWC7YK7E5TSF7MRNAZZVM362ABC5TVP', N'e1c808e1-7a5b-47e3-bc01-aa79be09ea70', NULL, 0, 0, NULL, 1, 0, N'Nguyen Ngoc Chien (FPL HCM)', CAST(N'2024-07-21T10:36:51.5351693' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'96cad7d9-9bec-4036-b902-79f46699dbd5', 1, N'Phước Đông', NULL, N'kietaabbcd', N'KIETAABBCD', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEN+WhuLtSzrvkZU4CJC6M9O/QKPXZAq9VC00molLGCP2MzPRbeiiKE47kRmwbsVVnQ==', N'KHT6LOBURK4GNMBMVVGQ2AKXMC2CRGKQ', N'99421a37-b833-4ad9-935e-3a43a032ea45', N'0965335702', 0, 0, NULL, 1, 0, N'kietaabbcd', CAST(N'2024-07-28T01:40:11.3442352' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'976b58f3-5afa-42d3-955a-8eb32e40c919', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'trinhtruonga6a@gmail.com', N'TRINHTRUONGA6A@GMAIL.COM', N'trinhtruonga6a@gmail.com', N'TRINHTRUONGA6A@GMAIL.COM', 0, NULL, N'TGEKFHLKKCBTG3FXZQZ5ERTYXFYA5JOL', N'fd1fba27-3f13-4e8f-b920-3457fcbfce23', NULL, 0, 0, NULL, 1, 0, N'Trịnh Trường 5403', CAST(N'2024-07-20T12:38:46.5168353' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'9bd8aa03-98f3-4f94-a087-753824f870eb', 1, N'Go Vap', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722578023/vchtowhuhivlaccrubsy.png', N'Huynh Kiet', N'HUYNH KIET', N'kiet43012@gmail.com', N'KIET43012@GMAIL.COM', 0, NULL, N'4014d943-208a-44b2-8f50-ef110f199b10', N'7e6b34cb-6343-49e1-bb08-0f4de788bc41', N'0965335702', 0, 0, NULL, 0, 0, N'Huynh Kiet', CAST(N'2024-08-02T05:53:44.7799678' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'9c8ae79e-2fbb-47be-aff8-0360bb2fcc6e', 0, N'Phước Đông', NULL, N'kietaabc', N'KIETAABC', N'huynahtrantuankiet2901@gmail.com', N'HUYNAHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEEvernT1T+rAKiv8lmLACRYkehpbnir5Q4+EFxDVO6wh9HZf7x9Xj9j1YbrUm+DXEw==', N'RRGLAPNHOJ7DSPTIYFOOHC5HPPGDL4EW', N'6059bb10-1132-4038-9f80-d221731000b8', N'0965335702', 0, 0, NULL, 1, 0, N'kietaabc', CAST(N'2024-07-28T01:15:41.8688560' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'9ede9ff2-da6a-4655-8c1f-26224ed2c3da', 1, N'string', N'string', N'KietTestPass1', N'KIETTESTPASS1', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEKSLY4roOwR/6kjI70WEwws5nUViqbMIHbQHhmCn57sNa0ZLab1g8vXk4hI/rGDHNg==', N'JQOZW5JMH2DUMNVLK5BMA6XYDOTEUGA6', N'48f806ba-b3a4-49aa-9783-c72472dda942', N'1234567890', 0, 0, NULL, 1, 0, N'KietTestPass1', CAST(N'2024-08-04T10:50:11.6870079' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'9i031425-3456-78ij-kl90-12345f678901', 1, N'606 Cedar St, Bludhaven, NJ', N'https://cdn-icons-png.flaticon.com/128/848/848043.png', N'frank.green', N'FRANK.GREEN', N'frank.green@example.com', N'FRANK.GREEN@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEPOSMJXQO5TVQToNLQYMV8RnAdM5KR4OP6Y4Y9QZ9ZH0RLVPM2N0S1OSTPMN==', N'RANDOMSECURITYSTAMP9', N'RANDOMCONCURRENCYSTAMP9', N'666-777-8888', 1, 0, NULL, 1, 0, N'Frank Green', CAST(N'2024-07-16T13:56:21.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'a0f79a6a-936f-4d15-93bc-ea97699abce8', 1, N'string', N'string', N'KietTestPass', N'KIETTESTPASS', N'KietTestPass@example.com', N'KIETTESTPASS@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAECE4NxOvR+aBiBRbZmMleyD2PKZtTPg0jLYc9xr3wykwaV0nACfWu1s39Fwz3IWxGw==', N'K2FDYPJNNQ6WCHLBCRYEAHRTRO2D5K2V', N'4bbb608f-4ae4-4c24-bffc-e6921c94a6ea', N'1234567890', 0, 0, NULL, 1, 0, N'KietTestPass', CAST(N'2024-08-04T10:49:14.6795751' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'a7d56ce3-ff8c-410d-9609-c3aec739bb26', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'truongtvps31921@fpt.edu.vn', N'TRUONGTVPS31921@FPT.EDU.VN', N'truongtvps31921@fpt.edu.vn', N'TRUONGTVPS31921@FPT.EDU.VN', 0, NULL, N'7KDLVWPGQAECVEXNSMSRRIEESG3L43HH', N'd9a9ed6f-80f2-45ed-a975-8665c930c03c', NULL, 0, 0, NULL, 1, 0, N'Trinh Van Truong (FPL HCM)', CAST(N'2024-07-26T14:43:53.9606349' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'a934505a-6a28-482d-99b6-194e69ea8338', 0, N'string', N'string', N'keit', N'KEIT', N'keit@example.com', N'KEIT@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEPDPrOGP45db3SqjpiTdcwHOSHloNL3PHTYbInxSF/dbWVdAC6jhlTVfJ8m0tsG/yQ==', N'F4TTFPEICERACFDI2BBTQZKXSWNVZGUC', N'1489c145-06fb-41a7-a710-ea6f7b38a4c2', N'1233211233', 0, 0, NULL, 1, 0, N'keit', CAST(N'2024-07-27T15:30:29.2326959' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'c6a608e7-292d-484b-9ddb-4f0f122fe9a8', 1, N'Light Center', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722447870/iv8ofocsi4hmn4zcp28j.jpg', N'kietreal', N'KIETREAL', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEIfAetLRAlZDyzTmch9uUjHZ4KIDhOscLkbjR0ZX55aDlPTX4uZ41pwsfWhYycYqAw==', N'KMSNA4IM52RMXKGEMSTDQCN4VFZE5I3U', N'119d9b78-fb23-4ade-89d0-582b31448aab', N'0965335702', 0, 0, NULL, 1, 0, N'kietreal', CAST(N'2024-07-31T17:44:39.3170946' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'd7db2ebc-bfba-4c91-b04b-9bdc0a5d8e33', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'ht24042002@gmail.com', N'HT24042002@GMAIL.COM', N'ht24042002@gmail.com', N'HT24042002@GMAIL.COM', 0, NULL, N'YH6V77GUUD5RY2WK5RWOX6OO6A4ZYNDF', N'ac85a752-beb4-46f0-a4bc-422fa4bbbf70', NULL, 0, 0, NULL, 1, 0, N'hoang truong', CAST(N'2024-07-28T12:22:49.3941687' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'dee68335-e3d0-44f8-96b1-e8b5c703cae2', 1, N'Thành phố Lạng Sơn', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722772141/ovurljyn8cxwrqeb5ywc.jpg', N'Admin3000', N'ADMIN3000', N'chiennnps27765@gmail.com', N'CHIENNNPS27765@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEPCl/zckUhA+sjacaHdhQb2c5XT01AcdErZK7MeZlCqQyKyUozHiUVtN+GTBfscH+g==', N'YRKDFIDNMZME2T6UE34JKEMSXUDR27V2', N'eb516029-e93c-4fd1-996e-9f2c1e74a734', N'0937806350', 0, 0, NULL, 1, 0, N'Admin3000', CAST(N'2024-08-04T11:49:07.3924865' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'df616ea5-06d0-49bc-805d-4fb8830e60bc', 0, N'tphcm', N'https://placehold.co/300x300', N'cdmasd', N'CDMASD', N'tienphan@example.com', N'TIENPHAN@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAELWVRRHBxth7jhg4DXOBIrkHN3uTEJPAAs79kBU7NidfYqSFthSZc73OUkjn4Ws24Q==', N'LAOUSFJQELEHYNQB564AF722UCDXOHZA', N'3720a84d-67e9-4119-ba50-7b2a650ef7af', N'0858839459', 0, 0, NULL, 1, 0, N'cdmasd', CAST(N'2024-07-24T02:17:46.0868972' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'e0150e40-6908-43eb-9ea1-9395a4b4328f', 0, N'Go Cong, Tien Giang', N'string', N'Anh', N'ANH', N'Anh@example.com', N'ANH@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEO4Fyx/ltvgTorFQwSXJDAYYO97vijIssyIGCgKUrDTqQESDXU5BD8ENaUjl4jyLNw==', N'056c0e98-5ef8-453c-9762-5c3c9860c9bc', N'6f334f17-735f-4929-ba53-8d025df30bb1', N'09984661242', 0, 0, NULL, 0, 0, N'Anh', CAST(N'2024-07-18T18:43:06.5669526' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'e9326160-8364-4bfa-9a94-8dd769c611eb', 0, N'Phước Đông', NULL, N'kietaabbaa', N'KIETAABBAA', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEBpV4z+ZkkuSIH1lwKmmbYWthKyKfIKUVrRTatuUxH1SOmRkXq/bGW/2ZlCMWJbjUg==', N'UHK5T4VSMNA5ACNT4MUTT4DEK75OYBA2', N'c1c7a105-dbee-4163-8998-1e706e21f95b', N'0965335702', 0, 0, NULL, 1, 0, N'kietaabbaa', CAST(N'2024-07-28T01:48:34.1370009' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'e9942b5d-af84-4696-b314-6c72449daa84', 0, N'Hãy cập nhật địa chỉ : ', N'https://img.freepik.com/free-photo/tasty-burger-isolated-white-background-fresh-hamburger-fastfood-with-beef-cheese_90220-1063.jpg?size=338&ext=jpg&ga=GA1.1.2008272138.1721433600&semt=sph', N'hisnameham@gmail.com', N'HISNAMEHAM@GMAIL.COM', N'hisnameham@gmail.com', N'HISNAMEHAM@GMAIL.COM', 0, NULL, N'LOUPFL7Q5I4LZKBF7BRY2TZAKHLGR7FF', N'37e6abe3-4051-4edf-9671-2922a50d9cda', NULL, 0, 0, NULL, 1, 0, N'His Name', CAST(N'2024-07-25T08:12:23.6151476' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'f06c16ff-7ddf-47de-b9fc-c6c59dadc599', 0, N'string', N'đsdấdsadsadsa', N'chien123', N'CHIEN123', N'chienprivate1234@gmail.com', N'CHIENPRIVATE1234@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEKJfdfRCrV8qlhT1HtaDOULPlzktHDrY53oTXfcCnXnlAg4SwCIAiezSbK7PxX2kog==', N'ULGXIIRWH2JAYILN5FAIS6F5S24ILJHI', N'cb945401-6cd2-4e2f-8a71-dadddc6e63b5', N'0937806350', 0, 0, NULL, 1, 0, N'chien123', CAST(N'2024-07-24T14:40:25.7390396' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'faf2f262-5c47-4d64-8fef-10cbaf131b4a', 0, N'string', N'string', N'kiettest123', N'KIETTEST123', N'huynhtrantuankiet2901@gmail.com', N'HUYNHTRANTUANKIET2901@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEAu1EW9BUnMKlGo8NJkPsn73OYVR4G1f6zbozq4uRndB3Hd2JGKiOCFLUOsI7vqDEg==', N'VPT33MVIVI4EGSYJ6I7FBVQVCDWJVAYY', N'0a818a4a-bc3c-47f9-8619-b4075c4dea5f', N'123321123', 0, 0, NULL, 1, 0, N'kiettest123', CAST(N'2024-07-29T01:59:17.4625817' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'fdc87154-20a5-4b72-8f4f-2b418640d7a6', 1, N'Quang Trung, Gò Vấp', N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722576865/djtn0cqhb35zlmoeqogj.png', N'KietAbcd', N'KIETABCD', N'KietAbcd@example.com', N'KIETABCD@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEPRXJWfRfhJZk/J3VbYYn7XwVCSkuo+yefh2GrQaA3WrXejnHFVF2q9kOq6aJcncLQ==', N'4d1eb468-755c-467d-b425-ba6d4eeb56d6', N'4f1238c4-f6f7-4608-bbf3-6a3f99b21c55', N'123321123', 0, 0, NULL, 0, 0, N'KietAbcd', CAST(N'2024-08-02T05:34:32.8540722' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Address], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [CreateAt], [Status]) VALUES (N'fe8588cb-e305-4837-8ac6-2b2806ad99ac', 0, N'string', N'string', N'rZT2Qlc3PyXFRLUtLbSExNEYHWanf00SrzblvIM78WwMWR6vjd', N'RZT2QLC3PYXFRLUTLBSEXNEYHWANF00SRZBLVIM78WWMWR6VJD', N'user@example.com', N'USER@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEO1tmUSdIS+R+4DmSxcyZlHHQz5pBtpmRnAkB1ARmnJJh/AXoAFOKOBwNiAeL52yaA==', N'NWGW46IUAWQNX3YEUWXJ2SIO7DM5QAWW', N'aa8bbe96-1008-42f6-aacd-1bb6ee43e1bd', N'0938842928', 0, 0, NULL, 1, 0, N'rZT2Qlc3PyXFRLUtLbSExNEYHWanf00SrzblvIM78WwMWR6vjd', CAST(N'2024-07-24T07:54:59.3605256' AS DateTime2), 0)
GO
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'0fee28f6-b8b1-4092-9979-f3a10edebf7f', 12, 1, CAST(80000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'976b58f3-5afa-42d3-955a-8eb32e40c919', 12, 3, CAST(240000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'e9942b5d-af84-4696-b314-6c72449daa84', 12, 1, CAST(80000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', 15, 1, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'7d0d4c8c-826a-4f5a-8f70-5865e6499670', 15, 1, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'8b81fa05-522e-44b3-ac60-f3237617385d', 15, 2, CAST(120000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'0fee28f6-b8b1-4092-9979-f3a1aedexf7f', 16, 1, CAST(30000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', 16, 2, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'4d5e6f70-8901-23de-ef45-67890abcdef2', 28, 1, CAST(80000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'2l364758-6789-01lm-no23-45678i901234', 30, 1, CAST(80000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'd7db2ebc-bfba-4c91-b04b-9bdc0a5d8e33', 38, 1, CAST(80000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'7d0d4c8c-826a-4f5a-8f70-5865e6499670', 41, 1, CAST(15000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartDetail] ([UserId], [ProductId], [Quantity], [Total]) VALUES (N'9i031425-3456-78ij-kl90-12345f678901', 41, 1, CAST(80000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Combo Chicken')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Combo Hamburger')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Combo Noodles')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Chicken')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (5, N'Hamburger')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (6, N'Chips Frries')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (7, N'Noodles')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (8, N'Drinks')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (9, N'Softie')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (19, 34, CAST(70000.00 AS Decimal(18, 2)), 1, CAST(80000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (28, 12, CAST(80000.00 AS Decimal(18, 2)), 3, CAST(240000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (29, 12, CAST(80000.00 AS Decimal(18, 2)), 3, CAST(240000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (33, 15, CAST(60000.00 AS Decimal(18, 2)), 1, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (33, 16, CAST(30000.00 AS Decimal(18, 2)), 2, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (36, 15, CAST(60000.00 AS Decimal(18, 2)), 1, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (36, 16, CAST(30000.00 AS Decimal(18, 2)), 2, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (37, 15, CAST(60000.00 AS Decimal(18, 2)), 1, CAST(60000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Total]) VALUES (37, 16, CAST(30000.00 AS Decimal(18, 2)), 2, CAST(60000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [UserId], [ReceviedDate], [FullName], [PhoneNumber], [Address], [PaymentType], [PaymentStatus], [OrderStatus], [note], [OrderDate]) VALUES (19, N'df616ea5-06d0-49bc-805d-4fb8830e60bc', NULL, N'Nguyễn Văn A', N'0384957123', N'12 A, Đường B, Tp. Hồ Chí Minh', N'MOMO', N'Đã thanh toán', N'Đang chuẩn bị', NULL, CAST(N'2024-07-31T09:44:43.6662838' AS DateTime2))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ReceviedDate], [FullName], [PhoneNumber], [Address], [PaymentType], [PaymentStatus], [OrderStatus], [note], [OrderDate]) VALUES (28, N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', NULL, N'Nguyễn Ngọc Chiến', N'0937806350', N'300/33/1 Newyork City, BateryPlace', N'COD', N'Chưa thanh toán', N'Đang chuẩn bị', NULL, CAST(N'2024-08-04T11:08:30.3928942' AS DateTime2))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ReceviedDate], [FullName], [PhoneNumber], [Address], [PaymentType], [PaymentStatus], [OrderStatus], [note], [OrderDate]) VALUES (29, N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', NULL, N'Nguyễn Ngọc Chiến', N'0937806350', N'300/33/1 Newyork City, BateryPlace', N'COD', N'Chưa thanh toán', N'Đang chuẩn bị', NULL, CAST(N'2024-08-04T11:26:28.6162322' AS DateTime2))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ReceviedDate], [FullName], [PhoneNumber], [Address], [PaymentType], [PaymentStatus], [OrderStatus], [note], [OrderDate]) VALUES (33, N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', NULL, N'Nguyễn Ngọc Chiến', N'0798260442', N'string', N'string', N'string', N'Đang chuẩn bị', N'string', CAST(N'2024-08-04T22:26:25.5299079' AS DateTime2))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ReceviedDate], [FullName], [PhoneNumber], [Address], [PaymentType], [PaymentStatus], [OrderStatus], [note], [OrderDate]) VALUES (36, N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', NULL, N'string', N'0873332166', N'string', N'string', N'string', N'Đang chuẩn bị', N'string', CAST(N'2024-08-04T22:46:42.9808324' AS DateTime2))
INSERT [dbo].[Orders] ([OrderId], [UserId], [ReceviedDate], [FullName], [PhoneNumber], [Address], [PaymentType], [PaymentStatus], [OrderStatus], [note], [OrderDate]) VALUES (37, N'4f70ab2f-a12b-4f3d-9266-dc0babf5a636', NULL, N'string', N'0707068769', N'string', N'string', N'string', N'Đang chuẩn bị', N'string', CAST(N'2024-08-04T22:54:29.2001953' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (12, N'SIÊU TIẾT KIỆM 1', N'BAO GỒM 1 BURGER TÔM + NƯỚC NGỌT + KEM', CAST(80000.00 AS Decimal(18, 2)), 2366, 1, 1, 2, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/11f3e6435b23ab624dc55c2d3fe9479d/5/0/500x500_jollisaver-04.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (15, N'SIÊU TIẾT KIỆM 2 ', N'BAO GỒM 1 BURGER GÀ GIÒN + NƯỚCNGỌT', CAST(60000.00 AS Decimal(18, 2)), 72, 1, 1, 2, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/o/bo_go_ga_nc.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (16, N'MIẾNG GÀ GIÒN VUI VẺ', N'BAO GỒM 1 MIẾNG GÀ GIÒN', CAST(30000.00 AS Decimal(18, 2)), 873, 1, 1, 4, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/1/_/1_mi_ng_ggvv_png_1.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (17, N'GÀ GIÒN VUI VẺ', N'BAO GỒM 2 MIẾNG GÀ GIÒN', CAST(55000.00 AS Decimal(18, 2)), 242, 1, 1, 4, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/d/1/d1834d87116836-2mingggin_1.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (18, N'GÀ GIÒN VUI VẺ', N'BAO GỒM 4 MIẾNG GÀ GIÒN', CAST(100000.00 AS Decimal(18, 2)), 1125, 1, 1, 4, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/4/2/427e7a3136f84a-4mingggin_1.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (24, N'COMBO JOLLY HOTDOG', N'BAO GỒM JOLLY HOTDOG + NƯỚC NGỌT', CAST(80000.00 AS Decimal(18, 2)), 33, 1, 1, 2, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/h/o/hotdog_web-02.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (27, N'KHOAI TÂY CHIÊN (VỪA)', N'BAO GỒM KHOAI TÂY CHIÊN (VỪA)', CAST(20000.00 AS Decimal(18, 2)), 221, 1, 1, 6, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/5/5/5532107fb902fd-1860001_khoaivua21.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (28, N'KHOAI TÂY LẮC VỊ BBQ (VỪA)', N'BAO GỒM KHOAI TÂY LẮC VỊ BBQ (VỪA)', CAST(25000.00 AS Decimal(18, 2)), 551, 1, 1, 6, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/k/h/khoai-16.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (30, N'KHOAI TÂY LẮC VỊ BBQ (LỚN)', N'BAO GỒM KHOAI TÂY LẮC VỊ BBQ (LỚN)', CAST(35000.00 AS Decimal(18, 2)), 2323, 1, 1, 6, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/k/h/khoai-16_1.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (34, N'MÌ Ý SỐT BÒ BẰM LỚN', N'BAO GỒM MÌ Ý SỐT BÒ BẰM LỚN', CAST(70000.00 AS Decimal(18, 2)), 121, 1, 1, 7, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m__2.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (36, N'COMBO1 MÌ Ý SỐT BÒ BẰM', N'BAO GỒM MÌ Ý BÒ BẰM + NƯỚC NGỌT', CAST(90000.00 AS Decimal(18, 2)), 1590, 1, 1, 3, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/y/my_nho_nc.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (38, N'COMBO2 MÌ Ý SỐT BÒ BẰM ', N'BAO GỒM MÌ Ý SỐT BÒ BẰM + KHOAI TÂY VỪA + NƯỚC NGỌT', CAST(120000.00 AS Decimal(18, 2)), 4565, 1, 1, 3, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/y/my_nho_khoai_nc.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (40, N'PEPSI', N'PEPSI VỪA', CAST(15000.00 AS Decimal(18, 2)), 1240, 1, 1, 8, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/2/2/22a5960148a32e-2mienggaran14.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (41, N'7 UP', N'7 UP VỪA', CAST(15000.00 AS Decimal(18, 2)), 2356, 1, 1, 8, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/7/6/76632fe162df44-1.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (42, N'NƯỚC ÉP XOÀI ĐÀO', N'BAO GỒM NƯỚC ÉP XOÀI ĐÀO', CAST(20000.00 AS Decimal(18, 2)), 2342, 1, 1, 8, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/x/o/xoai_dao_menu.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (43, N'KEM SÔCÔLA (CÚP)', N'BAO GỒM KEM SÔCÔLA (CÚP)', CAST(25000.00 AS Decimal(18, 2)), 1245, 1, 1, 9, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/c/4/c400652c2a03e0-chocolateicecream01.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (44, N'KEM SỮA TƯƠI (CÚP)', N'BAO GỒM KEM SỮA TƯƠI (CÚP)', CAST(25000.00 AS Decimal(18, 2)), 574, 1, 1, 9, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/a/ba9d396f70568c-kemvani201.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (45, N'KEM SUNDAES DÂU', N'BAO GỒM KEM SUNDAES DÂU', CAST(30000.00 AS Decimal(18, 2)), 252, 1, 1, 9, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/d/0/d01402ed11976b-kemsundeadau.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (46, N'KEM SUNDAES SÔCÔLA', N'BAO GỒM KEM SUNDAES SÔCÔLA', CAST(35000.00 AS Decimal(18, 2)), 150, 1, 1, 9, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/1/9/192dcb48e7393a-kemsocola16.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (68, N'Đẹp Trai', N'Đẹp Trai', CAST(15000.00 AS Decimal(18, 2)), 2, 1, 1, 4, CAST(N'2024-08-04T20:24:14.3180132' AS DateTime2), N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722803076/g4z2li7ngqaobena1tvg.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [View], [IsActive], [IsCombo], [CategoryId], [CreateAt], [ImageUrl]) VALUES (69, N'test6', N'aaaaaaaaaaaaaaaaaa', CAST(100000.00 AS Decimal(18, 2)), 1, 1, 1, 1, CAST(N'2024-08-05T00:56:05.8497210' AS DateTime2), N'https://res.cloudinary.com/dlfvbe9bi/image/upload/v1722819401/m6ja4z8rbq0nbru2vlvk.png')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CartDetail_UserId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_CartDetail_UserId] ON [dbo].[CartDetail]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetail_ProductId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetail_ProductId] ON [dbo].[OrderDetail]
(
	[ProductId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_CategoryId]    Script Date: 8/5/2024 8:24:33 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_CategoryId] ON [dbo].[Product]
(
	[CategoryId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CartDetail] ADD  DEFAULT (N'') FOR [UserId]
GO
ALTER TABLE [dbo].[CartDetail] ADD  DEFAULT ((0.0)) FOR [Total]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'') FOR [ImageUrl]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Product_ProductId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category_CategoryId]
GO
ALTER DATABASE [FatFoodDB] SET  READ_WRITE 
GO
