USE [NidanProd]
GO

/****** Object:  Table [dbo].[Room]    Script Date: 29/03/2017 06:03:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[Number] [int] NOT NULL,
	[Floor] [int] NOT NULL,
	[OccupiedStartDate] [date] NULL,
	[OccupiedEndDate] [date] NULL,
	[OccupiedStartTime] [varchar](50) NULL,
	[OccupiedEndTime] [varchar](50) NULL,
	[BatchId] [int] NULL,
	[RommTypeId] [int] NOT NULL,
	[Capacity] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO

ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Batch]
GO

ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_CourseType] FOREIGN KEY([RommTypeId])
REFERENCES [dbo].[CourseType] ([CourseTypeId])
GO

ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_CourseType]
GO

ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Organisation]
GO


