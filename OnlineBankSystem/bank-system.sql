USE [master]
GO
/****** Object:  Database [OnlineBankSystem]    Script Date: 5/28/2018 5:34:06 PM ******/
CREATE DATABASE [OnlineBankSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineBankSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\OnlineBankSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineBankSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\OnlineBankSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [OnlineBankSystem] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineBankSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineBankSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineBankSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineBankSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OnlineBankSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineBankSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [OnlineBankSystem] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineBankSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineBankSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineBankSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineBankSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineBankSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlineBankSystem', N'ON'
GO
ALTER DATABASE [OnlineBankSystem] SET QUERY_STORE = OFF
GO
USE [OnlineBankSystem]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [OnlineBankSystem]
GO
/****** Object:  Table [dbo].[Bills]    Script Date: 5/28/2018 5:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IBAN] [nchar](22) NULL,
	[Amount] [decimal](12, 2) NULL,
 CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfers]    Script Date: 5/28/2018 5:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderIBAN] [nchar](22) NOT NULL,
	[ReceiverIBAN] [nchar](22) NOT NULL,
	[Amount] [decimal](12, 2) NOT NULL,
	[Description] [nvarchar](32) NOT NULL,
	[IsFinished] [bit] NOT NULL,
 CONSTRAINT [PK_Transfers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBills]    Script Date: 5/28/2018 5:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBills](
	[UserId] [int] NOT NULL,
	[BillId] [int] NOT NULL,
 CONSTRAINT [PK_UserBills] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[BillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/28/2018 5:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](30) NOT NULL,
	[FirstName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
	[PasswordHash] [nchar](64) NOT NULL,
	[PasswordSalt] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bills] ON 

INSERT [dbo].[Bills] ([Id], [IBAN], [Amount]) VALUES (1, N'DthH3e08PFPgo4FlmzAE2g', CAST(630.01 AS Decimal(12, 2)))
INSERT [dbo].[Bills] ([Id], [IBAN], [Amount]) VALUES (2, N'JJDuX6oSK1GkxgKKi72pN7', CAST(1000.99 AS Decimal(12, 2)))
INSERT [dbo].[Bills] ([Id], [IBAN], [Amount]) VALUES (3, N'UDVI4VqYLT8eDXCPwNKtDu', CAST(23940.23 AS Decimal(12, 2)))
INSERT [dbo].[Bills] ([Id], [IBAN], [Amount]) VALUES (4, N'E6MCI9GT5L4b263oWQIfWc', CAST(9000.00 AS Decimal(12, 2)))
INSERT [dbo].[Bills] ([Id], [IBAN], [Amount]) VALUES (5, N'FFJVT4XLXYjWCtqwpZgRDV', CAST(159.88 AS Decimal(12, 2)))
SET IDENTITY_INSERT [dbo].[Bills] OFF
SET IDENTITY_INSERT [dbo].[Transfers] ON 

INSERT [dbo].[Transfers] ([Id], [SenderIBAN], [ReceiverIBAN], [Amount], [Description], [IsFinished]) VALUES (2, N'DthH3e08PFPgo4FlmzAE2g', N'FFJVT4XLXYjWCtqwpZgRDV', CAST(23.00 AS Decimal(12, 2)), N'Bank transaction', 1)
INSERT [dbo].[Transfers] ([Id], [SenderIBAN], [ReceiverIBAN], [Amount], [Description], [IsFinished]) VALUES (4, N'DthH3e08PFPgo4FlmzAE2g', N'FFJVT4XLXYjWCtqwpZgRDV', CAST(11.00 AS Decimal(12, 2)), N'Food Money', 1)
INSERT [dbo].[Transfers] ([Id], [SenderIBAN], [ReceiverIBAN], [Amount], [Description], [IsFinished]) VALUES (5, N'DthH3e08PFPgo4FlmzAE2g', N'FFJVT4XLXYjWCtqwpZgRDV', CAST(23.00 AS Decimal(12, 2)), N'Money for chicken', 1)
INSERT [dbo].[Transfers] ([Id], [SenderIBAN], [ReceiverIBAN], [Amount], [Description], [IsFinished]) VALUES (6, N'DthH3e08PFPgo4FlmzAE2g', N'FFJVT4XLXYjWCtqwpZgRDV', CAST(23.45 AS Decimal(12, 2)), N'Beer money', 0)
INSERT [dbo].[Transfers] ([Id], [SenderIBAN], [ReceiverIBAN], [Amount], [Description], [IsFinished]) VALUES (7, N'DthH3e08PFPgo4FlmzAE2g', N'UDVI4VqYLT8eDXCPwNKtDu', CAST(11.00 AS Decimal(12, 2)), N'Clothes', 1)
INSERT [dbo].[Transfers] ([Id], [SenderIBAN], [ReceiverIBAN], [Amount], [Description], [IsFinished]) VALUES (8, N'DthH3e08PFPgo4FlmzAE2g', N'FFJVT4XLXYjWCtqwpZgRDV', CAST(12.45 AS Decimal(12, 2)), N'Shorts', 0)
INSERT [dbo].[Transfers] ([Id], [SenderIBAN], [ReceiverIBAN], [Amount], [Description], [IsFinished]) VALUES (10, N'DthH3e08PFPgo4FlmzAE2g', N'FFJVT4XLXYjWCtqwpZgRDV', CAST(20.99 AS Decimal(12, 2)), N'Food money', 1)
SET IDENTITY_INSERT [dbo].[Transfers] OFF
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (1, 1)
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (1, 3)
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (3, 1)
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (3, 4)
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (3, 5)
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (4, 1)
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (4, 2)
INSERT [dbo].[UserBills] ([UserId], [BillId]) VALUES (5, 2)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [FirstName], [LastName], [PasswordHash], [PasswordSalt]) VALUES (1, N'pesho12', N'Pesho', N'Goshev', N'E1BE5CD42A6F1EB7EEE1C3AAE61E388BD98110F0A64ED8C09B7BCB0A5DAC33C6', N'PCeG3KU5x5')
INSERT [dbo].[Users] ([Id], [Username], [FirstName], [LastName], [PasswordHash], [PasswordSalt]) VALUES (3, N'gosho12', N'Gosho', N'Peshev', N'E1BE5CD42A6F1EB7EEE1C3AAE61E388BD98110F0A64ED8C09B7BCB0A5DAC33C6', N'PCeG3KU5x5')
INSERT [dbo].[Users] ([Id], [Username], [FirstName], [LastName], [PasswordHash], [PasswordSalt]) VALUES (4, N'minka90', N'Minka', N'Mincheva', N'E27620B6AC3B07E8824FBC875FB1380EF7B331288F2214EB17D667F5A7D2B7AB', N'ePMkHIh0FJ')
INSERT [dbo].[Users] ([Id], [Username], [FirstName], [LastName], [PasswordHash], [PasswordSalt]) VALUES (5, N'kostadin63', N'Kostadin', N'Ivanov', N'E27620B6AC3B07E8824FBC875FB1380EF7B331288F2214EB17D667F5A7D2B7AB', N'ePMkHIh0FJ')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Bills__8235CCBCE6F5B067]    Script Date: 5/28/2018 5:34:06 PM ******/
ALTER TABLE [dbo].[Bills] ADD UNIQUE NONCLUSTERED 
(
	[IBAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E47D732FA1]    Script Date: 5/28/2018 5:34:06 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transfers] ADD  DEFAULT ((0)) FOR [IsFinished]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD FOREIGN KEY([ReceiverIBAN])
REFERENCES [dbo].[Bills] ([IBAN])
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD FOREIGN KEY([SenderIBAN])
REFERENCES [dbo].[Bills] ([IBAN])
GO
ALTER TABLE [dbo].[UserBills]  WITH CHECK ADD  CONSTRAINT [FK_UserBills_Bills] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bills] ([Id])
GO
ALTER TABLE [dbo].[UserBills] CHECK CONSTRAINT [FK_UserBills_Bills]
GO
ALTER TABLE [dbo].[UserBills]  WITH CHECK ADD  CONSTRAINT [FK_UserBills_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserBills] CHECK CONSTRAINT [FK_UserBills_Users]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD CHECK  (([Amount]>=(0)))
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD CHECK  (([Amount]>(0)))
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD CHECK  ((len([Description])>(5)))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  ((len([FirstName])>(2)))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  ((len([LastName])>(2)))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  ((len([Username])>(5)))
GO
USE [master]
GO
ALTER DATABASE [OnlineBankSystem] SET  READ_WRITE 
GO
