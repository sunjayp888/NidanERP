USE [NidanProd]
GO

/****** Object:  Table [dbo].[Admission]    Script Date: 22/04/2017 04:11:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Admission](
	[AdmissionId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationPaymentReceiptId] [int] NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[BatchId] [int] NOT NULL,
	[CentreId] [int] NOT NULL,
	[Fee] [int] NULL,
	[DownPayment] [int] NULL,
	[Lumpsum] [int] NULL,
	[Discount] [int] NULL,
	[DiscountFee] [int] NULL,
	[Particulars] [varchar](500) NULL,
	[PaymentType] [varchar](100) NOT NULL,
	[PaymentModeId] [int] NOT NULL,
	[ChequeNo] [varchar](100) NOT NULL,
	[ChequeDate] [date] NOT NULL,
	[BankName] [varchar](1000) NOT NULL,
	[FinancialYear] [varchar](50) NULL,
	[AdmissionDate] [date] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Admission] PRIMARY KEY CLUSTERED 
(
	[AdmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO

ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Batch]
GO

ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Centre]
GO

ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO

ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Enquiry]
GO

ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Organisation]
GO

ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_PaymentMode] FOREIGN KEY([PaymentModeId])
REFERENCES [dbo].[PaymentMode] ([PaymentModeId])
GO

ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_PaymentMode]
GO

ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_RegistrationPaymentReceipt] FOREIGN KEY([RegistrationPaymentReceiptId])
REFERENCES [dbo].[RegistrationPaymentReceipt] ([RegistrationPaymentReceiptId])
GO

ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_RegistrationPaymentReceipt]
GO


