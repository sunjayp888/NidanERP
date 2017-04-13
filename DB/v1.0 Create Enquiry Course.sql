USE [NidanProd]
GO

/****** Object:  Table [dbo].[EnquiryCourse]    Script Date: 12/04/2017 12:47:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EnquiryCourse](
	[EnquiryCourseId] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_EnquiryCourse] PRIMARY KEY CLUSTERED 
(
	[EnquiryCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EnquiryCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnquiryCourse_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[EnquiryCourse] CHECK CONSTRAINT [FK_EnquiryCourse_Centre]
GO

ALTER TABLE [dbo].[EnquiryCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnquiryCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[EnquiryCourse] CHECK CONSTRAINT [FK_EnquiryCourse_Course]
GO

ALTER TABLE [dbo].[EnquiryCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnquiryCourse_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO

ALTER TABLE [dbo].[EnquiryCourse] CHECK CONSTRAINT [FK_EnquiryCourse_Enquiry]
GO

ALTER TABLE [dbo].[EnquiryCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnquiryCourse_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[EnquiryCourse] CHECK CONSTRAINT [FK_EnquiryCourse_Organisation]
GO


