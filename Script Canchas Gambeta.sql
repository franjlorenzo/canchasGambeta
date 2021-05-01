USE [master]
GO
/****** Object:  Database [Canchas_Gambeta]    Script Date: 28/04/2021 16:29:21 ******/
CREATE DATABASE [Canchas_Gambeta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Canchas_Gambeta', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Canchas_Gambeta.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Canchas_Gambeta_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Canchas_Gambeta_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Canchas_Gambeta] SET COMPATIBILITY_LEVEL = 140
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
ALTER DATABASE [Canchas_Gambeta] SET QUERY_STORE = OFF
GO
USE [Canchas_Gambeta]
GO
/****** Object:  Table [dbo].[Cancha]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[ElementoRoto]    Script Date: 28/04/2021 16:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElementoRoto](
	[idElementoRoto] [int] IDENTITY(1,1) NOT NULL,
	[elemento] [nvarchar](100) NOT NULL,
	[fecha] [date] NOT NULL,
 CONSTRAINT [PK_ElementoRoto] PRIMARY KEY CLUSTERED 
(
	[idElementoRoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Email]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[Equipo]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[EquipoMails]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[Horario]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[HorarioReservas]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[Insumo]    Script Date: 28/04/2021 16:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insumo](
	[idInsumo] [int] IDENTITY(1,1) NOT NULL,
	[insumo] [nvarchar](50) NOT NULL,
	[precio] [money] NOT NULL,
	[stock] [int] NOT NULL,
 CONSTRAINT [PK_Insumo] PRIMARY KEY CLUSTERED 
(
	[idInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 28/04/2021 16:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[proveedor] [int] NOT NULL,
	[descripcion] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 28/04/2021 16:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[idProveedor] [int] IDENTITY(1,1) NOT NULL,
	[nombreCompleto] [nvarchar](50) NOT NULL,
	[empresa] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 28/04/2021 16:29:22 ******/
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
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[idReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservaInsumos]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[Rol]    Script Date: 28/04/2021 16:29:22 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 28/04/2021 16:29:22 ******/
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

INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (1, N'Futbol - Cancha "L. Messi"')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (2, N'Futbol - Cancha "D. Maradona"')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (3, N'Tenis - Cancha "J M del Potro"')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (4, N'Tenis - Cancha "D. Schwartzman"')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (5, N'Padel - Cancha "S. Gutiérrez"')
INSERT [dbo].[Cancha] ([idCancha], [tipoCancha]) VALUES (6, N'Padel - Cancha "F. Belasteguín"')
SET IDENTITY_INSERT [dbo].[Cancha] OFF
SET IDENTITY_INSERT [dbo].[Email] ON 

INSERT [dbo].[Email] ([idEmail], [email]) VALUES (1, N'lorenzofran1@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (2, N'asdasd@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (3, N'matifl@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (4, N'qwerqwer@gmail.com')
INSERT [dbo].[Email] ([idEmail], [email]) VALUES (5, N'opopopo@gmail.com')
SET IDENTITY_INSERT [dbo].[Email] OFF
SET IDENTITY_INSERT [dbo].[Equipo] ON 

INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (4, N'0')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (5, N'boca')
INSERT [dbo].[Equipo] ([idEquipo], [nombreEquipo]) VALUES (6, N'lanus')
SET IDENTITY_INSERT [dbo].[Equipo] OFF
SET IDENTITY_INSERT [dbo].[EquipoMails] ON 

INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (1, 1, 5)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (2, 2, 5)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (3, 3, 5)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (4, 4, 6)
INSERT [dbo].[EquipoMails] ([idEquipoMails], [email], [equipo]) VALUES (5, 5, 6)
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
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([idRol], [rol]) VALUES (1, N'administrador')
INSERT [dbo].[Rol] ([idRol], [rol]) VALUES (2, N'cliente')
SET IDENTITY_INSERT [dbo].[Rol] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (1, N'Francisco', N'1234567890', N'lorenzof@gmail.com', N'francisco123', NULL, 1)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (2, N'Lio Messi', N'3453453453', N'liomessi@hotmail.com', N'a', 5, 2)
INSERT [dbo].[Usuario] ([idUsuario], [nombreCompleto], [telefono], [email], [password], [equipo], [rol]) VALUES (3, N'Matias', N'1231231231', N'mati992008@gmail.com', N'matibobi', NULL, 2)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
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
