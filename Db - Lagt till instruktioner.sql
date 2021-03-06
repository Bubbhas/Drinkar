USE [master]
GO
/****** Object:  Database [Drinks]    Script Date: 2018-09-07 11:27:30 ******/
CREATE DATABASE [Drinks]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Drinks', FILENAME = N'C:\Users\Administrator\Drinks.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Drinks_log', FILENAME = N'C:\Users\Administrator\Drinks_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Drinks] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Drinks].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Drinks] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Drinks] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Drinks] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Drinks] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Drinks] SET ARITHABORT OFF 
GO
ALTER DATABASE [Drinks] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Drinks] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Drinks] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Drinks] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Drinks] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Drinks] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Drinks] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Drinks] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Drinks] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Drinks] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Drinks] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Drinks] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Drinks] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Drinks] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Drinks] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Drinks] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Drinks] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Drinks] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Drinks] SET  MULTI_USER 
GO
ALTER DATABASE [Drinks] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Drinks] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Drinks] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Drinks] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Drinks] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Drinks] SET QUERY_STORE = OFF
GO
USE [Drinks]
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
USE [Drinks]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2018-09-07 11:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drink]    Script Date: 2018-09-07 11:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drink](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Glass] [int] NULL,
	[Description] [varchar](300) NULL,
	[Instructions] [varchar](500) NULL,
 CONSTRAINT [PK_Drink] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DrinkToCategory]    Script Date: 2018-09-07 11:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DrinkToCategory](
	[DrinkId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_DrinkToCategory] PRIMARY KEY CLUSTERED 
(
	[DrinkId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Glass]    Script Date: 2018-09-07 11:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Glass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
 CONSTRAINT [PK_Glass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 2018-09-07 11:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
 CONSTRAINT [PK_Ingredient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientToDrink]    Script Date: 2018-09-07 11:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientToDrink](
	[DrinkId] [int] NOT NULL,
	[IngredientId] [int] NOT NULL,
	[MeasureOfDrink] [decimal](18, 2) NULL,
 CONSTRAINT [PK_IngredientToDrink] PRIMARY KEY CLUSTERED 
(
	[DrinkId] ASC,
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Klassisk')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Söt')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Sur')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Fruktig')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (5, N'Sexuell')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (6, N'Bitter')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (7, N'Mjölkig')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Drink] ON 

INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (1, N'Mojito', NULL, N'Fräsch drink från Kuba, gjord på rom. socker och mynta. ', N'Ta lite lime och lägg i glaset, häll på sockerlag och muddla. Smiska lite mynta och släng i glaset. Fyll på med is och fyll upp till hälften med soda. Rör om så att myntan sprider sig i glaset. Fyll sedan upp med soda igen. Garnera med en myntakvist.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (2, N'Screwdriver', NULL, N'Enkel drink med vodka och apelsinjuice', N'Fyll glaset med is. Häll i vodkan och sedan apelsinjuicen. Garnera med en apelsinskiva')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (3, N'Dry martini', NULL, N'En gammal klassiker! Hur torr den ska vara bestämmer du!', N'Hur torr vill du ha denne jävel? Ska den vara riktigt torr öppnar du martiniflaskan och förolämpar martinin, annars häller du lite i glaset. Sedan fylle du ett rörglas med is och gin. Rör runt tills glaset frostar. Häll sedan upp i glaset och garnera med en oliv.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (4, N'Redbull & Vodka', NULL, N'När kvällen är till för fistpumping', N'Dags att bli riktigt jävla full snabbt. Fyll ett glas med is och häll i vodkan och fyll på med red bull.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (5, N'Margarita', NULL, N'Mexikansk feeling i drinkformat. En gammal goding!', N'Har du en burrito till hands? Nej? Trist. Iallafall, häll i ingredienserna i en shaker, fyll med is och skaka. Häll sedan upp i ett glas som har en saltkant på utsidan, salt ska inte vara i drinken.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (6, N'Negroni', NULL, N'Christoffers favorit och således din också!!', N'Bra val! Du har riktigt god smak! Häll i ingredienserna i ett glas, fyll på med is och rör om lite. Garnera med en apelsinzest och njut!')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (7, N'Sex on the beach', NULL, N';)', N';) ;) Fyll glaset med is och häll i ingredienserna med apelsinjuicen sist. Njut sedan av drinken och se upp för sand i underlivet.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (8, N'White Russian', NULL, N'En mjölkdrink med smak av kaffe!', N'Istället för mjölk till barnen. Häll ingredienserna i en shaker, fyll upp med is och skaka. Häll upp i ett glas och njut!')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (9, N'Black Russian', NULL, N'Perfekt för kvällen när kaffesmak erfordras men inget koffein.', N'Häll ingredienserna i ett glas med is, antingen krossad eller ej. Du bestämmer, du är vuxen nu när du får dricka.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (10, N'Daiquiri', NULL, N'Syrlig sommardrink, perfekt för svalkning i hettan!', N'Tjuhu! Häll ingredienserna i en shaker och fyll med is, skaka. Häll upp i ett glas och garnera med en limeskiva. Ta dig till beachen och njut. OBS! beställt ALDRIG frozen daiquiris. Det är en konspiration...')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (11, N'Cosmopolitan', NULL, N'Sex and the city? Självklart!', N'"That''s such a Samantha thing to say! OMG! Häll ingredienserna i en shaker, fyll med is och skaka. Häll upp i ett glas och garnera med ett limehjul. Åtnjuts gärna i sällskap med skvallriga vänner.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (12, N'Gin & Tonic', NULL, N'Otroligt flexibel! Byt gin och drinken blir helt annorlunda!', N'Fyll ett glas med is, häll i gin och sedan tonic. Välj mellan lime eller citronskiva, helst lime dock och ibland gurka. Det beror på din gin.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (13, N'Campari orange', NULL, N'Okänd av många, men något som måste provas! Med en liten beska och ljuvlig fruktighet är detta ett måste på sommarfesten!', N'Fyll ett glas med is, häll i campari och sedan apelsinjuice. Garnera med en apelsinskiva.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (14, N'Rom & Cola', NULL, N'Du vet...', N'Ta rom, sedan cola. I ett glas. Drick. Glöm inte is.')
INSERT [dbo].[Drink] ([Id], [Name], [Glass], [Description], [Instructions]) VALUES (15, N'Long Island Ice Tea', NULL, N'När kvällen ska glömmas. SNABBT!', NULL)
SET IDENTITY_INSERT [dbo].[Drink] OFF
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (1, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (1, 2)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (2, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (2, 4)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (3, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (4, 2)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (5, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (5, 3)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (6, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (6, 2)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (6, 6)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (7, 2)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (7, 4)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (7, 5)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (8, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (8, 7)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (9, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (10, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (10, 3)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (11, 3)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (11, 4)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (11, 5)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (12, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (13, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (13, 4)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (13, 6)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (14, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (14, 2)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (15, 1)
INSERT [dbo].[DrinkToCategory] ([DrinkId], [CategoryId]) VALUES (15, 2)
SET IDENTITY_INSERT [dbo].[Glass] ON 

INSERT [dbo].[Glass] ([Id], [Name]) VALUES (1, N'Highball')
INSERT [dbo].[Glass] ([Id], [Name]) VALUES (2, N'Martini')
INSERT [dbo].[Glass] ([Id], [Name]) VALUES (3, N'Margarita')
INSERT [dbo].[Glass] ([Id], [Name]) VALUES (4, N'Rocks')
INSERT [dbo].[Glass] ([Id], [Name]) VALUES (5, N'Old fashioned')
INSERT [dbo].[Glass] ([Id], [Name]) VALUES (6, N'Svängare')
INSERT [dbo].[Glass] ([Id], [Name]) VALUES (7, N'Vin')
SET IDENTITY_INSERT [dbo].[Glass] OFF
SET IDENTITY_INSERT [dbo].[Ingredient] ON 

INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (1, N'Ljus rom')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (2, N'Mörk rom')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (3, N'Ljus tequila')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (4, N'Gin')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (5, N'Triple sec')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (6, N'Vodka')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (7, N'Sockerlag')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (8, N'Limejuice')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (9, N'Citronjuice')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (10, N'Mynta')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (11, N'Mjölk')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (12, N'Kaffelikör')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (13, N'Red bull')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (14, N'Ginger ale')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (15, N'Apelsinjuice')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (16, N'Fruktsoda')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (17, N'Cola')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (18, N'Tonic')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (19, N'Ananasjuice')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (20, N'Kokoslikör')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (21, N'Angostura bitter')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (22, N'Campari')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (23, N'Martini - dry')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (24, N'Martini - rosso')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (25, N'Martini - bianco')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (26, N'Jägermeister')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (27, N'Äggvita')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (28, N'Persikolikör')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (29, N'Grädde')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (30, N'Kakaolikör')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (31, N'Tranbärsjuice')
INSERT [dbo].[Ingredient] ([Id], [Name]) VALUES (32, N'Citronvodka')
SET IDENTITY_INSERT [dbo].[Ingredient] OFF
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (1, 1, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (1, 7, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (1, 10, CAST(6.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (2, 6, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (2, 15, CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (3, 4, CAST(5.50 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (3, 23, CAST(0.50 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (4, 6, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (4, 13, CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (5, 3, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (5, 5, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (5, 7, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (5, 8, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (6, 4, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (6, 22, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (6, 24, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (7, 6, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (7, 15, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (7, 28, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (7, 31, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (8, 6, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (8, 11, CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (8, 12, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (9, 6, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (9, 12, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (10, 1, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (10, 7, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (10, 8, CAST(3.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (11, 5, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (11, 8, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (11, 31, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (11, 32, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (12, 4, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (12, 18, CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (13, 15, CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (13, 22, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (14, 1, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (14, 17, CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 1, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 3, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 4, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 5, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 6, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 7, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 9, CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[IngredientToDrink] ([DrinkId], [IngredientId], [MeasureOfDrink]) VALUES (15, 17, CAST(1.00 AS Decimal(18, 2)))
ALTER TABLE [dbo].[Drink]  WITH CHECK ADD  CONSTRAINT [FK_Drink_Glass] FOREIGN KEY([Glass])
REFERENCES [dbo].[Glass] ([Id])
GO
ALTER TABLE [dbo].[Drink] CHECK CONSTRAINT [FK_Drink_Glass]
GO
ALTER TABLE [dbo].[DrinkToCategory]  WITH CHECK ADD  CONSTRAINT [FK_DrinkToCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[DrinkToCategory] CHECK CONSTRAINT [FK_DrinkToCategory_Category]
GO
ALTER TABLE [dbo].[DrinkToCategory]  WITH CHECK ADD  CONSTRAINT [FK_DrinkToCategory_Drink] FOREIGN KEY([DrinkId])
REFERENCES [dbo].[Drink] ([Id])
GO
ALTER TABLE [dbo].[DrinkToCategory] CHECK CONSTRAINT [FK_DrinkToCategory_Drink]
GO
ALTER TABLE [dbo].[IngredientToDrink]  WITH CHECK ADD  CONSTRAINT [FK_IngredientToDrink_Drink] FOREIGN KEY([DrinkId])
REFERENCES [dbo].[Drink] ([Id])
GO
ALTER TABLE [dbo].[IngredientToDrink] CHECK CONSTRAINT [FK_IngredientToDrink_Drink]
GO
ALTER TABLE [dbo].[IngredientToDrink]  WITH CHECK ADD  CONSTRAINT [FK_IngredientToDrink_Ingredient] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredient] ([Id])
GO
ALTER TABLE [dbo].[IngredientToDrink] CHECK CONSTRAINT [FK_IngredientToDrink_Ingredient]
GO
USE [master]
GO
ALTER DATABASE [Drinks] SET  READ_WRITE 
GO
