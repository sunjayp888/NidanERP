USE [NidanProd]
GO

/****** Object:  Table [dbo].[SubjectCourse]    Script Date: 12/04/2017 12:48:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubjectCourse](
	[SubjectCourseId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_SubjectCourse] PRIMARY KEY CLUSTERED 
(
	[SubjectCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubjectCourse]  WITH CHECK ADD  CONSTRAINT [FK_SubjectCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[SubjectCourse] CHECK CONSTRAINT [FK_SubjectCourse_Course]
GO

ALTER TABLE [dbo].[SubjectCourse]  WITH CHECK ADD  CONSTRAINT [FK_SubjectCourse_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[SubjectCourse] CHECK CONSTRAINT [FK_SubjectCourse_Organisation]
GO

ALTER TABLE [dbo].[SubjectCourse]  WITH CHECK ADD  CONSTRAINT [FK_SubjectCourse_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO

ALTER TABLE [dbo].[SubjectCourse] CHECK CONSTRAINT [FK_SubjectCourse_Subject]
GO


