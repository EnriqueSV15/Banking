CREATE DATABASE [DBBANKING]
GO
USE [DBBANKING]
GO
/****** Object:  User [dev_user]    Script Date: 3/09/2023 18:44:01 ******/
CREATE LOGIN [dev_user] WITH PASSWORD='123456', DEFAULT_DATABASE=[DBBANKINGPRUEBA], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [dev_user] FOR LOGIN [dev_user] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [dev_user]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 3/09/2023 18:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[clienteId] [int] IDENTITY(1,1) NOT NULL,
	[contrasena] [varchar](50) NULL,
	[estado] [bit] NULL,
	[personaId] [int] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[clienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 3/09/2023 18:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[cuentaId] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](20) NULL,
	[tipo] [int] NULL,
	[saldoInicial] [numeric](10, 2) NULL,
	[estado] [bit] NULL,
	[clienteId] [int] NOT NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[cuentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimiento]    Script Date: 3/09/2023 18:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimiento](
	[movimientoId] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[tipo] [int] NULL,
	[valor] [numeric](10, 2) NULL,
	[saldo] [numeric](10, 2) NULL,
	[cuentaId] [int] NOT NULL,
 CONSTRAINT [PK_Movimiento] PRIMARY KEY CLUSTERED 
(
	[movimientoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 3/09/2023 18:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[personaId] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](80) NULL,
	[genero] [char](1) NULL,
	[edad] [int] NULL,
	[identificacion] [char](8) NULL,
	[direccion] [varchar](150) NULL,
	[telefono] [char](9) NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[personaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 
GO
INSERT [dbo].[Cliente] ([clienteId], [contrasena], [estado], [personaId]) VALUES (1, N'1234', 1, 1)
GO
INSERT [dbo].[Cliente] ([clienteId], [contrasena], [estado], [personaId]) VALUES (2, N'5678', 1, 2)
GO
INSERT [dbo].[Cliente] ([clienteId], [contrasena], [estado], [personaId]) VALUES (3, N'1245', 1, 3)
GO
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuenta] ON 
GO
INSERT [dbo].[Cuenta] ([cuentaId], [numero], [tipo], [saldoInicial], [estado], [clienteId]) VALUES (1, N'478758', 2, CAST(2000.00 AS Numeric(10, 2)), 1, 1)
GO
INSERT [dbo].[Cuenta] ([cuentaId], [numero], [tipo], [saldoInicial], [estado], [clienteId]) VALUES (2, N'225487', 1, CAST(100.00 AS Numeric(10, 2)), 1, 2)
GO
INSERT [dbo].[Cuenta] ([cuentaId], [numero], [tipo], [saldoInicial], [estado], [clienteId]) VALUES (3, N'495878', 2, CAST(0.00 AS Numeric(10, 2)), 1, 3)
GO
INSERT [dbo].[Cuenta] ([cuentaId], [numero], [tipo], [saldoInicial], [estado], [clienteId]) VALUES (4, N'496825', 2, CAST(540.00 AS Numeric(10, 2)), 1, 2)
GO
INSERT [dbo].[Cuenta] ([cuentaId], [numero], [tipo], [saldoInicial], [estado], [clienteId]) VALUES (5, N'585545', 1, CAST(1000.00 AS Numeric(10, 2)), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Movimiento] ON 
GO
INSERT [dbo].[Movimiento] ([movimientoId], [fecha], [tipo], [valor], [saldo], [cuentaId]) VALUES (1, CAST(N'2023-09-03T06:10:32.103' AS DateTime), 2, CAST(-575.00 AS Numeric(10, 2)), CAST(1425.00 AS Numeric(10, 2)), 1)
GO
INSERT [dbo].[Movimiento] ([movimientoId], [fecha], [tipo], [valor], [saldo], [cuentaId]) VALUES (2, CAST(N'2023-09-03T06:11:29.113' AS DateTime), 1, CAST(600.00 AS Numeric(10, 2)), CAST(700.00 AS Numeric(10, 2)), 2)
GO
INSERT [dbo].[Movimiento] ([movimientoId], [fecha], [tipo], [valor], [saldo], [cuentaId]) VALUES (3, CAST(N'2023-09-03T06:17:51.177' AS DateTime), 1, CAST(150.00 AS Numeric(10, 2)), CAST(150.00 AS Numeric(10, 2)), 3)
GO
INSERT [dbo].[Movimiento] ([movimientoId], [fecha], [tipo], [valor], [saldo], [cuentaId]) VALUES (4, CAST(N'2023-09-03T06:20:58.207' AS DateTime), 2, CAST(-540.00 AS Numeric(10, 2)), CAST(0.00 AS Numeric(10, 2)), 4)
GO
SET IDENTITY_INSERT [dbo].[Movimiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Persona] ON 
GO
INSERT [dbo].[Persona] ([personaId], [nombre], [genero], [edad], [identificacion], [direccion], [telefono]) VALUES (1, N'Jose Lema', N'M', 29, N'75205320', N'Otavalo sn y principal', N'098254785')
GO
INSERT [dbo].[Persona] ([personaId], [nombre], [genero], [edad], [identificacion], [direccion], [telefono]) VALUES (2, N'Marianela Montalvo', N'F', 32, N'75205305', N'Amazonas y NNUU', N'097548965')
GO
INSERT [dbo].[Persona] ([personaId], [nombre], [genero], [edad], [identificacion], [direccion], [telefono]) VALUES (3, N'Juan Osorio', N'M', 35, N'75202405', N'13 junio y Quinoccial', N'098874587')
GO
SET IDENTITY_INSERT [dbo].[Persona] OFF
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Persona] FOREIGN KEY([personaId])
REFERENCES [dbo].[Persona] ([personaId])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_Persona]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Cliente] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Cliente] ([clienteId])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Cliente]
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK_Movimiento_Cuenta] FOREIGN KEY([cuentaId])
REFERENCES [dbo].[Cuenta] ([cuentaId])
GO
ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK_Movimiento_Cuenta]
GO