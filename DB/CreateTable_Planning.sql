USE [NidanProd]
GO

/****** Object:  Table [dbo].[Planning]    Script Date: 16/03/2017 7:11:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Planning](
	[PlanningId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Completed] [varchar](10) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
 CONSTRAINT [PK_Planning] PRIMARY KEY CLUSTERED 
(
	[PlanningId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Centre]
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Event]
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Organisation]
GO

ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO

ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_Question]
GO


