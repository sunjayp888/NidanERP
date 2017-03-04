USE [Nidan_Dev]
GO

/****** Object:  Table [dbo].[CommercialAdmission]    Script Date: 04/03/2017 02:37:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CommercialAdmission](
	[CommercialAdmissionId] [int] IDENTITY(1,1) NOT NULL,
	[AdmissionDate] [date] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[BatchId] [int] NULL,
	[SectorId] [int] NULL,
	[CourseId] [int] NULL,
	[StudentTypeId] [int] NULL,
	[Salutation] [varchar](50) NOT NULL,
	[FirstName] [varchar](500) NOT NULL,
	[MiddleName] [varchar](500) NULL,
	[LastName] [varchar](500) NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[EmailId] [varchar](500) NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [varchar](100) NOT NULL,
	[CasteCategoryId] [int] NOT NULL,
	[ReligionId] [int] NOT NULL,
	[FatherName] [varchar](500) NULL,
	[FatherMobile] [bigint] NULL,
	[ResidentialNo] [bigint] NULL,
	[Address] [varchar](max) NOT NULL,
	[TalukaId] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[PinCode] [int] NULL,
	[CommunicationAddress] [varchar](max) NULL,
	[CommunicationTalukaId] [int] NULL,
	[CommunicationDistrictId] [int] NULL,
	[CommunicationStateId] [int] NULL,
	[CommunicationPinCode] [int] NULL,
	[QualificationId] [int] NOT NULL,
	[ProfessionalQualification] [varchar](500) NULL,
	[TechnicalQualification] [varchar](500) NULL,
	[PreTrainingStatus] [varchar](100) NOT NULL,
	[YearOfExperience] [decimal](18, 2) NULL,
	[EmploymentStatus] [varchar](100) NOT NULL,
	[EmployerName] [varchar](500) NULL,
	[EmployerContactNo] [bigint] NULL,
	[EmployerAddress] [varchar](max) NULL,
	[AnnualIncome] [bigint] NULL,
 CONSTRAINT [PK_CommercialAdmission] PRIMARY KEY CLUSTERED 
(
	[CommercialAdmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Batch]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_CasteCategory] FOREIGN KEY([CasteCategoryId])
REFERENCES [dbo].[CasteCategory] ([CasteCategoryId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_CasteCategory]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Centre]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Course] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Course]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([DistrictId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_District]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_DistrictCommunication] FOREIGN KEY([CommunicationDistrictId])
REFERENCES [dbo].[District] ([DistrictId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_DistrictCommunication]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Enquiry]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Organisation]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Qualification] FOREIGN KEY([QualificationId])
REFERENCES [dbo].[Qualification] ([QualificationId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Qualification]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Religion] FOREIGN KEY([ReligionId])
REFERENCES [dbo].[Religion] ([ReligionId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Religion]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Sector] FOREIGN KEY([SectorId])
REFERENCES [dbo].[Sector] ([SectorId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Sector]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_State]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_StateCommunication] FOREIGN KEY([CommunicationStateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_StateCommunication]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_StudentType] FOREIGN KEY([StudentTypeId])
REFERENCES [dbo].[StudentType] ([StudentTypeId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_StudentType]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_Taluka] FOREIGN KEY([TalukaId])
REFERENCES [dbo].[Taluka] ([TalukaId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_Taluka]
GO

ALTER TABLE [dbo].[CommercialAdmission]  WITH CHECK ADD  CONSTRAINT [FK_CommercialAdmission_TalukaCommunication] FOREIGN KEY([CommunicationTalukaId])
REFERENCES [dbo].[Taluka] ([TalukaId])
GO

ALTER TABLE [dbo].[CommercialAdmission] CHECK CONSTRAINT [FK_CommercialAdmission_TalukaCommunication]
GO


