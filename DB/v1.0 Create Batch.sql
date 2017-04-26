USE [NidanProd]
GO

/****** Object:  Table [dbo].[Batch]    Script Date: 22/04/2017 04:07:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Batch](
	[BatchId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Intake] [int] NOT NULL,
	[CourseInstallmentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[BatchStartDate] [date] NOT NULL,
	[BatchEndDate] [date] NOT NULL,
	[NoOfHolidays] [int] NOT NULL,
	[NoOfHoursDaily] [int] NOT NULL,
	[BatchStartTimeHours] [int] NOT NULL,
	[BatchStartTimeMinutes] [int] NOT NULL,
	[BatchStartTimeSpan] [varchar](10) NOT NULL,
	[BatchEndTimeHours] [int] NOT NULL,
	[BatchEndTimeMinutes] [int] NOT NULL,
	[BatchEndTimeSpan] [varchar](10) NOT NULL,
	[AssesmentDate] [date] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[Remarks] [varchar](1000) NULL,
	[Month] [int] NOT NULL,
	[NoOfInstallment] [int] NOT NULL,
	[FirstInstallment] [int] NULL,
	[SecondInstallment] [int] NULL,
	[ThirdInstallment] [int] NULL,
	[ForthInstallment] [int] NULL,
	[FifthInstallment] [int] NULL,
	[SixthInstallment] [int] NULL,
	[SeventhInstallment] [int] NULL,
	[EighthInstallment] [int] NULL,
	[NinethInstallment] [int] NULL,
	[TenthInstallment] [int] NULL,
	[EleventhInstallment] [int] NULL,
	[TwelvethInstallment] [int] NULL,
	[RoomId] [int] NULL,
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

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_CourseInstallment] FOREIGN KEY([CourseInstallmentId])
REFERENCES [dbo].[CourseInstallment] ([CourseInstallmentId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_CourseInstallment]
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Organisation]
GO

ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO

ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Room]
GO


