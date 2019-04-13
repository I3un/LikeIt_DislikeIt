USE [Sample]
GO

/****** Object:  Table [dbo].[Likes]    Script Date: 13/04/2019 7:18:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Likes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](250) NULL,
	[Like] [tinyint] NULL,
	[Dislike] [tinyint] NULL,
 CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


