USE [master]
GO
/****** Object:  Database [Canchas_Gambeta]    Script Date: 03/07/2021 9:21:11 ******/
CREATE DATABASE [Canchas_Gambeta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Canchas_Gambeta', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.FJLORENZO\MSSQL\DATA\Canchas_Gambeta.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Canchas_Gambeta_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.FJLORENZO\MSSQL\DATA\Canchas_Gambeta_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Canchas_Gambeta] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Canchas_Gambeta].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Canchas_Gambeta] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET ARITHABORT OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Canchas_Gambeta] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Canchas_Gambeta] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Canchas_Gambeta] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Canchas_Gambeta] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Canchas_Gambeta] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Canchas_Gambeta] SET  MULTI_USER 
GO
ALTER DATABASE [Canchas_Gambeta] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Canchas_Gambeta] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Canchas_Gambeta] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Canchas_Gambeta] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Canchas_Gambeta] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Canchas_Gambeta]
GO
/****** Object:  Table [dbo].[Cancha]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cancha](
	[idCancha] [int] IDENTITY(1,1) NOT NULL,
	[tipoCancha] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Cancha] PRIMARY KEY CLUSTERED 
(
	[idCancha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DetallePedido]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallePedido](
	[idDetallePedido] [int] IDENTITY(1,1) NOT NULL,
	[pedido] [int] NOT NULL,
	[insumo] [int] NOT NULL,
	[cantidadPedida] [int] NOT NULL,
	[cantidadRecibida] [int] NULL,
	[fechaRecibido] [date] NULL,
 CONSTRAINT [PK_DetallePedido] PRIMARY KEY CLUSTERED 
(
	[idDetallePedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Email]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email](
	[idEmail] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[idEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Equipo]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipo](
	[idEquipo] [int] IDENTITY(1,1) NOT NULL,
	[nombreEquipo] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Equipo] PRIMARY KEY CLUSTERED 
(
	[idEquipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipoMails]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipoMails](
	[idEquipoMails] [int] IDENTITY(1,1) NOT NULL,
	[email] [int] NOT NULL,
	[equipo] [int] NOT NULL,
 CONSTRAINT [PK_EquipoMails] PRIMARY KEY CLUSTERED 
(
	[idEquipoMails] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Horario]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horario](
	[idHorario] [int] IDENTITY(1,1) NOT NULL,
	[horario] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Horario] PRIMARY KEY CLUSTERED 
(
	[idHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HorarioReservas]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HorarioReservas](
	[idHorarioReserva] [int] IDENTITY(1,1) NOT NULL,
	[horario] [int] NOT NULL,
	[reserva] [int] NOT NULL,
 CONSTRAINT [PK_HorarioReservas] PRIMARY KEY CLUSTERED 
(
	[idHorarioReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instrumento]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instrumento](
	[idInstrumento] [int] IDENTITY(1,1) NOT NULL,
	[instrumento] [nvarchar](100) NOT NULL,
	[fechaCompra] [date] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Instrumento] PRIMARY KEY CLUSTERED 
(
	[idInstrumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InstrumentoRepuesto]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstrumentoRepuesto](
	[idInstrumentoRepuesto] [int] IDENTITY(1,1) NOT NULL,
	[nombreInstrumento] [nvarchar](100) NOT NULL,
	[idInstrumentoRoto] [int] NOT NULL,
	[fechaRepuesto] [date] NOT NULL,
 CONSTRAINT [PK_InstrumentoRepuesto] PRIMARY KEY CLUSTERED 
(
	[idInstrumentoRepuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InstrumentoRoto]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstrumentoRoto](
	[idInstrumentoRoto] [int] IDENTITY(1,1) NOT NULL,
	[instrumento] [int] NOT NULL,
	[fechaRotura] [date] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_ElementoRoto] PRIMARY KEY CLUSTERED 
(
	[idInstrumentoRoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Insumo]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insumo](
	[idInsumo] [int] IDENTITY(1,1) NOT NULL,
	[insumo] [nvarchar](50) NOT NULL,
	[precio] [money] NOT NULL,
	[stock] [int] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Insumo] PRIMARY KEY CLUSTERED 
(
	[idInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[proveedor] [int] NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[fecha] [date] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[idProveedor] [int] IDENTITY(1,1) NOT NULL,
	[nombreCompleto] [nvarchar](50) NOT NULL,
	[empresa] [nvarchar](50) NOT NULL,
	[telefono] [nchar](10) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[idReserva] [int] IDENTITY(1,1) NOT NULL,
	[cliente] [int] NOT NULL,
	[cancha] [int] NOT NULL,
	[horario] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[servicioAsador] [bit] NOT NULL,
	[servicioInstrumentos] [bit] NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[idReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReservaInsumos]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservaInsumos](
	[idReservaInsumos] [int] IDENTITY(1,1) NOT NULL,
	[reserva] [int] NOT NULL,
	[insumo] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
 CONSTRAINT [PK_ReservaInsumos] PRIMARY KEY CLUSTERED 
(
	[idReservaInsumos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rol]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[rol] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 03/07/2021 9:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombreCompleto] [nvarchar](50) NOT NULL,
	[telefono] [nchar](10) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[equipo] [int] NULL,
	[rol] [int] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Cancha] ON 

INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (1, N'L. Messi - Futbol')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (2, N'D. Maradona - Futbol')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (3, N'J. M. del Potro - Tenis')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (4, N'D. Schwartzman - Tenis')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (5, N'S. Gutierrez - Padel')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (6, N'F. Belasteguin - Padel')
SET IDENTITY_INSERT [dbo].[Cancha] OFF
SET IDENTITY_INSERT [dbo].[DetallePedido] ON 

INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (6, 1, 1, 5, 20, CAST(N'2021-06-08' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (7, 1, 18, 10, 20, CAST(N'2021-06-08' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (8, 1, 19, 15, 20, CAST(N'2021-06-08' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (9, 1, 20, 20, 20, CAST(N'2021-06-08' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (10, 2, 12, 10, 10, CAST(N'2021-06-08' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (11, 2, 13, 20, 20, CAST(N'2021-06-08' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (12, 2, 14, 15, 15, CAST(N'2021-06-08' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (27, 4, 1, 9, 2, CAST(N'2021-06-20' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (28, 4, 18, 5, 4, CAST(N'2021-06-20' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (29, 4, 19, 15, 5, CAST(N'2021-06-20' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (30, 5, 4, 16, 10, CAST(N'2021-06-14' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (31, 5, 15, 10, 10, CAST(N'2021-06-14' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (32, 5, 16, 12, 12, CAST(N'2021-06-14' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (36, 6, 1, 1, 10, CAST(N'2021-06-22' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (37, 6, 3, 1, 10, CAST(N'2021-06-22' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (38, 6, 8, 1, 1, CAST(N'2021-06-22' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (39, 6, 18, 1, 0, CAST(N'2021-06-22' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (40, 7, 9, 15, 0, CAST(N'2021-06-22' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (41, 7, 11, 15, 0, CAST(N'2021-06-22' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (42, 7, 12, 10, 0, CAST(N'2021-06-22' AS Date))
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (43, 8, 1, 20, NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (44, 8, 18, 22, NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [pedido], [insumo], [cantidadPedida], [cantidadRecibida], [fechaRecibido]) VALUES (45, 8, 19, 18, NULL, NULL)
SET IDENTITY_INSERT [dbo].[DetallePedido] OFF
SET IDENTITY_INSERT [dbo].[Email] ON 

INSERT [dbo].[Email] ([idEmail], [email]) VALUES (4, N'qwerqwer@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (5, N'opopopo@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (48, N'lorenzofran1@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (50, N'liomessi@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (51, N'liomessi@hotmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (52, N'cande@yahoo.com.ar')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (53, N'mati992008@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (54, N'ao2.agustin.oliva@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (55, N'carmencitagimenez@hotmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (56, N'carlitosibarra@hotmail.com')
SET IDENTITY_INSERT [dbo].[Email] OFF
SET IDENTITY_INSERT [dbo].[Equipo] ON 

INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (4, N'0')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (6, N'lanus')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (18, N'boca')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (19, N'barcelona')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (25, N'talleres')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (26, N'Los Rafitas')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (27, N'Real Bañil')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (28, N'Barcelona FC')
SET IDENTITY_INSERT [dbo].[Equipo] OFF
SET IDENTITY_INSERT [dbo].[EquipoMails] ON 

INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (4, 4, 6)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (5, 5, 6)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (64, 48, 25)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (65, 48, 27)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (66, 54, 27)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (67, 53, 25)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (68, 55, 25)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (69, 56, 25)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (70, 48, 28)
SET IDENTITY_INSERT [dbo].[EquipoMails] OFF
SET IDENTITY_INSERT [dbo].[Horario] ON 

INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (1, N'11:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (2, N'12:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (3, N'13:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (4, N'14:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (5, N'15:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (6, N'16:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (7, N'17:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (8, N'18:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (9, N'19:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (10, N'20:00')
INSERT [dbo].[Horario] ([idHorario], [horario]) VALUES (11, N'21:00')
SET IDENTITY_INSERT [dbo].[Horario] OFF
SET IDENTITY_INSERT [dbo].[HorarioReservas] ON 

INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (108, 4, 152)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (109, 11, 153)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (110, 10, 154)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (111, 3, 155)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (112, 11, 156)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (113, 3, 157)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (114, 4, 158)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (115, 10, 159)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (116, 3, 160)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (117, 11, 161)
INSERT [dbo].[HorarioReservas] ([idHorarioReserva], [horario], [reserva]) VALUES (118, 2, 162)
SET IDENTITY_INSERT [dbo].[HorarioReservas] OFF
SET IDENTITY_INSERT [dbo].[Instrumento] ON 

INSERT [dbo].[Instrumento] ([idInstrumento], [instrumento], [fechaCompra], [estado]) VALUES (2, N'Raqueta Prince Thunder Dome 100 Grip 2', CAST(N'2021-06-24' AS Date), 1)
INSERT [dbo].[Instrumento] ([idInstrumento], [instrumento], [fechaCompra], [estado]) VALUES (5, N'Paleta Padel Steel Custom Air Impact Carbono', CAST(N'2021-06-24' AS Date), 1)
INSERT [dbo].[Instrumento] ([idInstrumento], [instrumento], [fechaCompra], [estado]) VALUES (7, N'Brazzuca Adidas - Mundial Brasil 2014', CAST(N'2021-06-23' AS Date), 1)
INSERT [dbo].[Instrumento] ([idInstrumento], [instrumento], [fechaCompra], [estado]) VALUES (8, N'Arco De Futbol 3 X 2 Metros Desarmable Caño De Hierro', CAST(N'2021-06-20' AS Date), 0)
INSERT [dbo].[Instrumento] ([idInstrumento], [instrumento], [fechaCompra], [estado]) VALUES (9, N'Pelota Futbol Puma Original N1 Laliga España Accelerate', CAST(N'2021-06-09' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Instrumento] OFF
SET IDENTITY_INSERT [dbo].[InstrumentoRepuesto] ON 

INSERT [dbo].[InstrumentoRepuesto] ([idInstrumentoRepuesto], [nombreInstrumento], [idInstrumentoRoto], [fechaRepuesto]) VALUES (5, N'Telstar 18 - Mundial Rusia 2018 ', 3, CAST(N'2021-06-23' AS Date))
SET IDENTITY_INSERT [dbo].[InstrumentoRepuesto] OFF
SET IDENTITY_INSERT [dbo].[InstrumentoRoto] ON 

INSERT [dbo].[InstrumentoRoto] ([idInstrumentoRoto], [instrumento], [fechaRotura], [estado]) VALUES (1, 7, CAST(N'2021-06-25' AS Date), 0)
INSERT [dbo].[InstrumentoRoto] ([idInstrumentoRoto], [instrumento], [fechaRotura], [estado]) VALUES (2, 7, CAST(N'2021-06-25' AS Date), 0)
INSERT [dbo].[InstrumentoRoto] ([idInstrumentoRoto], [instrumento], [fechaRotura], [estado]) VALUES (3, 7, CAST(N'2021-06-25' AS Date), 0)
INSERT [dbo].[InstrumentoRoto] ([idInstrumentoRoto], [instrumento], [fechaRotura], [estado]) VALUES (4, 8, CAST(N'2021-06-25' AS Date), 1)
SET IDENTITY_INSERT [dbo].[InstrumentoRoto] OFF
SET IDENTITY_INSERT [dbo].[Insumo] ON 

INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (1, N'Imperial IPA 1 Litro', 200.0000, 11, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (3, N'Agua Villavicencio 500ml', 50.0000, 28, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (4, N'Aquarius Uva 1,5 Litros', 180.0000, 25, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (8, N'Coca-Cola 500ml', 80.0000, 27, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (9, N'Fanta 500ml', 70.0000, 20, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (10, N'Sprite 500ml', 70.0000, 23, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (11, N'Coca-Cola Zero 500ml', 80.0000, 28, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (12, N'Powerade Clasica 500ml', 100.0000, 18, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (13, N'Powerade Manzana 500ml', 100.0000, 23, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (14, N'Powerade Frutas tropicales 500ml', 100.0000, 23, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (15, N'Aquarius Manzana 500ml', 70.0000, 35, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (16, N'Aquarius Pera 500ml', 70.0000, 31, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (17, N'Aquarius Pomelo 500ml', 70.0000, 22, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (18, N'Imperial Amber 1 Litro', 170.0000, 13, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (19, N'Imperial Golden 1 Litro', 150.0000, 24, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (20, N'Imperial Clasica 1 Litro', 150.0000, 5, 1)
INSERT [dbo].[Insumo] ([idInsumo], [insumo], [precio], [stock], [estado]) VALUES (25, N'Papas fritas Lays sabor Mediterraneo 200grs', 120.0000, 50, 1)
SET IDENTITY_INSERT [dbo].[Insumo] OFF
SET IDENTITY_INSERT [dbo].[Pedido] ON 

INSERT [dbo].[Pedido] ([idPedido], [proveedor], [descripcion], [fecha], [estado]) VALUES (1, 6, N'Imperial IPA 1 Litro - Cantidad: 5
Imperial Amber 1 Litro - Cantidad: 10
Imperial Golden 1 Litro - Cantidad: 15
Imperial Clasica 1 Litro - Cantidad: 20
', CAST(N'2021-06-07' AS Date), 0)
INSERT [dbo].[Pedido] ([idPedido], [proveedor], [descripcion], [fecha], [estado]) VALUES (2, 7, N'Powerade Clasica 500ml - Cantidad: 10
Powerade Manzana 500ml - Cantidad: 20
Powerade Frutas tropicales 500ml - Cantidad: 15
', CAST(N'2021-06-07' AS Date), 0)
INSERT [dbo].[Pedido] ([idPedido], [proveedor], [descripcion], [fecha], [estado]) VALUES (4, 7, N'Imperial IPA 1 Litro - Cantidad: 9
Imperial Amber 1 Litro - Cantidad: 5
Imperial Golden 1 Litro - Cantidad: 15
', CAST(N'2021-06-08' AS Date), 0)
INSERT [dbo].[Pedido] ([idPedido], [proveedor], [descripcion], [fecha], [estado]) VALUES (5, 6, N'Aquarius Uva 1,5 Litros - Cantidad: 16
Aquarius Manzana 500ml - Cantidad: 10
Aquarius Pera 500ml - Cantidad: 12
', CAST(N'2021-06-15' AS Date), 0)
INSERT [dbo].[Pedido] ([idPedido], [proveedor], [descripcion], [fecha], [estado]) VALUES (6, 7, N'Imperial IPA 1 Litro - Cantidad: 1
Agua Villavicencio 500ml - Cantidad: 1
Coca-Cola 500ml - Cantidad: 1
Imperial Amber 1 Litro - Cantidad: 1
', CAST(N'2021-06-20' AS Date), 0)
INSERT [dbo].[Pedido] ([idPedido], [proveedor], [descripcion], [fecha], [estado]) VALUES (7, 7, N'Fanta 500ml - Cantidad: 15
Coca-Cola Zero 500ml - Cantidad: 15
Powerade Clasica 500ml - Cantidad: 10
', CAST(N'2021-06-22' AS Date), 0)
INSERT [dbo].[Pedido] ([idPedido], [proveedor], [descripcion], [fecha], [estado]) VALUES (8, 6, N'Imperial IPA 1 Litro - Cantidad: 20
Imperial Amber 1 Litro - Cantidad: 22
Imperial Golden 1 Litro - Cantidad: 18
', CAST(N'2021-06-22' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Pedido] OFF
SET IDENTITY_INSERT [dbo].[Proveedor] ON 

INSERT [dbo].[Proveedor] ([idProveedor], [nombreCompleto], [empresa], [telefono], [email]) VALUES (6, N'Matias Dominguez', N'Quilmes', N'3515573421', N'lorenzofran1@gmail.com')
INSERT [dbo].[Proveedor] ([idProveedor], [nombreCompleto], [empresa], [telefono], [email]) VALUES (7, N'Mauricio Lopez', N'Coca Cola', N'3516426780', N'lorenzofran1@gmail.com')
SET IDENTITY_INSERT [dbo].[Proveedor] OFF
SET IDENTITY_INSERT [dbo].[Reserva] ON 

INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (152, 15, 4, 4, CAST(N'2021-06-25' AS Date), 1, 1, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (153, 15, 4, 11, CAST(N'2021-06-26' AS Date), 0, 1, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (154, 23, 1, 10, CAST(N'2021-06-26' AS Date), 1, 0, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (155, 17, 1, 3, CAST(N'2021-06-27' AS Date), 1, 1, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (156, 18, 2, 11, CAST(N'2021-06-27' AS Date), 0, 1, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (157, 17, 6, 3, CAST(N'2021-06-28' AS Date), 1, 0, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (158, 2, 3, 4, CAST(N'2021-06-27' AS Date), 1, 0, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (159, 17, 4, 10, CAST(N'2021-06-28' AS Date), 0, 1, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (160, 15, 4, 3, CAST(N'2021-07-01' AS Date), 1, 0, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (161, 17, 1, 11, CAST(N'2021-07-01' AS Date), 0, 1, 1)
INSERT [dbo].[Reserva] ([idReserva], [cliente], [cancha], [horario], [fecha], [servicioAsador], [servicioInstrumentos], [estado]) VALUES (162, 17, 6, 2, CAST(N'2021-07-01' AS Date), 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Reserva] OFF
SET IDENTITY_INSERT [dbo].[ReservaInsumos] ON 

INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (282, 152, 12, 2)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (283, 152, 14, 1)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (284, 153, 18, 2)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (285, 153, 20, 1)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (286, 155, 8, 2)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (287, 155, 11, 2)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (288, 155, 20, 1)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (289, 156, 1, 3)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (290, 156, 19, 1)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (294, 157, 16, 3)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (295, 157, 3, 1)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (296, 157, 4, 1)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (299, 161, 3, 2)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (300, 161, 4, 3)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (301, 160, 18, 2)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (302, 160, 1, 1)
INSERT [dbo].[ReservaInsumos] ([idReservaInsumos], [reserva], [insumo], [cantidad]) VALUES (303, 160, 20, 6)
SET IDENTITY_INSERT [dbo].[ReservaInsumos] OFF
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([idRol], [rol]) VALUES (1, N'administrador')
INSERT [dbo].[Rol] ([idRol], [rol]) VALUES (2, N'cliente')
SET IDENTITY_INSERT [dbo].[Rol] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (1, N'Francisco', N'1234567891', N'q', N'q', NULL, 1)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (2, N'Francisco', N'3453453453', N'a', N'a', 25, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (15, N'Rafael Nadal', N'3514569872', N'rafanadal@hotmail.com', N'123', 26, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (16, N'Roger Federer', N'3512342334', N'federerroger@gmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (17, N'Lionel Messi', N'3519904563', N'liomessi@gmail.com', N'123', 28, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (18, N'Sergio Aguero', N'3514470853', N'kunaguero@yahoo.com.ar', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (19, N'Jorge Lopez', N'3511123906', N'jorgelopez@gmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (20, N'Carlos Ibarra', N'3510095741', N'carlitosibarra@hotmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (21, N'Angelica Ureña', N'3510694888', N'angeureña@yahoo.com.ar', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (22, N'Estrella Ventura', N'3514466000', N'venturaestrella@hotmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (23, N'Sofia dos Santos', N'3514479980', N'dossantossofia@yahoo.com.ar', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (24, N'Carmen Gimenez', N'3513320069', N'carmencitagimenez@hotmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (25, N'Ana Duran', N'3513309984', N'anaduran@gmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (26, N'Cecilia Cuellar', N'3513002918', N'cecicuellar@gmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (27, N'Maria Aguilar', N'3514460499', N'aguilarmaria@gmail.com', N'123', NULL, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (28, N'Matias Lorenzo', N'3512523987', N'mati992008@gmail.com', N'123', 27, 2)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedido_Insumo] FOREIGN KEY([insumo])
REFERENCES [dbo].[Insumo] ([idInsumo])
GO
ALTER TABLE [dbo].[DetallePedido] CHECK CONSTRAINT [FK_DetallePedido_Insumo]
GO
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedido_Pedido] FOREIGN KEY([pedido])
REFERENCES [dbo].[Pedido] ([idPedido])
GO
ALTER TABLE [dbo].[DetallePedido] CHECK CONSTRAINT [FK_DetallePedido_Pedido]
GO
ALTER TABLE [dbo].[EquipoMails]  WITH CHECK ADD  CONSTRAINT [FK_EquipoMails_Email] FOREIGN KEY([email])
REFERENCES [dbo].[Email] ([idEmail])
GO
ALTER TABLE [dbo].[EquipoMails] CHECK CONSTRAINT [FK_EquipoMails_Email]
GO
ALTER TABLE [dbo].[EquipoMails]  WITH CHECK ADD  CONSTRAINT [FK_EquipoMails_Equipo] FOREIGN KEY([equipo])
REFERENCES [dbo].[Equipo] ([idEquipo])
GO
ALTER TABLE [dbo].[EquipoMails] CHECK CONSTRAINT [FK_EquipoMails_Equipo]
GO
ALTER TABLE [dbo].[HorarioReservas]  WITH CHECK ADD  CONSTRAINT [FK_HorarioReservas_Horario] FOREIGN KEY([horario])
REFERENCES [dbo].[Horario] ([idHorario])
GO
ALTER TABLE [dbo].[HorarioReservas] CHECK CONSTRAINT [FK_HorarioReservas_Horario]
GO
ALTER TABLE [dbo].[HorarioReservas]  WITH CHECK ADD  CONSTRAINT [FK_HorarioReservas_Reserva] FOREIGN KEY([reserva])
REFERENCES [dbo].[Reserva] ([idReserva])
GO
ALTER TABLE [dbo].[HorarioReservas] CHECK CONSTRAINT [FK_HorarioReservas_Reserva]
GO
ALTER TABLE [dbo].[InstrumentoRepuesto]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentoRepuesto_InstrumentoRoto] FOREIGN KEY([idInstrumentoRoto])
REFERENCES [dbo].[InstrumentoRoto] ([idInstrumentoRoto])
GO
ALTER TABLE [dbo].[InstrumentoRepuesto] CHECK CONSTRAINT [FK_InstrumentoRepuesto_InstrumentoRoto]
GO
ALTER TABLE [dbo].[InstrumentoRoto]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentoRoto_Instrumento] FOREIGN KEY([instrumento])
REFERENCES [dbo].[Instrumento] ([idInstrumento])
GO
ALTER TABLE [dbo].[InstrumentoRoto] CHECK CONSTRAINT [FK_InstrumentoRoto_Instrumento]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Proveedor] FOREIGN KEY([proveedor])
REFERENCES [dbo].[Proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Proveedor]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cancha] FOREIGN KEY([cancha])
REFERENCES [dbo].[Cancha] ([idCancha])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cancha]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cliente] FOREIGN KEY([cliente])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cliente]
GO
ALTER TABLE [dbo].[ReservaInsumos]  WITH CHECK ADD  CONSTRAINT [FK_ReservaInsumos_Insumo] FOREIGN KEY([insumo])
REFERENCES [dbo].[Insumo] ([idInsumo])
GO
ALTER TABLE [dbo].[ReservaInsumos] CHECK CONSTRAINT [FK_ReservaInsumos_Insumo]
GO
ALTER TABLE [dbo].[ReservaInsumos]  WITH CHECK ADD  CONSTRAINT [FK_ReservaInsumos_Reserva] FOREIGN KEY([reserva])
REFERENCES [dbo].[Reserva] ([idReserva])
GO
ALTER TABLE [dbo].[ReservaInsumos] CHECK CONSTRAINT [FK_ReservaInsumos_Reserva]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Equipo] FOREIGN KEY([equipo])
REFERENCES [dbo].[Equipo] ([idEquipo])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Cliente_Equipo]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([rol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
USE [master]
GO
ALTER DATABASE [Canchas_Gambeta] SET  READ_WRITE 
GO
