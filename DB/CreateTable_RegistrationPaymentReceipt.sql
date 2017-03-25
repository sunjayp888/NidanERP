USE [NidanProd]
GO

/****** Object:  Table [dbo].[RegistrationPaymentReceipt]    Script Date: 16/03/2017 4:03:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RegistrationPaymentReceipt](
	[RegistrationPaymentReceiptId] [int] IDENTITY(1,1) NOT NULL,
	[CentreId] [int] NOT NULL,
	[EnquiryId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Fees] [int] NOT NULL,
	[ChequeNo] [varchar](100) NOT NULL,
	[ChequeDate] [date] NOT NULL,
	[BankName] [varchar](500) NOT NULL,
	[Particulars] [varchar](500) NOT NULL,
	[PaymentModeId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[RegistrationDate] [date] NOT NULL,
 CONSTRAINT [PK_RegistrationPaymentReceipt] PRIMARY KEY CLUSTERED 
(
	[RegistrationPaymentReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Centre] FOREIGN KEY([CentreId])
REFERENCES [dbo].[Centre] ([CentreId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Centre]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Course]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Enquiry] FOREIGN KEY([EnquiryId])
REFERENCES [dbo].[Enquiry] ([EnquiryId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Enquiry]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_Organisation]
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationPaymentReceipt_PaymentMode] FOREIGN KEY([PaymentModeId])
REFERENCES [dbo].[PaymentMode] ([PaymentModeId])
GO

ALTER TABLE [dbo].[RegistrationPaymentReceipt] CHECK CONSTRAINT [FK_RegistrationPaymentReceipt_PaymentMode]
GO


