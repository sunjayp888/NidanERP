USE [NidanProd]
GO

/****** Object:  Table [dbo].[CentreSector]    Script Date: 14/04/2017 06:35:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CentreSector](
	[CentreSectorId] [int] IDENTITY(1,1) NOT NULL,
	[SectorId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreSector] PRIMARY KEY CLUSTERED 
(
	[CentreSectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CentreSector]  WITH CHECK ADD  CONSTRAINT [FK_CentreSector_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[CentreSector] CHECK CONSTRAINT [FK_CentreSector_Centre]
GO

ALTER TABLE [dbo].[CentreSector]  WITH CHECK ADD  CONSTRAINT [FK_CentreSector_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[CentreSector] CHECK CONSTRAINT [FK_CentreSector_Organisation]
GO

ALTER TABLE [dbo].[CentreSector]  WITH CHECK ADD  CONSTRAINT [FK_CentreSector_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO

ALTER TABLE [dbo].[CentreSector] CHECK CONSTRAINT [FK_CentreSector_Sector]
GO


