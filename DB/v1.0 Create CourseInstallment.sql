USE [NidanProd]
GO

/****** Object:  Table [dbo].[CourseInstallment]    Script Date: 14/04/2017 02:26:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CourseInstallment](
	[CourseInstallmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[CourseId] [int] NOT NULL,
	[Fee] [int] NOT NULL,
	[DownPayment] [int] NOT NULL,
	[LumpsumAmt] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[CreatedDate] [date] NOT NULL,
 CONSTRAINT [PK_CourseInstallment] PRIMARY KEY CLUSTERED 
(
	[CourseInstallmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CourseInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CourseInstallment_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[CourseInstallment] CHECK CONSTRAINT [FK_CourseInstallment_Course]
GO

ALTER TABLE [dbo].[CourseInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CourseInstallment_CourseInstallment] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[CourseInstallment] CHECK CONSTRAINT [FK_CourseInstallment_CourseInstallment]
GO


