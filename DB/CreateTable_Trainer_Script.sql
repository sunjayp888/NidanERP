USE [NidanProd]
GO

/****** Object:  Table [dbo].[Trainer]    Script Date: 22/03/2017 06:02:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Trainer](
	[TrainerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Gender] [varchar](100) NULL,
	[AadharNo] [bigint] NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[EmailId] [varchar](500) NULL,
	[CertificationNo] [varchar](500) NULL,
	[SectorId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NULL,
	[CreatedDate] [date] NOT NULL,
 CONSTRAINT [PK_Trainer] PRIMARY KEY CLUSTERED 
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Trainer]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Trainer] CHECK CONSTRAINT [FK_Trainer_Centre]
GO

ALTER TABLE [dbo].[Trainer]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[Trainer] CHECK CONSTRAINT [FK_Trainer_Course]
GO

ALTER TABLE [dbo].[Trainer]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Trainer] CHECK CONSTRAINT [FK_Trainer_Organisation]
GO

ALTER TABLE [dbo].[Trainer]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO

ALTER TABLE [dbo].[Trainer] CHECK CONSTRAINT [FK_Trainer_Personnel]
GO

ALTER TABLE [dbo].[Trainer]  WITH CHECK ADD  CONSTRAINT [FK_Trainer_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO

ALTER TABLE [dbo].[Trainer] CHECK CONSTRAINT [FK_Trainer_Sector]
GO


