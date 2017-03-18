USE [NidanProd]
GO

/****** Object:  Table [dbo].[Registration]    Script Date: 16/03/2017 4:17:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Registration](
	[RegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationPaymentReceiptId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Registration]  WITH CHECK ADD  CONSTRAINT [FK_Registration_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO

ALTER TABLE [dbo].[Registration] CHECK CONSTRAINT [FK_Registration_Organisation]
GO

ALTER TABLE [dbo].[Registration]  WITH CHECK ADD  CONSTRAINT [FK_Registration_RegistrationPaymentReceipt] FOREIGN KEY([RegistrationPaymentReceiptId])
REFERENCES [dbo].[RegistrationPaymentReceipt] ([RegistrationPaymentReceiptId])
GO

ALTER TABLE [dbo].[Registration] CHECK CONSTRAINT [FK_Registration_RegistrationPaymentReceipt]
GO


