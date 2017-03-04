USE [Nidan_Dev]
GO

/****** Object:  Table [dbo].[GovernmentAdmission]    Script Date: 04/03/2017 02:38:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GovernmentAdmission](
	[GovernmentAdmissionId] [int] IDENTITY(1,1) NOT NULL,
	[AdmissionDate] [date] NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[BatchId] [int] NULL,
	[SchemeId] [int] NOT NULL,
	[TrainingType] [varchar](500) NOT NULL,
	[SectorId] [int] NOT NULL,
	[SubSectorId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[HowDidYouKnowAboutId] [int] NOT NULL,
	[ConveyanceAndBoardingPreference] [varchar](500) NOT NULL,
	[Salutation] [varchar](50) NOT NULL,
	[FirstName] [varchar](500) NOT NULL,
	[MiddleName] [varchar](500) NULL,
	[LastName] [varchar](500) NOT NULL,
	[FatherName] [varchar](500) NULL,
	[Gender] [varchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[YearOfBirth] [int] NULL,
	[AadhaarNo] [bigint] NOT NULL,
	[AadhaarVerificationStatus] [varchar](1000) NULL,
	[DisabilityId] [int] NOT NULL,
	[QualificationId] [int] NOT NULL,
	[CasteCategoryId] [int] NOT NULL,
	[ReligionId] [int] NOT NULL,
	[AlternateIdTypeId] [int] NULL,
	[AlternateIdNumber] [bigint] NULL,
	[Address] [varchar](max) NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[LandlineNo] [bigint] NULL,
	[EmailId] [varchar](500) NULL,
	[NameAsInBank] [varchar](500) NULL,
	[BankAccountNo] [bigint] NULL,
	[IfscCode] [varchar](500) NULL,
	[BankName] [varchar](500) NULL,
	[TcName] [varchar](500) NULL,
	[TcId] [varchar](500) NULL,
	[PartnerName] [varchar](500) NULL,
	[TcAddress] [varchar](max) NULL,
	[SdmsCandidateId] [varchar](500) NULL,
 CONSTRAINT [PK_GovernmentAdmission] PRIMARY KEY CLUSTERED 
(
	[GovernmentAdmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_AlternateIdType] FOREIGN KEY([AlternateIdTypeId])
REFERENCES [dbo].[AlternateIdType] ([AlternateIdTypeId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_AlternateIdType]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Batch]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_CasteCategory] FOREIGN KEY([CasteCategoryId])
REFERENCES [dbo].[CasteCategory] ([CasteCategoryId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_CasteCategory]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Centre]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Course]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Disability] FOREIGN KEY([DisabilityId])
REFERENCES [dbo].[Disability] ([DisabilityId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Disability]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Enquiry]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_HowDidYouKnowAbout] FOREIGN KEY([HowDidYouKnowAboutId])
REFERENCES [dbo].[HowDidYouKnowAbout] ([HowDidYouKnowAboutId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_HowDidYouKnowAbout]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Organisation]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Qualification] FOREIGN KEY([QualificationId])
REFERENCES [dbo].[Qualification] ([QualificationId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Qualification]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Religion] FOREIGN KEY([ReligionId])
REFERENCES [dbo].[Religion] ([ReligionId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Religion]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Scheme] FOREIGN KEY([SchemeId])
REFERENCES [dbo].[Scheme] ([SchemeId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Scheme]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_Sector]
GO

ALTER TABLE [dbo].[GovernmentAdmission]  WITH CHECK ADD  CONSTRAINT [FK_GovernmentAdmission_SubSector] FOREIGN KEY([SubSectorId])
REFERENCES [dbo].[SubSector] ([SubSectorId])
GO

ALTER TABLE [dbo].[GovernmentAdmission] CHECK CONSTRAINT [FK_GovernmentAdmission_SubSector]
GO


