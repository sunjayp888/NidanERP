USE [NidanProd]
GO

/****** Object:  Table [dbo].[CentreScheme]    Script Date: 14/04/2017 06:34:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CentreScheme](
	[CentreSchemeId] [int] IDENTITY(1,1) NOT NULL,
	[SchemeId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreScheme] PRIMARY KEY CLUSTERED 
(
	[CentreSchemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CentreScheme]  WITH CHECK ADD  CONSTRAINT [FK_CentreScheme_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[CentreScheme] CHECK CONSTRAINT [FK_CentreScheme_Centre]
GO

ALTER TABLE [dbo].[CentreScheme]  WITH CHECK ADD  CONSTRAINT [FK_CentreScheme_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[CentreScheme] CHECK CONSTRAINT [FK_CentreScheme_Organisation]
GO

ALTER TABLE [dbo].[CentreScheme]  WITH CHECK ADD  CONSTRAINT [FK_CentreScheme_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO

ALTER TABLE [dbo].[CentreScheme] CHECK CONSTRAINT [FK_CentreScheme_Scheme]
GO


