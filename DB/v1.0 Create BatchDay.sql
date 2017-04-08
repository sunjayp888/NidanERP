USE [NidanDev]
GO

/****** Object:  Table [dbo].[BatchDay]    Script Date: 08-04-2017 17:03:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BatchDay](
	[BatchDayId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[BatchId] [int] NOT NULL,
	[IsMonday] [bit] NOT NULL,
	[IsTuesday] [bit] NOT NULL,
	[IsWednesday] [bit] NOT NULL,
	[IsThursday] [bit] NOT NULL,
	[IsFriday] [bit] NOT NULL,
	[IsSaturday] [bit] NOT NULL,
	[IsSunday] [bit] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_BatchDay] PRIMARY KEY CLUSTERED 
(
	[BatchDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BatchDay]  WITH CHECK ADD  CONSTRAINT [FK_BatchDay_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO

ALTER TABLE [dbo].[BatchDay] CHECK CONSTRAINT [FK_BatchDay_Batch]
GO

ALTER TABLE [dbo].[BatchDay]  WITH CHECK ADD  CONSTRAINT [FK_BatchDay_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[BatchDay] CHECK CONSTRAINT [FK_BatchDay_Centre]
GO

ALTER TABLE [dbo].[BatchDay]  WITH CHECK ADD  CONSTRAINT [FK_BatchDay_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[BatchDay] CHECK CONSTRAINT [FK_BatchDay_Organisation]
GO


