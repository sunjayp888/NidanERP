USE [NidanDev]
GO

/****** Object:  Table [dbo].[CentreCourse]    Script Date: 10-04-2017 08:38:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CentreCourse](
	[CentreCourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_CentreCourse] PRIMARY KEY CLUSTERED 
(
	[CentreCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CentreCourse]  WITH CHECK ADD  CONSTRAINT [FK_CentreCourse_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[CentreCourse] CHECK CONSTRAINT [FK_CentreCourse_Centre]
GO

ALTER TABLE [dbo].[CentreCourse]  WITH CHECK ADD  CONSTRAINT [FK_CentreCourse_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[CentreCourse] CHECK CONSTRAINT [FK_CentreCourse_Organisation]
GO


