USE [LocalNidanERP]
GO

/****** Object:  Table [dbo].[BatchPrePlacement]    Script Date: 19/03/2018 05:42:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BatchPrePlacement](
	[BatchPrePlacementId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[BatchId] [int] NOT NULL,
	[ScheduledStartDate] [date] NOT NULL,
	[ScheduledEndDate] [date] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CentreId] [int] NOT NULL,
	[Remark] [varchar](max) NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_BatchPrePlacement] PRIMARY KEY CLUSTERED 
(
	[BatchPrePlacementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BatchPrePlacement]  WITH CHECK ADD  CONSTRAINT [FK_BatchPrePlacement_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO

ALTER TABLE [dbo].[BatchPrePlacement] CHECK CONSTRAINT [FK_BatchPrePlacement_Batch]
GO

ALTER TABLE [dbo].[BatchPrePlacement]  WITH CHECK ADD  CONSTRAINT [FK_BatchPrePlacement_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[BatchPrePlacement] CHECK CONSTRAINT [FK_BatchPrePlacement_Centre]
GO

ALTER TABLE [dbo].[BatchPrePlacement]  WITH CHECK ADD  CONSTRAINT [FK_BatchPrePlacement_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[BatchPrePlacement] CHECK CONSTRAINT [FK_BatchPrePlacement_Organisation]
GO


