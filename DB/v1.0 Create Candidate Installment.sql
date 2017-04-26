USE [NidanProd]
GO

/****** Object:  Table [dbo].[CandidateInstallment]    Script Date: 22/04/2017 04:11:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CandidateInstallment](
	[CandidateInstallmentId] [int] IDENTITY(1,1) NOT NULL,
	[AdmissionId] [int] NOT NULL,
	[Fee] [int] NOT NULL,
	[DownPayment] [int] NOT NULL,
	[Discount] [int] NULL,
	[DiscountFee] [int] NULL,
	[Month] [int] NOT NULL,
	[NoOfInstallment] [int] NOT NULL,
	[FirstInstallment] [int] NULL,
	[SecondInstallment] [int] NULL,
	[ThirdInstallment] [int] NULL,
	[ForthInstallment] [int] NULL,
	[FifthInstallment] [int] NULL,
	[SixthInstallment] [int] NULL,
	[SeventhInstallment] [int] NULL,
	[EighthInstallment] [int] NULL,
	[NinethInstallment] [int] NULL,
	[TenthInstallment] [int] NULL,
	[EleventhInstallment] [int] NULL,
	[TwelvethInstallment] [int] NULL,
	[CentreId] [int] NULL,
	[OrganisationId] [int] NULL,
 CONSTRAINT [PK_CandidateInstallment] PRIMARY KEY CLUSTERED 
(
	[CandidateInstallmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CandidateInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInstallment_Admission] FOREIGN KEY([AdmissionId])
REFERENCES [dbo].[Admission] ([AdmissionId])
GO

ALTER TABLE [dbo].[CandidateInstallment] CHECK CONSTRAINT [FK_CandidateInstallment_Admission]
GO

ALTER TABLE [dbo].[CandidateInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInstallment_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[CandidateInstallment] CHECK CONSTRAINT [FK_CandidateInstallment_Centre]
GO

ALTER TABLE [dbo].[CandidateInstallment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInstallment_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[CandidateInstallment] CHECK CONSTRAINT [FK_CandidateInstallment_Organisation]
GO


