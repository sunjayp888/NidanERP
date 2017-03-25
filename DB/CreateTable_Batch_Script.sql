USE [NidanProd]
GO

/****** Object:  Table [dbo].[Batch]    Script Date: 23/03/2017 05:58:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Batch](
	[BatchId] [int] IDENTITY(1,1) NOT NULL,
	[SchemeId] [int] NOT NULL,
	[BatchType] [varchar](100) NOT NULL,
	[SectorId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[NoOfDays] [int] NULL,
	[NoOfHrs] [int] NOT NULL,
	[NoOfHolidays] [int] NULL,
	[BatchStartDate] [date] NOT NULL,
	[BatchEndDate] [date] NOT NULL,
	[PreferredAssesmentDate] [date] NOT NULL,
	[BatchTime] [varchar](100) NULL,
	[TrainerId] [int] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[Remarks] [varchar](1000) NULL,
 CONSTRAINT [PK_Batch] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Centre]
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Course]
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Organisation]
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Scheme]
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Sector]
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainer] ([TrainerId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Trainer]
GO


