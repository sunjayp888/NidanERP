USE [Nidan_Dev]
GO

/****** Object:  Table [dbo].[Counselling]    Script Date: 27/02/2017 6:09:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Counselling](
	[CounsellingId] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[CourseOfferedId] [int] NOT NULL,
	[PreferTiming] [varchar](500) NULL,
	[Remarks] [varchar](max) NULL,
	[FollowUpDate] [date] NULL,
	[RemarkByBranchManager] [varchar](max) NULL,
	[Name] [varchar](500) NULL,
	[SectorId] [int] NULL,
	[PsychomatricTest] [varchar](100) NULL,
	[ConversionProspect] [varchar](100) NULL,
 CONSTRAINT [PK_Counselling] PRIMARY KEY CLUSTERED 
(
	[CounsellingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Centre]
GO

ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Course] FOREIGN KEY([CourseOfferedId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Course]
GO

ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO

ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Enquiry]
GO

ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Organisation]
GO

ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Personnel] FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnel] ([PersonnelId])
GO

ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Personnel]
GO

ALTER TABLE [dbo].[Counselling]  WITH CHECK ADD  CONSTRAINT [FK_Counselling_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO

ALTER TABLE [dbo].[Counselling] CHECK CONSTRAINT [FK_Counselling_Sector]
GO


